using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject player;
    public ItemContainer inventoryContainer;
    public ItemDragAndDropController dragAndDropController;
    public DayTimeController timeController;
    public PlaceableObjectsReferenceManager placeableObjects;
    public ItemList itemDB;
    public ScreenTint screenTint;
    public Settings settings;
    public ItemContainer inventory;
    public GameDataList gameData;

    private void Awake()
    {
        instance = this;
        if(settings.dictionary == null){settings.setLanguage(settings.currentLanguage);}
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {   
            inventory.Add(itemDB.items[41],10);
        }
         if(Input.GetKeyDown(KeyCode.P))
        {   
            inventory.Add(itemDB.items[32],10);
        }
    }

    private void OnDestroy()
    {
        List<ItemSlot> ToSave = new List<ItemSlot>();
      
        for(int i = 0; i < inventory.slots.Count; i++)
        {
            ToSave.Add(inventory.slots[i]);
        }

        FileHandler.SavetoJSON<ItemSlot>(ToSave,"inventory.json");
        FileHandler.SavetoJSON<GameData>(gameData.entries,"gameData.json");
   
    }
}
