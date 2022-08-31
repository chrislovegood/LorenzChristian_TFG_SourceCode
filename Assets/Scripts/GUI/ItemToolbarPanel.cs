using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemToolbarPanel : ItemPanel
{
    public ItemContainer inventory;
    [SerializeField] ToolbarController toolbarController;

    private void Start()
    {
        Init();
        toolbarController.onChange += Hightlight;
        Hightlight(0);
    }

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
        toolbarController.Set(id);
        Hightlight(id);
    }

    int currentSelectedTool;

    public void Hightlight(int id)
    {
         buttons[currentSelectedTool].Hightlight(false);
         currentSelectedTool = id;
         buttons[currentSelectedTool].Hightlight(true);
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
      toolbarController.UpdateHightlightIcon();
   }
}
