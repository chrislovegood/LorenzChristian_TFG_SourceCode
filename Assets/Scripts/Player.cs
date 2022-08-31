using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Stat
{
    public int maxVal;
    public int currentVal;

    public Stat(int max, int curr)
    {
        maxVal = max;
        currentVal = curr;
    }

    internal void Subtract(int amount)
    {
        currentVal -= amount;
        if(currentVal < 0){currentVal = 0;}
    }   
    internal void Add(int amount)
    {
        currentVal += amount;
        if(currentVal > maxVal){currentVal = maxVal;}
    }   
    internal void SetToMax()
    {
        currentVal = maxVal;
    }   
}

public class Player : MonoBehaviour
{
    [SerializeField] EnergyBar energyBar;

    private Stat energy;

    private void Start()
    {
        energy = new Stat(400,(int)  GameManager.instance.gameData.entries[1].value);
        energyBar.Set(energy.currentVal);
    }

    public void GetTired(int amount)
    {
        energy.Subtract(amount);

        if(energy.currentVal == 0)
        {
            Exhausted();
        }
        GameManager.instance.gameData.entries[1].value = energy.currentVal;
        energyBar.Set(energy.currentVal);
    }

    public void Rest(int amount)
    {
        energy.Add(amount);
        GameManager.instance.gameData.entries[1].value = energy.currentVal;
        energyBar.Set(energy.currentVal);
    }

    public void FullRest()
    {
        energy.SetToMax();
        GameManager.instance.gameData.entries[1].value = energy.currentVal;
        energyBar.Set(energy.currentVal);
    }

    private void Exhausted()
    {
        Sleep sleep = GetComponent<Sleep>();
        sleep.DoSleep();
        Currency currency = GetComponent<Currency>();
        currency.Spend(currency.getAmount()/2);
    }

    public bool isFullRest()
    {
        return energy.currentVal == energy.maxVal;
    }

}

