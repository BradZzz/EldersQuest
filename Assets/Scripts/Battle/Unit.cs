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
    private int cHlth = 1;
    private int moveSpeed = 3;
    private int team = 0;

    public Unit()
    {
        this.characterName = "0";
        this.characterMoniker = "Null";
    }

    public Unit(string characterName, string characterMoniker, int team, int mxHlth, int moveSpeed)
    {
        this.characterName = characterName;
        this.characterMoniker = characterMoniker;
        this.team = team;
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

    public int GetTeam()
    {
      return team;
    }
}
