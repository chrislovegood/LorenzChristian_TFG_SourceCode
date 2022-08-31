using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trading : MonoBehaviour
{
    [SerializeField] ItemContainer inventory;
    [SerializeField] Currency currency;
    [SerializeField] AudioClip audio;
    
    public bool Check(ShopObject shopObject, int numItem = 1){
        return currency.getAmount() >= shopObject.price * numItem;
    }


    public void Buy(ShopObject shopObject, int numItem = 1){
        AudioManager.instance.Play(audio);
        currency.Spend(shopObject.price * numItem);

        if(shopObject.output.item.stackable){
            inventory.Add(shopObject.output.item, shopObject.output.count * numItem);
        }else{
            for(int i = 0; i < numItem; i++){
                 inventory.Add(shopObject.output.item, shopObject.output.count);
            }
        }

    }

    public void Sell(ItemSlot itemSlot){
        AudioManager.instance.Play(audio);
        currency.Earn(itemSlot.item.sellPrice * itemSlot.count);
    }
}
