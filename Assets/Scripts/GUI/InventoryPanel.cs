using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanel : ItemPanel
{
   public ItemContainer inventory;

   private void LateUpdate()
   {
      if(inventory.isDirty)
      {
            Show();
            inventory.isDirty = false;
      }
   }

   public override void OnClick(int id)
   {
        GameManager.instance.dragAndDropController.OnClick(inventory.slots[id]);
        Show();
   }

   public override void Show()
   {
      for(int i = 0; i < inventory.slots.Count && i < buttons.Count; i++)
      {
         if(inventory.slots[i].item == null)
         {
               buttons[i].Clean();
         }
         else 
         {
               buttons[i].Set(inventory.slots[i]);
         }
      }
   }
   
}
