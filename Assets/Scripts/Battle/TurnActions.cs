using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class TurnActions
{
    //The amount of times this unit can attack
    protected int atk;
    //The amount of times this unit an move
    protected int mv;

    public bool idle;

    private int cAtk;
    private int cMv;

    private int enfeebled;
    private int rooted;

    //public TurnActions()
    //{
    //    atk = 1;
    //    mv = 1;
    //}

    public void BeginTurn(){

        if (enfeebled > 0) {
            cAtk = atk - 1;
            enfeebled--;
        } else {
            cAtk = atk;
        }
        if (rooted > 0) {
            cMv = mv - 1;
            rooted--;
        } else {
            cMv = mv;
        }
        idle = false;
    }

    public void EndTurn(){
        cAtk = 0;
        cMv = 0;
        idle = false;
    }

    public void SetMoves(int moves){
        cMv = moves;
        cMv = cMv < 0 ? 0 : cMv;
    }

    public void SetAttacks(int attacks){
        cAtk = attacks;
        cAtk = cAtk < 0 ? 0 : cAtk;
    }

    public int GetMoves(){
        return cMv;
    }

    public int GetAttacks(){
        return cAtk;
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

    public bool IsEnfeebled(){
        return enfeebled > 0;
    }

    public void EnfeebledForTurns(int feeble)
    {
        if (feeble > enfeebled) {
            enfeebled = feeble;
        }
    }

    public bool IsRooted(){
        return rooted > 0;
    }

    public void RootForTurns(int root)
    {
        if (root > rooted) {
            rooted = root;
        }
    }

    public string ToString()
    {
        return "Atk: " + cAtk.ToString() + " | Mv: " + cMv.ToString();
    }
}
