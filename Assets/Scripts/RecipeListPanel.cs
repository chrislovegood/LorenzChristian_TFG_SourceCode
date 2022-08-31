using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeListPanel : ItemPanel
{
    [SerializeField] RecipeList recipeList;
    [SerializeField] RecipeListPanelController recipeDetailsPanel;
    [SerializeField] RecipeRequiredElement recipeElementPrefab;
    [SerializeField] Crafting crafting;
    [SerializeField] ItemContainer inventory;
    
    private List<ItemSlot> requiredElementsToCraft = new List<ItemSlot>();
    private CraftingRecipe activeCraftingRecipe;

    public void Awake() {
        recipeDetailsPanel.craftTenButton.onClick.AddListener(craftTen);
        recipeDetailsPanel.craftFiveButton.onClick.AddListener(craftFive);
        recipeDetailsPanel.craftOneButton.onClick.AddListener(craftOne);
        OnClick(0);
    }

    public override void Show()
    {
        for(int i = 0; i < buttons.Count && i < recipeList.recipes.Count; i++)
        {
            buttons[i].Set(recipeList.recipes[i].output);
        }
    }
    public override void OnClick(int id)
    {
        if(id >= recipeList.recipes.Count){return;}

        activeCraftingRecipe = recipeList.recipes[id];

        recipeDetailsPanel.recipeTitle.text = GameManager.instance.settings.dictionary[activeCraftingRecipe.output.item.Name]; 

        recipeDetailsPanel.recipeImage.sprite = activeCraftingRecipe.output.item.icon;

        foreach (Transform child in recipeDetailsPanel.recipeItemsPanel.transform) {
            GameObject.Destroy(child.gameObject);
        }
        
        requiredElementsToCraft.Clear();

        for(int i = 0; i < activeCraftingRecipe.elements.Count; i++)
        {
            requiredElementsToCraft.Add(activeCraftingRecipe.elements[i]);
        }

        for(int i = 0; i < requiredElementsToCraft.Count; i++)
        {
            RecipeRequiredElement recipeElement = Instantiate(recipeElementPrefab);
            recipeElement.transform.SetParent(recipeDetailsPanel.recipeItemsPanel,true);
            recipeElement.image.sprite = requiredElementsToCraft[i].item.icon;
            recipeElement.text.text = "x" + requiredElementsToCraft[i].count;
        }
        updateCraftButtonState();
    }

    private void updateCraftButtonState()
    {

        bool hasFreeSpace = true,  has5FreeSpaces = true,  has10FreeSpaces = true, canCraft10 = true, canCraft5 = true, canCraft1 = true;

        if(activeCraftingRecipe.output.item.stackable)
        {
            hasFreeSpace = inventory.HasFreeSpace() || inventory.HasItemToStack(activeCraftingRecipe.output);
            has5FreeSpaces = hasFreeSpace;
            has10FreeSpaces = hasFreeSpace;
        }else
        {
            has10FreeSpaces = inventory.HasFreeSpaces(10);

            if(!has10FreeSpaces){

                has5FreeSpaces = inventory.HasFreeSpaces(5);

                if(!has5FreeSpaces){

                    hasFreeSpace = inventory.HasFreeSpace();
                }
            }
        }

        for(int i =0; i < activeCraftingRecipe.elements.Count; i++)
        {
            if(inventory.CheckItem(activeCraftingRecipe.elements[i],10)==false)
            {  
                canCraft10 = false;
                break;
            }
        }

        if(!canCraft10)
        {
            for(int i =0; i < activeCraftingRecipe.elements.Count; i++)
            {
                if(inventory.CheckItem(activeCraftingRecipe.elements[i],5)==false)
                {  
                    canCraft5 = false;
                    break;
                }
            }
        
            if(!canCraft5)
            {
                for(int i =0; i <activeCraftingRecipe.elements.Count; i++)
                {
                    if(inventory.CheckItem(activeCraftingRecipe.elements[i])==false)
                    {  
                        canCraft1 = false;
                        break;
                    }
                }
            }
        }

        recipeDetailsPanel.craftTenButton.interactable = has10FreeSpaces && canCraft10;
        recipeDetailsPanel.craftFiveButton.interactable = has5FreeSpaces && canCraft5;
        recipeDetailsPanel.craftOneButton.interactable = hasFreeSpace && canCraft1; 
    }

    private void craftOne(){
		crafting.Craft(activeCraftingRecipe);
        updateCraftButtonState();
	}
    private void craftFive(){
		crafting.Craft(activeCraftingRecipe,5);
        updateCraftButtonState();
	}
    private void craftTen(){
		crafting.Craft(activeCraftingRecipe,10);
        updateCraftButtonState();
	}

}
