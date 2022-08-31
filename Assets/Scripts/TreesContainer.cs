using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public enum TypeTree {
    NormalTree,
    FruitTree,
    AncientTree
}

public enum Fruit {
    Apple,
    Orange,
    Pear
}

[Serializable]
public class Tree
{
    public bool growing;
    public int growingTime;
    public Vector3Int position;
    public TypeTree type;
    public Fruit fruit;
}

[CreateAssetMenu(menuName="Data/Trees Container")]
public class TreesContainer : ScriptableObject
{
    public List<Tree> trees;

    internal Tree Get(Vector3Int position)
    {
        return trees.Find(x => x.position == position);
    }
}
