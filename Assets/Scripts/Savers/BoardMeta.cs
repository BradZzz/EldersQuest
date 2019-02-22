using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class BoardMeta
{
    public int height;
    public int width;

    public Unit[] enemies;
    public CondMeta[] conditions;

    public string[] unlocks;

    public BoardMeta() { 
        height = 10;
        width = 10;
        enemies = new Unit[]{ 
            new Unit(),
            new Unit(),
            new Unit()
        };
        conditions = new CondMeta[]{ 
            new CondMeta()
        };
        unlocks = new string[0];
    }

    public BoardMeta(int height, int width, Unit[] enemies, CondMeta[] conditions, string[] unlocks)
    {
        this.height = height;
        this.width = width;
        this.enemies = enemies;
        this.conditions = conditions;
        this.unlocks = unlocks;
    }

    public string ReturnMapDesc(){
        string retString = "";
        int lvl = enemies.Sum(emy => emy.GetLvl());
        retString += "pwr lvl: " + lvl.ToString();
        return retString;
    }
}
