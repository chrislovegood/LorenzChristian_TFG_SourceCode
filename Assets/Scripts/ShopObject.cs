using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Data/Shop Object")]
public class ShopObject : ScriptableObject
{
    public int price;
    public ItemSlot output;
}
