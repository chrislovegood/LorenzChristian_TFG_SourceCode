using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkStationInteract : Interactable
{
    public override void Interact()
    {
        Crafting crafting = GameManager.instance.player.GetComponent<Crafting>();
        crafting.Show(true);
    }

    public override void finishInteraction()
    {
        Crafting crafting = GameManager.instance.player.GetComponent<Crafting>();
        crafting.Show(false);
    }

    public override void OnEndedInteraction()
    {
        finishInteraction();
    }
}
