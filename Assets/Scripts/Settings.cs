using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Languages
{
    ES,
    EN
}

[CreateAssetMenu(menuName="Settings")]
public class Settings : ScriptableObject
{

    static private Dictionary<string, string> languageES = new Dictionary<string, string>{
        //MENU DE INICIO
        ["new_game"] = "NUEVA PARTIDA",
        ["new_game_alert"] = "¿Quieres empezar una nueva partida? Todo tu progeso se perderá",
        ["continue_game"] = "CONTINUAR",
        ["options"] = "OPCIONES",
        ["exit"] = "SALIR",
        ["exit_alert"] = "¿Quieres salir al escritorio?",

        //OVERWORLD
        ["day"] = "Día",

        //SHOP
        ["cauliflower_seed"] = "Semilla de coliflor",
        ["carrot_seed"] = "Semilla de zanahoria",
        ["cucumber_seed"] = "Semilla de pepino",
        ["eggplant_seed"] = "Semilla de berenjena",
        ["lettuce_seed"] = "Semilla de lechuga",
        ["pumpkin_seed"] = "Semilla de calabaza",
        ["radish_seed"] = "Semilla de rábano",
        ["starfruit_seed"] = "Semilla de estrella",
        ["tomatoe_seed"] = "Semilla de tomate",
        ["wheat_seed"] = "Semilla de trigo",
        ["stone"] = "Piedra",


        //RECIPES
        ["bed"] = "Cama",
        ["chest"] = "Cofre",
        ["workstation"] = "Mesa de trabajo",
        ["plank"] = "Tabla",
        ["carrot_sign"] = "Señal de zanahoría",
        ["cauliflower_sign"] = "Señal de coliflor",
        ["cucumber_sign"] = "Señal de pepino",
        ["eggplant_sign"] = "Señal de berenjena",
        ["lettuce_sign"] = "Señal de lechuga",
        ["pumpkin_sign"] = "Señal de calabaza",
        ["radish_sign"] = "Señal de rábano",
        ["starfruit_sign"] = "Señal de estrella",
        ["tomatoe_sign"] = "Señal de tomate",
        ["wheat_sign"] = "Señal de trigo",
        ["stone_brick"] = "Ladrillo de piedra",


    };
    
    static private Dictionary<string, string> languageEN = new Dictionary<string, string>{
        //MENU DE INICIO
        ["new_game"] = "NEW GAME",
        ["new_game_alert"] = "Do you want to start a new game? All your progress will be lost",
        ["continue_game"] = "CONTINUE GAME",
        ["options"] = "OPTIONS",
        ["exit"] = "EXIT",
        ["exit_alert"] = "Do you want to go to the desktop?",

        //OVERWORLD
        ["day"] = "Day",

        //SHOP
        ["cauliflower_seed"] = "Cauliflower seed",
        ["carrot_seed"] = "Carrot seed",
        ["cucumber_seed"] = "Cucumber seed",
        ["eggplant_seed"] = "Eggplant seed",
        ["lettuce_seed"] = "Lettuce seed",
        ["pumpkin_seed"] = "Pumpkin seed",
        ["radish_seed"] = "Radish seed",
        ["starfruit_seed"] = "Starfruit seed",
        ["tomatoe_seed"] = "Tomatoe seed",
        ["wheat_seed"] = "Wheat seed",
        ["stone"] = "Stone",

        //RECIPES
        ["bed"] = "Bed",
        ["chest"] = "Chest",
        ["workstation"] = "Workstation",
        ["plank"] = "Plank",
        ["carrot_sign"] = "Carrot sign",
        ["cauliflower_sign"] = "Cauliflower sign",
        ["cucumber_sign"] = "Cucumber sign",
        ["eggplant_sign"] = "Eggplant sign",
        ["lettuce_sign"] = "Lettuce sign",
        ["pumpkin_sign"] = "Pumpkin sign",
        ["radish_sign"] = "Radish sign",
        ["starfruit_sign"] = "Starfruit seed sign",
        ["tomatoe_sign"] = "Tomatoe sign",
        ["wheat_sign"] = "Wheat sign",
        ["stone_brick"] = "Stone brick",
    };
  
    public Dictionary<string, string> dictionary;

    public Languages currentLanguage;
    
    public bool hasSave;

    public void setLanguage(Languages language = Languages.EN)
    {
        switch(language)
        {
            case Languages.EN:
                dictionary = languageEN;
                currentLanguage = Languages.EN;
                break;
            case Languages.ES: 
                dictionary = languageES;
                currentLanguage = Languages.ES;
                break;
        }
    }
}
