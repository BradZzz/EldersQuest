using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class Skill
{
    public int value;

    public abstract void RouteBehavior(Actions action, UnitProxy u1, UnitProxy u2, List<TileProxy> path);
    //public abstract void RouteBehavior(Actions action, UnitProxy u1, UnitProxy u2, List<TileProxy> path);
  
    //Implemented
    public abstract void BeginningGame(UnitProxy unit);
    public abstract void EndTurn(UnitProxy unit);
    public abstract void DidMove(UnitProxy unit, List<TileProxy> path);
    public abstract void DidWait(UnitProxy unit);
    public abstract void DidAttack(UnitProxy attacker, UnitProxy defender);
    public abstract void DidKill(UnitProxy attacker, UnitProxy defender);

    public abstract string PrintDetails();

    public static Skill ReturnSkillByString(SkillClasses sClass){
        switch(sClass){
            case SkillClasses.BideKill: return new BideKill();
            case SkillClasses.BideWait: return new BideWait();
            case SkillClasses.FireAtk: return new FireAtk();
            case SkillClasses.FireDef: return new FireDef();
            case SkillClasses.FireMove: return new FireMove();
            case SkillClasses.HealKill: return new HealKill();
            case SkillClasses.HealTurn: return new HealTurn();
            case SkillClasses.HealWait: return new HealWait();
            case SkillClasses.RageAlliesWait: return new RageAlliesWait();
            case SkillClasses.RageKill: return new RageKill();
            case SkillClasses.RageWait: return new RageWait();
            case SkillClasses.SicklyAtk: return new SicklyAtk();
            default: return null;
        }
    }

    public enum Actions{
        BeginGame, EndedTurn, DidAttack, DidMove, DidWait, DidKill, DidDefend, None
    }

    public enum SkillClasses{
        BideKill, BideWait, FireAtk, FireDef, FireMove, HealKill, HealTurn, HealWait, RageAlliesWait, RageKill, RageWait, SicklyAtk, None
    }
}
