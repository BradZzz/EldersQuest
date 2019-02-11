using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
/*
 * TODO: Build out the abstract class
 */
public class CondMeta
{
    public string meta = "All"; 
    public condition cond = condition.Enemy;

    /*
     * Condition:
     * Enemy: Defeat an enemy 
     * Item: Collect an item
     * Tile: Land on a tile
     * Turns: Last for a certain number of turns
     */
    public enum condition
    {
        Enemy, Item, Tile, Turns, None
    }
}
