using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterWellInteract : Interactable
{

    [SerializeField] AudioClip onFillingWateringCan;

    public override void Interact()
    {
        AudioManager.instance.Play(onFillingWateringCan);
        GameManager.instance.player.GetComponent<WaterBarController>().Fill();
    }

}
