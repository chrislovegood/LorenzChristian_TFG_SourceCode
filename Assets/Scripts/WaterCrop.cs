using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Data/Tool Action/Water crop")]
public class WaterCrop : ToolAction
{
    [SerializeField] AudioClip effectSound;

    public override bool OnApplyToTileMap(Vector3Int gridPosition, TileMapReadController tileMapReadController, Item item)
    {
        WaterBarController waterBar = GameManager.instance.player.GetComponent<WaterBarController>();

        if(waterBar.GetUses() > 0)
        {  
            AudioManager.instance.Play(effectSound);
            waterBar.SpendUse();
        }else
        {
            return false;
        }

        if(tileMapReadController.cropsManager.Check(gridPosition) == false){
            return false;
        }
        
        if( tileMapReadController.cropsManager.isWatered(gridPosition) == true){
            return false;
        };

        tileMapReadController.cropsManager.WaterCrop(gridPosition);
        return true;
    }
}
