using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] GameObject toolbarPanel;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
           if(panel.activeInHierarchy == false)
           {
                Open();
           }
           else
           {
                Close();
           }
        }
    }

    public void Open()
    {
        GetComponent<DisableControls>().setEnableToolbar(false);
        panel.SetActive(true);
        toolbarPanel.SetActive(false);
        
    }

    public void Close()
    {
        GetComponent<DisableControls>().setEnableToolbar(true);
        panel.SetActive(false);
        toolbarPanel.SetActive(true);
    }
}
