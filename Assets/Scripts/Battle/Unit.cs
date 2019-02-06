using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Unit : GridObject
{
    public string characterName;
    public string characterMoniker;

    public int mxHlth = 1;

    private int cHlth = 1;//default values

    private int moveSpeed = 3;

    public Unit()
    {
        this.characterName = "0";
        this.characterMoniker = "Null";
        this.mxHlth = 1;
        this.moveSpeed = 1;
    }

    public Unit(string characterName, string characterMoniker, int mxHlth, int moveSpeed)
    {
        this.characterName = characterName;
        this.characterMoniker = characterMoniker;
        this.mxHlth = cHlth = mxHlth;
        this.moveSpeed = moveSpeed;
    }

    public void Init()
    {
        cHlth = mxHlth;
    }

    public int GetMoveSpeed()
    {
        return moveSpeed;
    }

    public int GetCurrHealth()
    {
        return cHlth;
    }


}
