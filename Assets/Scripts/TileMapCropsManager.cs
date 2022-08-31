using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapCropsManager : TimeAgent
{
    [SerializeField] TileBase plowed;
    [SerializeField] TileBase seeded;
    [SerializeField] TileBase watered;

    Tilemap targetTilemap;

    [SerializeField] GameObject cropsSpritePrefab;
    [SerializeField] CropsContainer container;

    [SerializeField] PlaceableObjectsContainer placeableObjectsContainer;
    [SerializeField] TreesContainer treesContainer;
    [SerializeField] ReservedCells reservedCells;
    [SerializeField] TileMapReadController tilemapReader;
    [SerializeField] AudioClip OnPickUp;

    private int timer = 0;
    private bool isRaining = false;

    private void Start()
    {
        GameManager.instance.GetComponent<CropsManager>().cropsManager = this;
        targetTilemap = GetComponent<Tilemap>();
        onTimeTick += Tick;
        Init();
        VisualizeMap();
    }

    void OnDestroy()
    {
        for(int i=0; i< container.crops.Count; i++)
        {
            container.crops[i].renderer = null;
        }
        FileHandler.SavetoJSON<CropTile>(container.crops,"crops.json");
    }

    internal bool Check(Vector3Int position)
    {
        return container.Get(position) != null || placeableObjectsContainer.Get(position) != null ||  treesContainer.Get(position) != null 
                    || reservedCells.Get(position) || tilemapReader.GetTileOcean(position) != null;
    }

    internal bool isCropGrowing(Vector3Int position)
    {
        return container.Get(position).crop != null;
    }

    internal bool isWatered(Vector3Int position)
    {
        CropTile tile = container.Get(position);
        if(tile !=null){return tile.watered;}
        return false;
    }

    public void Tick()
    {
        ++timer;

        if(targetTilemap == null){ return; }

        List<CropTile> instanceList = new List<CropTile>(container.crops);

        foreach (CropTile cropTile in instanceList)
        {
          
            if(cropTile.crop == null)
            {
                if(timer % 96 == 0){
                    
                    if(cropTile.watered == true && !isRaining)
                    { 
                        cropTile.watered = false;  
                        targetTilemap.SetTile(cropTile.position, plowed);
                        cropTile.waterlessTimeToLive = 96 * 2;
                    }
                    else
                    {
                        cropTile.waterlessTimeToLive -= 96;
                        if(cropTile.waterlessTimeToLive <= 0)
                        {
                            targetTilemap.SetTile(cropTile.position, null);
                            container.Clear(cropTile);
                        }
                    }
                }
                continue;
            }

            if(cropTile.Complete)
            {
                GameObject marker = cropTile.cropObject.transform.GetChild(0).gameObject;
                    
                if(marker != null)
                {
                    marker.SetActive(false);
                }
                continue;
            }
            else
            {
                ++cropTile.growTimer;
                ++cropTile.damage;

                if(cropTile.crop.damageToDead <= cropTile.damage)
                {   
                    cropTile.Harvested();
                    targetTilemap.SetTile(cropTile.position, plowed);
                    continue;
                }
                else
                {
                    GameObject marker = cropTile.cropObject.transform.GetChild(0).gameObject;
                    
                    if(marker != null)
                    {
                        if(cropTile.damage >= cropTile.crop.damageToDead/2)
                        {
                            marker.SetActive(true);
                            targetTilemap.SetTile(cropTile.position, plowed);
                            cropTile.watered = false;
                        }
                    }
                }
            }

            if(cropTile.growTimer >= cropTile.crop.growthStageTime[cropTile.growStage])
            {
                cropTile.renderer.gameObject.SetActive(true);
                cropTile.growStage += 1;
                cropTile.renderer.sprite = cropTile.crop.sprites[cropTile.growStage];
               
                if(cropTile.Complete)
                {
                    GameObject marker = cropTile.cropObject.transform.GetChild(0).gameObject;
                        
                    if(marker != null)
                    {
                        marker.SetActive(false);
                    }
                    continue;
                }
            }
        }
    }
    public void Plow(Vector3Int position)
    {
        if(Check(position)==true){return;}
        CreatePlowedTile(position);
    }

    public void Seeded(Vector3Int position, Crop toSeed)
    {
        CropTile tile = container.Get(position);

        if(tile==null){return;}

        targetTilemap.SetTile(position, seeded);

        tile.crop = toSeed;

        tile.renderer.gameObject.SetActive(true);
        tile.renderer.sprite = tile.crop.sprites[0];
        tile.watered = false;

        GameObject marker = tile.cropObject.transform.GetChild(0).gameObject;
        if(marker != null)
        {
            marker.SetActive(false);
        }

        if(isRaining){WaterCrop(position);};
    }

    public void WaterCrop(Vector3Int position)
    {
        CropTile tile = container.Get(position);

        if(tile==null){return;}

        tile.watered = true;
        tile.damage = 0;


        if(tile.crop != null)
        {
            GameObject marker = tile.cropObject.transform.GetChild(0).gameObject;
        
            if(marker != null)
            {
                marker.SetActive(false);
            }
        }
        CreateWateredTile(position);
    }

    public void WaterAllCropTiles()
    {
        foreach (CropTile cropTile in container.crops)
        {
            WaterCrop(cropTile.position);
        }
        
        setRaining(true);
    }

    public void setRaining(bool boolean)
    {
        isRaining = boolean;
    }

    public void CreatePlowedTile(Vector3Int position)
    {
        CropTile crop = new CropTile();
        container.Add(crop);
        crop.position = position;
        VisualizeTile(crop);
        targetTilemap.SetTile(position, plowed);
        if(isRaining){WaterCrop(position);};
    }

    public void CreateWateredTile(Vector3Int position)
    {
        GetComponent<Tilemap>().SetTile(position, watered);
    }
    
    internal void PickUp(Vector3Int gridPosition)
    {
        Vector2Int position = (Vector2Int) gridPosition;
        CropTile tile = container.Get(gridPosition);
        if(tile == null){return;}

        if(tile.Complete)
        {
            ItemSpawnManager.instance.SpawnItem(
                targetTilemap.CellToWorld(gridPosition),
                tile.crop.yield,
                tile.crop.count
            );
            AudioManager.instance.Play(OnPickUp);
            tile.Harvested();
            VisualizeTile(tile);
        }
    }

    private void VisualizeMap()
    {
        for(int i=0; i < container.crops.Count; i++)
        {
            VisualizeTile(container.crops[i]);
        }
    }

    public void VisualizeTile(CropTile cropTile)
    {
        
        targetTilemap.SetTile(cropTile.position, cropTile.watered ? watered : plowed);
        
        if(cropTile.renderer==null)
        {
            GameObject go= Instantiate(cropsSpritePrefab, transform);
            go.transform.position = targetTilemap.CellToWorld(cropTile.position);
            go.transform.position -= Vector3.forward * 0.01f;

            cropTile.renderer = go.GetComponent<SpriteRenderer>();
            cropTile.cropObject = go;
        }

        bool growing = cropTile.crop != null;
        cropTile.renderer.gameObject.SetActive(growing);

        if(growing==true)
        {
            cropTile.renderer.sprite = cropTile.crop.sprites[cropTile.growStage];

            GameObject marker = cropTile.cropObject.transform.GetChild(0).gameObject;
                    
            if(marker != null)
            {
                if(cropTile.damage >= cropTile.crop.damageToDead/2)
                {
                    marker.SetActive(true);
                    targetTilemap.SetTile(cropTile.position, plowed);
                    cropTile.watered = false;
                }
            }
        }
        
    }
}
