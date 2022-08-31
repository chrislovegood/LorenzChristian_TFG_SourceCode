using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Data/Reserved Cells")]
public class ReservedCells : ScriptableObject
{
    public List<Vector3Int> reservedCells;

    internal bool Get(Vector3Int position)
    {
        foreach(Vector3Int vector in reservedCells)
        {
            if(vector.Equals(position)){return true;}
        }
        return false;
    }
}
