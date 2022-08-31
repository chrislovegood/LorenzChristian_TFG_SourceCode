using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemContainerPanel : ItemPanel
{
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
        GameManager.instance.dragAndDropController.OnClick(storage.slots[id]);
        Show();
   }


   public override void Show()
   {
      for(int i = 0; i < storage.slots.Count && i < buttons.Count; i++)
      {
         if(storage.slots[i].item == null)
         {
               buttons[i].Clean();
         }
         else 
         {
               buttons[i].Set(storage.slots[i]);
         }
      }
   }  
}
