using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Tile
{
    public Tile(int x, int y)
    {
        position = new Vector3Int(x, y, 0);
    }
    public Vector3Int position;
    public List<GridObject> objects;
}
