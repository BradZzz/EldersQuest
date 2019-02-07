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

    public BoardMeta() { 
        this.height = 10;
        this.width = 10;
        this.enemies = new CharMeta[]{ 
            new CharMeta(),
            new CharMeta(),
            new CharMeta()
        };
        this.conditions = new CondMeta[]{ 
            new CondMeta()
        };
    }

    public BoardMeta(int height, int width, CharMeta[] enemies, CondMeta[] conditions)
    {
        this.height = height;
        this.width = width;
        this.enemies = enemies;
        this.conditions = conditions;
    }
}
