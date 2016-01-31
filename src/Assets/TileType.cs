using UnityEngine;
using System.Collections;

[System.Serializable]
public class TileType
{
    string name;
    public GameObject tilePrefabs;
    public bool isWalkable;
    public int movementCost = 1;
}

