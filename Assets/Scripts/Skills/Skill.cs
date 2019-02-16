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
            case SkillClasses.AegisAlliesAtk: return new AegisAlliesAtk();
            case SkillClasses.AegisAtk: return new AegisAtk();
            case SkillClasses.AegisBegin: return new AegisBegin();
            case SkillClasses.AegisWait: return new AegisWait();
            case SkillClasses.AoeAtk: return new AoeAtk();
            case SkillClasses.BideKill: return new BideKill();
            case SkillClasses.BideWait: return new BideWait();
            case SkillClasses.EnfeebleAtk: return new EnfeebleAtk();
            case SkillClasses.FireAtk: return new FireAtk();
            case SkillClasses.FireDef: return new FireDef();
            case SkillClasses.FireKill: return new FireKill();
            case SkillClasses.FireMove: return new FireMove();
            case SkillClasses.ForceAtk: return new ForceAtk();  
            case SkillClasses.HealAlliesAtk: return new HealAlliesAtk(); 
            case SkillClasses.HealAtk: return new HealAtk(); 
            case SkillClasses.HealKill: return new HealKill();
            case SkillClasses.HealTurn: return new HealTurn();
            case SkillClasses.HealWait: return new HealWait();
            case SkillClasses.RageAlliesWait: return new RageAlliesWait();
            case SkillClasses.RageAtk: return new RageAtk();
            case SkillClasses.RageDef: return new RageDef();
            case SkillClasses.RageKill: return new RageKill();
            case SkillClasses.RageMove: return new RageMove();
            case SkillClasses.RageWait: return new RageWait();
            case SkillClasses.RootAtk: return new RootAtk();
            case SkillClasses.SicklyAtk: return new SicklyAtk();
            case SkillClasses.SkeleKill: return new SkeleKill();
            case SkillClasses.ThornDef: return new ThornDef();       
            case SkillClasses.VoidAtk: return new VoidAtk();  
            case SkillClasses.WarpAtk: return new WarpAtk();       
            default: return null;
        }
    }

    public enum Actions{
        BeginGame, EndedTurn, DidAttack, DidMove, DidWait, DidKill, DidDefend, None
    }

    public enum SkillClasses{
        AegisAlliesAtk, AegisAtk, AegisBegin, AegisWait, AoeAtk, BideKill, BideWait, EnfeebleAtk, FireAtk, FireDef, FireKill, FireMove, ForceAtk, 
        HealAtk, HealAlliesAtk, HealKill, HealTurn, HealWait,
        RageAlliesWait, RageAtk, RageDef, RageKill, RageMove, RageWait, RootAtk, SicklyAtk, SkeleKill, ThornDef, VoidAtk, WarpAtk, None
    }
}
