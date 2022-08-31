using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafting : MonoBehaviour
{
    [SerializeField] ItemContainer inventory;
    [SerializeField] GameObject craftingPanel;


    public void Show(bool show)
    {
        craftingPanel.SetActive(show);
        GetComponent<DisableControls>().setEnableToolbar(!show);
    }

    public void Craft(CraftingRecipe recipe, int numItem = 1)
    {
        for(int i =0; i < recipe.elements.Count; i++)
        {
            inventory.Remove(recipe.elements[i].item, recipe.elements[i].count * numItem);
        }

        if(recipe.output.item.stackable){
            inventory.Add(recipe.output.item, recipe.output.count * numItem);
        }else{
            for(int i = 0; i < numItem; i++){
                 inventory.Add(recipe.output.item, recipe.output.count);
            }
        }

    }
}
