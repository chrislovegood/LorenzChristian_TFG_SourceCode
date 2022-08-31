using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[Serializable]
public class CropTile
{
    public int growTimer;
    public int growStage;
    public Crop crop;
    public SpriteRenderer renderer;
    public GameObject cropObject;
    public int damage = 0;
    public bool watered;
    public Vector3Int position;
    public int waterlessTimeToLive = 96 * 2;
    
    public bool Complete
    {
        get
        {
            if(crop==null){return false;}
            return growTimer >= crop.timeToGrow;
        }
    }

    internal void Harvested()
    {
        growTimer = 0;
        growStage = 0;
        crop = null;
        renderer.gameObject.SetActive(false);
        damage = 0;
    }

}

public class CropsManager : MonoBehaviour
{
    public TileMapCropsManager cropsManager;

    public void PickUp(Vector3Int position)
    {
        if(cropsManager==null)
        {
            Debug.Log("No tilemap crops manager are referenced in the crops manager");
            return;
        }
        cropsManager.PickUp(position);
    }

    public bool Check(Vector3Int position)
    {
        if(cropsManager==null)
        {
            Debug.Log("No tilemap crops manager are referenced in the crops manager");
            return false;
        }
        return cropsManager.Check(position);
    }

    public bool isCropGrowing(Vector3Int position)
    {
        if(cropsManager==null)
        {
            Debug.Log("No tilemap crops manager are referenced in the crops manager");
            return false;
        }
        return cropsManager.isCropGrowing(position);
    }

    public bool isWatered(Vector3Int position)
    {
        if(cropsManager==null)
        {
            Debug.Log("No tilemap crops manager are referenced in the crops manager");
            return false;
        }
        return cropsManager.isWatered(position);
    }

    public void Seeded(Vector3Int position, Crop toSeed)
    {
        if(cropsManager==null)
        {
            Debug.Log("No tilemap crops manager are referenced in the crops manager");
            return;
        }
        cropsManager.Seeded(position, toSeed);
    }

    public void Plow(Vector3Int position)
    {
        if(cropsManager==null)
        {
            Debug.Log("No tilemap crops manager are referenced in the crops manager");
            return;
        }
        cropsManager.Plow(position);
    }

    public void WaterCrop(Vector3Int position)
    {
        if(cropsManager==null)
        {
            Debug.Log("No tilemap crops manager are referenced in the crops manager");
            return;
        }
        cropsManager.WaterCrop(position);
    }


}
