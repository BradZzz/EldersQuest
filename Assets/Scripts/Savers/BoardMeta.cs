using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BoardMeta
{
    public int height;
    public int width;

    public CharMeta[] enemies;
    public CondMeta[] conditions;

    public string[] unlocks;

    public BoardMeta() { 
        height = 10;
        width = 10;
        enemies = new CharMeta[]{ 
            new CharMeta(),
            new CharMeta(),
            new CharMeta()
        };
        conditions = new CondMeta[]{ 
            new CondMeta()
        };
        unlocks = new string[0];
    }

    public BoardMeta(int height, int width, CharMeta[] enemies, CondMeta[] conditions, string[] unlocks)
    {
        this.height = height;
        this.width = width;
        this.enemies = enemies;
        this.conditions = conditions;
        this.unlocks = unlocks;
    }
}
