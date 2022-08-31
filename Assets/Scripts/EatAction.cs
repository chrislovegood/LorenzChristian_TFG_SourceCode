using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Tool Action/Eat")]
public class EatAction : ToolAction
{

    [SerializeField] AudioClip OnEat;

    public override bool OnApply(Vector2 worldPoint)
    {
        return true;
    }

    public override void OnItemUsed(Item usedItem, ItemContainer inventory)
    {
        AudioManager.instance.Play(OnEat);
        inventory.Remove(usedItem);
    }
}
