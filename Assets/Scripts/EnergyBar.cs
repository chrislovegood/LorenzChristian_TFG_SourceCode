using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    Image image;
    int numStatesBar = 18;
    int currentValue; 
    int maxValue = 400; 
    int unit;
    
    [SerializeField] Sprite[] sprites;
    
    Sprite FULL_REST;
    Sprite EXHAUSTED;

    [SerializeField] Text energyString; 

    private void Start()
    {
        image =  gameObject.GetComponent<Image>();
        FULL_REST = sprites[18];
        EXHAUSTED = sprites[0];
        unit = maxValue/numStatesBar;
    }

    public void Set(int currentVal){
        currentValue = currentVal;
        UpdateBar();
    }

    public void SetEnergyString()
    {
        energyString.text = currentValue.ToString() + "/" + maxValue.ToString();
    }

    public void UpdateBar(){
        if(maxValue != 0)
        {
            if(currentValue == 0){image.sprite = EXHAUSTED;}
            if(currentValue == maxValue){image.sprite = FULL_REST;}
            else if(currentValue <= unit * 17 && currentValue > unit * 16){image.sprite = sprites[17];}
            else if(currentValue <= unit * 16 && currentValue > unit * 15){image.sprite = sprites[16];}           
            else if(currentValue <= unit * 15 && currentValue > unit * 14){image.sprite = sprites[15];}    
            else if(currentValue <= unit * 14 && currentValue > unit * 13){image.sprite = sprites[14];}    
            else if(currentValue <= unit * 13 && currentValue > unit * 12){image.sprite = sprites[13];}    
            else if(currentValue <= unit * 12 && currentValue > unit * 11){image.sprite = sprites[12];}    
            else if(currentValue <= unit * 11 && currentValue > unit * 10){image.sprite = sprites[11];}    
            else if(currentValue <= unit * 10 && currentValue > unit * 9){image.sprite = sprites[10];}    
            else if(currentValue <= unit * 9 && currentValue > unit * 8){image.sprite = sprites[9];}    
            else if(currentValue <= unit * 8 && currentValue > unit * 7){image.sprite = sprites[8];}    
            else if(currentValue <= unit * 7 && currentValue > unit * 6){image.sprite = sprites[7];}    
            else if(currentValue <= unit * 6 && currentValue > unit * 5){image.sprite = sprites[6];}    
            else if(currentValue <= unit * 5 && currentValue > unit * 4){image.sprite = sprites[5];}    
            else if(currentValue <= unit * 4 && currentValue > unit * 3){image.sprite = sprites[4];}    
            else if(currentValue <= unit * 3 && currentValue > unit * 2){image.sprite = sprites[3];}    
            else if(currentValue <= unit * 2 && currentValue > unit * 1){image.sprite = sprites[2];}    
            else if(currentValue <= unit * 1 && currentValue > unit * 0){image.sprite = sprites[1];}     
        } 
        SetEnergyString();

    }
}
