using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ToolsPlayerController : MonoBehaviour
{
    PlayerController playerController;
    Player player;
    Rigidbody2D rigidbody2d;
    ToolbarController toolbarController;
    Animator animator;

    [SerializeField] float offsetDistance = 1f;
    [SerializeField] float sizeOfInteractableArea = 1.2f;
    [SerializeField] MarkerManager markerManager;
    [SerializeField] TileMapReadController tileMapReadController;
    [SerializeField] float maxDistance = 1.5f;
    [SerializeField] ToolAction onTilePickUp;
    [SerializeField] IconHighlight iconHighlight;

    Vector3Int selectedTilePosition;
    Vector3Int tilePosition;
    bool selectable;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        toolbarController = GetComponent<ToolbarController>();
        animator = GetComponent<Animator>();
        player = GetComponent<Player>();
    }

    private void Update()
    {            
        SelectTile();
        CanSelectCheck();
        Marker();

        if(Input.GetMouseButtonDown(0))
        {
                tilePosition = tileMapReadController.GetGridPosition(Input.mousePosition, true);
                Item item = toolbarController.GetItem;
                if( item !=null ){
                        switch(item.name)
                        {
                                case "Axe": 
                                        animator.SetTrigger("cut");
                                break;
                                case "Plow": 
                                        animator.SetTrigger("plow");
                                break;
                                case "WateringCan": 
                                        animator.SetTrigger("water");
                                break;
                                default:
                                        processUseTool();
                                break;
                        }
                }else{
                        processUseTool();
                }
        }
    }

    private void processUseTool()
    {
        if(UseToolWorld() ==  true)
        {
                return;
        }
        UseToolGrid();
    }

    private void SelectTile()
    {
        selectedTilePosition = tileMapReadController.GetGridPosition(Input.mousePosition, true);
    }

    void CanSelectCheck()
    {
        Vector2 characterPosition = transform.position;
        Vector3 cameraPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        selectable = Vector2.Distance(characterPosition, cameraPosition) < maxDistance;
        markerManager.Show(selectable);
        iconHighlight.CanSelect = selectable;
    }

    private void Marker()
    {
        markerManager.markedCellPosition = selectedTilePosition;
        iconHighlight.cellPosition = selectedTilePosition;
    }

    private bool UseToolWorld()
    {
        Vector2 position = rigidbody2d.position + playerController.lastMotionVector * offsetDistance;

        Item item = toolbarController.GetItem;

        if( item == null ){ return false; }
        if( item.onAction == null ){ return false; }
        
        bool complete = item.onAction.OnApply(position);

        if(complete == true)
        {
                if(item.onAction.energyCost != 0){ EnergyCost(item.onAction.energyCost);}

                if(player.isFullRest()){ return false; }
                else if(item.energyGained != 0){ EnergyGained(item.energyGained);}

                if( item.onItemUsed != null )
                {
                        item.onItemUsed.OnItemUsed(item,GameManager.instance.inventoryContainer);
                }   
        }

        return complete;
    }

    private void UseToolGrid()
    {
        if(selectable == true)
        {
        Item item = toolbarController.GetItem;
        if( item == null ){ 
                PickUpTile();
                return; 
        }
        if( item.onTileMapAction == null ){ return; }

        bool complete = item.onTileMapAction.OnApplyToTileMap(tilePosition, tileMapReadController, item);

        if(complete == true)
        {
                EnergyCost(item.onTileMapAction.energyCost);

                if( item.onItemUsed != null )
                {
                        item.onItemUsed.OnItemUsed(item,GameManager.instance.inventoryContainer);
                }   
        }
        }
    }

    private void PickUpTile()
    {
        if(onTilePickUp == null){return;}
        onTilePickUp.OnApplyToTileMap(selectedTilePosition, tileMapReadController,null);
    }

    private void EnergyCost(int energyCost)
    {
        player.GetTired(energyCost);
    }

    private void EnergyGained(int energyGained)
    {
        player.Rest(energyGained);
    }

}