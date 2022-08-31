using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData {
    public string name;
    public float value;

    public GameData(string n, float val)
    {
        name = n;
        value = val;
    }
}
[CreateAssetMenu(menuName="Game Data List")]
public class GameDataList : ScriptableObject
{
    public List<GameData> entries = new List<GameData>();
}
