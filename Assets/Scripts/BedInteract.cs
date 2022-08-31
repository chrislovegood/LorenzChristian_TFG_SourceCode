using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedInteract : Interactable
{
    public override void Interact()
    {
        Sleep sleep = GameManager.instance.player.GetComponent<Sleep>();
        sleep.DoSleep();
    }
}
