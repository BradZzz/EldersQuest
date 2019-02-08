using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class TurnActions
{
    //The amount of times this unit can attack
    public int atk;
    //The amount of times this unit an move
    public int mv;

    private int cAtk;
    private int cMv;

    public TurnActions()
    {
        atk = 1;
        mv = 1;
    }

    public void BeginTurn(){
        cAtk = atk;
        cMv = mv;
    }

    public void EndTurn(){
        cAtk = 0;
        cMv = 0;
    }

    public bool CanAttack()
    {
        return cAtk > 0;
    }

    public void Attack()
    {
        cAtk -= 1;
    }

    public bool CanMove()
    {
        return cMv > 0;
    }

    public void Move()
    {
        cMv -= 1;
    }

    public string ToString()
    {
        return "Atk: " + cAtk.ToString() + " | Mv: " + cMv.ToString();
    }
}
