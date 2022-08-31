using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanel : ItemPanel
{
    [SerializeField] ShopObjectList shopList;
    [SerializeField] ShopDetailPanelController shopDetail;
    [SerializeField] ItemContainer inventory;
    [SerializeField] Trading trading;

    private ShopObject activeShopObject;

    public void Awake() {
        shopDetail.buyTenButton.onClick.AddListener(buyTen);
        shopDetail.buyFiveButton.onClick.AddListener(buyFive);
        shopDetail.buyOneButton.onClick.AddListener(buyOne);
        OnClick(0);
    }

    public override void Show()
    {
        for(int i = 0; i < buttons.Count && i < shopList.objectsForSale.Count; i++)
        {
            buttons[i].Set(shopList.objectsForSale[i].output);
        }
    }
    public override void OnClick(int id) {

        if(id >= shopList.objectsForSale.Count){return;}

        activeShopObject = shopList.objectsForSale[id];

        shopDetail.title.text = GameManager.instance.settings.dictionary[activeShopObject.output.item.Name];
        shopDetail.image.sprite = activeShopObject.output.item.icon;
        shopDetail.price.text = activeShopObject.price + "";

        updateBuyButtonState();
    }

    private void updateBuyButtonState()
    {
        bool hasFreeSpace = true,  has5FreeSpaces = true,  has10FreeSpaces = true;
        bool canBuy10 = true, canBuy5 = true, canBuy1 = true;

        if(activeShopObject.output.item.stackable)
        {
            hasFreeSpace = inventory.HasFreeSpace() || inventory.HasItemToStack(activeShopObject.output);
            has5FreeSpaces = hasFreeSpace;
            has10FreeSpaces = hasFreeSpace;
        }
        else
        {
            has10FreeSpaces = inventory.HasFreeSpaces(10);

            if(!has10FreeSpaces)
            {
                has5FreeSpaces = inventory.HasFreeSpaces(5);

                if(!has5FreeSpaces){

                    hasFreeSpace = inventory.HasFreeSpace();
                }
            }
        }

        canBuy10 = trading.Check(activeShopObject,10);

        if(!canBuy10)
        {
            canBuy5 = trading.Check(activeShopObject,5);
            if(!canBuy5)
            {
                canBuy1 = trading.Check(activeShopObject);
            }
        }

        shopDetail.buyTenButton.interactable = has10FreeSpaces && canBuy10;
        shopDetail.buyFiveButton.interactable = has5FreeSpaces && canBuy5;
        shopDetail.buyOneButton.interactable = hasFreeSpace && canBuy1; 
    }

    private void buyOne(){
		trading.Buy(activeShopObject);
        updateBuyButtonState();
	}
    private void buyFive(){
		trading.Buy(activeShopObject,5);
        updateBuyButtonState();
	}
    private void buyTen(){
		trading.Buy(activeShopObject,10);
        updateBuyButtonState();
	}

}
