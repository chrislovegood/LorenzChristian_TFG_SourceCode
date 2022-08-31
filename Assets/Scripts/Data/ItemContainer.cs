using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ItemSlot 
{
    public Item item;
    public int count;

    public void Copy(ItemSlot slot)
    {
        item = slot.item;
        count = slot.count;
    }

    public void Clear()
    {
        item = null;
        count = 0;
    }

    public void Set(Item item, int count)
    {
        this.item = item;
        this.count = count;
    }
}

[CreateAssetMenu(menuName = "Data/Item Container")]
public class ItemContainer : ScriptableObject
{
    public List<ItemSlot> slots;
    public bool isDirty;

    
    internal void Init()
    {
        slots = new List<ItemSlot>();
        for(int i = 0; i < 12; i++)
        {
            slots.Add(new ItemSlot());
        }
    }
    
    public void Add(Item item, int count = 1)
    {
        isDirty = true;

        if (item.stackable == true)
        {
            ItemSlot itemSlot = slots.Find(x => x.item == item);
            if (itemSlot != null)
            {
                itemSlot.count += count;
            }
            else 
            {
                itemSlot = slots.Find(x => x.item == null);
                if (itemSlot != null)
                {
                    itemSlot.item = item;
                    itemSlot.count = count;
                }
            }
        }
        else
        {
            //add non stackable item to the item container
            ItemSlot itemSlot = slots.Find(x => x.item == null);
            if (itemSlot != null)
            {
                itemSlot.item = item;
            }
        }
    }

    public void Remove(Item itemToRemove, int count = 1)
    {
        isDirty = true;

        if( itemToRemove.stackable)
        {
            ItemSlot itemSlot = slots.Find(x => x.item == itemToRemove);
            if( itemSlot == null ){ return; }
            itemSlot.count -= count;

            if(itemSlot.count == 0)
            {
                itemSlot.Clear();
            }
        }
        else 
        {
            while(count > 0)
            {
                count -= 1;
                ItemSlot itemSlot = slots.Find(x => x.item == itemToRemove);
                if( itemSlot == null ){ return; }
                itemSlot.Clear();
            }
        }
    }

    internal bool HasFreeSpace()
    {
        for(int i =0; i < slots.Count; i++)
        {
            if(slots[i].item==null)
            {
                return true;
            }
        }
        return false;
    }

    internal bool HasFreeSpaces(int numFreeSpaces)
    {
        int freeSpacesFound = 0;
        
        for(int i =0; i < slots.Count; i++)
        {
            if(slots[i].item==null)
            {
                freeSpacesFound++;
            }
            if(numFreeSpaces == freeSpacesFound){
                return true;
            }
        }
        return false;
    }

    internal bool CheckItem(ItemSlot checkingItem, int numItem = 1)
    {
        ItemSlot itemSlot = slots.Find(x => x.item == checkingItem.item);
        
        if(itemSlot == null){return false;}

        if(checkingItem.item.stackable){return itemSlot.count >= checkingItem.count * numItem;}

        return true;
    }

    internal bool HasItemToStack(ItemSlot checkingItem)
    {
        ItemSlot itemSlot = slots.Find(x => x.item == checkingItem.item);
        return itemSlot != null;
        return true;
    }
}
