using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Experimental.Rendering.Universal;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] Settings settings;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject optionsMenu;
    [SerializeField] GameObject confirmAlert;

    [SerializeField] Button continueGameButton;

    [SerializeField] Text newGameText;
    [SerializeField] Text continueGameText;
    [SerializeField] Text optionsText;
    [SerializeField] Text exitText;
    [SerializeField] Text confirmAlertText;

    [SerializeField] CropsContainer crops;
    [SerializeField] PlaceableObjectsContainer placeableObjects;
    [SerializeField] TreesContainer trees;
    [SerializeField] ItemContainer inventory;
    [SerializeField] GameDataList gameData;
    [SerializeField] ItemList itemDB;
    
    [SerializeField] Light2D globalLight;
    [SerializeField] Color nightLightColor;
    [SerializeField] Color dayLightColor = Color.white;
    
    private string currentFrom;

    void Awake()
    {
        if(UnityEngine.Random.value < 0.5f)
        {
             globalLight.color = nightLightColor;
        }
        else
        {
            globalLight.color = dayLightColor;
        }
       
        List<GameData> data =   FileHandler.ReadFromJSON<GameData>("gameData.json");

        if(data.Count != 0)
        {
            settings.setLanguage(data[6].value == 1 ? Languages.ES : Languages.EN);
            settings.hasSave = data[7].value == 1;
        }
        else
        {
            settings.setLanguage(Languages.ES);
            settings.hasSave = false;
        }
    
        continueGameButton.interactable = settings.hasSave;
        Rebuild();
    }


    public void buildConfirmAlert(string from)
    {
        currentFrom = from;
        switch(from)
        {
            case "new_game":

                if(!settings.hasSave){submitConfirmAlert();return;}
                else{confirmAlertText.text = settings.dictionary["new_game_alert"];}
                
                break;
            case "exit":

                confirmAlertText.text = settings.dictionary["exit_alert"];
                break;

            default:
                break;
        }
        this.showConfirmAlert(true);
    }

    public void submitConfirmAlert()
    {
        switch(currentFrom)
        {
            case "new_game":
                StartNewGame();
                break;
            case "exit":
                ExitGame();
                break;
            default:
                break;
        }
    }

    public void showConfirmAlert(bool show)
    {
       mainMenu.SetActive(!show);
       confirmAlert.SetActive(show);
    }
    
    public void showOptionsMenu(bool show)
    {
       mainMenu.SetActive(!show);
       optionsMenu.SetActive(show);
    }

    public void setLanguage(string language)
    {
       switch(language)
        {
            case "en":
                settings.setLanguage(Languages.EN);
                gameData.entries[6].value  =  0;
                
                break;
            case "es": 
                settings.setLanguage(Languages.ES);
                gameData.entries[6].value  =  1;
                break;
        }
        FileHandler.SavetoJSON<GameData>(gameData.entries,"gameData.json");
        Rebuild();
        showOptionsMenu(false);
    }

    public void ContinueGame()
    {
        placeableObjects.placeableObjects   =   FileHandler.ReadFromJSON<PlaceableObject>("placeableObjects.json");
        crops.crops                         =   FileHandler.ReadFromJSON<CropTile>("crops.json");
        inventory.slots                     =   FileHandler.ReadFromJSON<ItemSlot>("inventory.json");
        trees.trees                         =   FileHandler.ReadFromJSON<Tree>("trees.json");

        List<GameData> data                 =   FileHandler.ReadFromJSON<GameData>("gameData.json");
        gameData.entries[0].value           =   (int) data[0].value;
        gameData.entries[1].value           =   (int) data[1].value;
        gameData.entries[2].value           =   data[2].value;
        gameData.entries[3].value           =   (int) data[3].value;
        gameData.entries[4].value           =   (int) data[4].value;
        gameData.entries[5].value           =   (int) data[5].value;

        StartGame();
    }

    public void StartNewGame()
    {
        crops.crops = new List<CropTile>();
        placeableObjects.placeableObjects = new List<PlaceableObject>();

        for(int i = 0; i < inventory.slots.Count; i++)
        {
            inventory.slots[i].Clear();
        }

        inventory.Add(itemDB.items[1],1);
        inventory.Add(itemDB.items[22],1);
        inventory.Add(itemDB.items[37],1);
        inventory.Add(itemDB.items[42],1);
        inventory.Add(itemDB.items[2],1);

        for(int i = 0; i < trees.trees.Count; i++)
        {
            trees.trees[i].growing = false;
            trees.trees[i].growingTime = 0;
        }
        
        gameData.entries[0].value = (int) 500;
        gameData.entries[1].value = (int) 400;
        gameData.entries[2].value = 28800f;
        gameData.entries[3].value = (int) 1;
        gameData.entries[4].value = (int) 5;
        gameData.entries[5].value = (int) 0;
        gameData.entries[7].value = (int) 1;

        StartGame();
    }

    private void StartGame()
    {
        settings.hasSave = true;
        gameData.entries[7].value = 1;
        SceneManager.LoadScene("OverworldScene", LoadSceneMode.Single);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private void Rebuild()
    {
        newGameText.text = settings.dictionary["new_game"];
        continueGameText.text = settings.dictionary["continue_game"];
        optionsText.text = settings.dictionary["options"];
        exitText.text = settings.dictionary["exit"];
    }
}
