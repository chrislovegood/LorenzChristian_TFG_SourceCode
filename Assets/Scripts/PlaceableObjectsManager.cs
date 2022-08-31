using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlaceableObjectsManager : MonoBehaviour
{
    [SerializeField] PlaceableObjectsContainer placeableObjects;
    [SerializeField] Tilemap targetTilemap;

    //FOR VALIDATIONS
    [SerializeField] TileMapReadController tilemapReader;
    [SerializeField] CropsContainer cropsContainer;
    [SerializeField] TreesContainer treesContainer;
    [SerializeField] ReservedCells reservedCells;

    private void Start()
    {
        GameManager.instance.GetComponent<PlaceableObjectsReferenceManager>().placeableObjectsManager = this;
        VisualizeMap();
    }

    private void OnDestroy()
    {
        for(int i = 0; i < placeableObjects.placeableObjects.Count; i++)
        {
            if(placeableObjects.placeableObjects[i].targetObject == null){continue;}

            IPersistant persistant = placeableObjects.placeableObjects[i].targetObject.GetComponent<IPersistant>();

            if(persistant != null)
            {
                string jsonString = persistant.Read();
                placeableObjects.placeableObjects[i].objectState = jsonString;
            }

            placeableObjects.placeableObjects[i].targetObject = null;
        }

        FileHandler.SavetoJSON<PlaceableObject>(placeableObjects.placeableObjects,"placeableObjects.json");
    }

    public void UpdateState()
    {
        for(int i = 0; i < placeableObjects.placeableObjects.Count; i++)
        {
            if(placeableObjects.placeableObjects[i].targetObject == null){continue;}

            IPersistant persistant = placeableObjects.placeableObjects[i].targetObject.GetComponent<IPersistant>();

            if(persistant != null)
            {
                string jsonString = persistant.Read();
                placeableObjects.placeableObjects[i].objectState = jsonString;
            }
        }
    }

    private void VisualizeMap()
    {
        for(int i = 0; i < placeableObjects.placeableObjects.Count; i++)
        {
           VisualizeItem(placeableObjects.placeableObjects[i]);
        }
    }

    private void VisualizeItem(PlaceableObject placeableObject)
    {
        GameObject go = Instantiate(placeableObject.placedItem.itemPrefab);
        go.transform.parent = transform;

        Vector3 position = targetTilemap.WorldToCell(placeableObject.positionOnGrid) + targetTilemap.cellSize/2;
        position -= Vector3.forward * 0.1f;
        go.transform.position = position;

        SpriteRenderer renderer = go.GetComponent<SpriteRenderer>();

        if(renderer != null)
        {
            renderer.sprite = placeableObject.placedItem.previewPlaced;
        }
        
        IPersistant persistant = go.GetComponent<IPersistant>();

        if(persistant != null)
        {
            persistant.Load(placeableObject.objectState);
        }

        placeableObject.targetObject = go.transform;

    }   
    
    public bool Check(Vector3Int position)
    {
        return placeableObjects.Get(position) != null || cropsContainer.Get(position) != null ||  treesContainer.Get(position) != null
                    || reservedCells.Get(position) || tilemapReader.GetTileOcean(position) != null;;
    }

    public void PickUp(Vector3Int positionOnGrid)
    {
        PlaceableObject placedObject = placeableObjects.Get(positionOnGrid);

        if(placedObject == null){return;}

        ItemSpawnManager.instance.SpawnItem(targetTilemap.CellToWorld(positionOnGrid), placedObject.placedItem, 1);

        if(placedObject.objectState != null && placedObject.objectState != "")
        {
            ToSave toLoad = JsonUtility.FromJson<ToSave>(placedObject.objectState);
            
            for(int i = 0; i < toLoad.itemDatas.Count; i++)
            {
                if(toLoad.itemDatas[i].itemId != -1)
                {
                    ItemSpawnManager.instance.SpawnItem(targetTilemap.CellToWorld(positionOnGrid), GameManager.instance.itemDB.items[toLoad.itemDatas[i].itemId], toLoad.itemDatas[i].count);
                }
            }
        }

        Destroy(placedObject.targetObject.gameObject);
        placeableObjects.Remove(placedObject);
    }

    public void Place(Item item, Vector3Int positionOnGrid)
    {
        if(Check(positionOnGrid)){return;}
        PlaceableObject placeableObject = new PlaceableObject(item, positionOnGrid);
        VisualizeItem(placeableObject);
        placeableObjects.placeableObjects.Add(placeableObject);
    }

    [Serializable]
    public class SaveLootItemData
    {
        public int itemId;
        public int count;

        public SaveLootItemData(int id, int c)
        {
            itemId= id;
            count = c;
        }
    }

    [Serializable]
    public class ToSave
    {
        public List<SaveLootItemData> itemDatas;

        public ToSave()
        {
            itemDatas = new List<SaveLootItemData>();
        }
    }
}
