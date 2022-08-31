using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Currency : MonoBehaviour
{
    int amount;
    [SerializeField] Text text;

    void Start()
    {
        amount = (int) GameManager.instance.gameData.entries[0].value;
        UpdateText();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {   
            Earn(500);
        }
    }

    private void UpdateText()
    {
        text.text = amount.ToString();
    }

    public void Earn(int currencies)
    {
        amount += currencies;
        GameManager.instance.gameData.entries[0].value = amount;
        UpdateText();
    }

    public void Spend(int currencies)
    {
        amount -= currencies;
        if(amount == 0){amount = 0;}

        GameManager.instance.gameData.entries[0].value = amount;
        UpdateText();
    }

    public int getAmount() {
        return amount;
    }
}
