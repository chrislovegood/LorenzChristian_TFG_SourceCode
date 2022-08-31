using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreInteract : Interactable
{
    [SerializeField] GameObject shopPanel;
    [SerializeField] DisableControls controls;

    public override void Interact()
    {
        shopPanel.SetActive(true);
        controls.setEnableToolbar(false);
    }

    public override void finishInteraction()
    {
         shopPanel.SetActive(false);
         controls.setEnableToolbar(true);
    }

    public override void OnEndedInteraction()
    {
        finishInteraction();
    }
}
