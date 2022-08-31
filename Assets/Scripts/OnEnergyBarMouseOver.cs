using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; 

public class OnEnergyBarMouseOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GameObject energyString; 

    public void OnPointerEnter(PointerEventData eventData)
    {
        energyString.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        energyString.SetActive(false);
    }
}

