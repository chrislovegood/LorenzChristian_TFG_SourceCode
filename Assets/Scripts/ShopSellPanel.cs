using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSellPanel : ItemPanel
{

    [SerializeField] Text price;
    [SerializeField] Button sellPrice;
    [SerializeField] Trading trading;
    ItemSlot itemSlot;

    private void Awake()
    {
        sellPrice.onClick.AddListener(Sell);
    }

    private void LateUpdate()
    {
        if(storage.isDirty)
        {
                Show();
                storage.isDirty = false;
        }
    }

   public override void OnClick(int id)
   {
        itemSlot = storage.slots[id];

        GameManager.instance.dragAndDropController.OnClick(itemSlot);

        Show();

        if(itemSlot != null)
        {
            if(itemSlot.count == 0)
            {
                itemSlot.count = 1;
            }
            price.text = "+" + itemSlot.item.sellPrice * itemSlot.count;
        }
         
   }


   public override void Show()
   {
      for(int i = 0; i < storage.slots.Count && i < buttons.Count; i++)
      {
         if(storage.slots[i].item == null)
         {
                buttons[i].Clean();
                itemSlot = null;
                price.text = "+" + 0;
         }
         else 
         {
                buttons[i].Set(storage.slots[i]);
                itemSlot = storage.slots[i];
                if(itemSlot.count == 0)
                {
                    itemSlot.count = 1;
                }
                price.text = "+" + itemSlot.item.sellPrice * itemSlot.count;
         }
      }

      sellPrice.interactable = itemSlot != null && itemSlot.item.sellPrice != 0;
   }

   public void Sell()
   {
        trading.Sell(itemSlot);
        storage.Remove(itemSlot.item, itemSlot.count);
        Show();
        price.text = "+" + 0;
        sellPrice.interactable = false;
   }
}
