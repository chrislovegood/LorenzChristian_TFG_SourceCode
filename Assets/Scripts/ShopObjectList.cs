using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Data/Shop Object List")]
public class ShopObjectList : ScriptableObject
{
    public List<ShopObject> objectsForSale;
}
