using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableControls : MonoBehaviour
{
    PlayerController playerController;
    ToolsPlayerController toolsController;
    InventoryController inventoryController;
    ToolbarController toolbarController;
    ItemContainerInteractController itemContainerInteractController;

    [SerializeField] GameObject iconPlaced;
    [SerializeField] GameObject marker;
    [SerializeField] GameObject waterBar;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        toolsController = GetComponent<ToolsPlayerController>();
        inventoryController = GetComponent<InventoryController>();
        toolbarController = GetComponent<ToolbarController>();
        itemContainerInteractController = GetComponent<ItemContainerInteractController>();
    }

    public void DisableControl()
    {
        playerController.setFrozen(1);
        toolsController.enabled = false;
        inventoryController.enabled = false;
        toolbarController.enabled = false;
        itemContainerInteractController.enabled = false;
        marker.SetActive(false);
    }

    public void EnableControl()
    {
        playerController.setFrozen(0);
        toolsController.enabled = true;
        inventoryController.enabled = true;
        toolbarController.enabled = true;
        itemContainerInteractController.enabled = true;
        marker.SetActive(true);
    }

    public void setEnableToolbar(bool enable)
    {
        if(toolbarController.GetItem != null && toolbarController.GetItem.name == "WateringCan"){
                waterBar.SetActive(enable);
        }
        
        toolsController.enabled = enable;
        toolbarController.enabled = enable;
        iconPlaced.SetActive(enable);
        marker.SetActive(enable);
    }
}
