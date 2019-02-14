using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class Skill
{
    public int value;

    public abstract void RouteBehavior(Actions action, UnitProxy u1, UnitProxy u2);
  
    //Implemented
    public abstract void BeginningGame(UnitProxy unit);
    public abstract void WasAttacked(UnitProxy attacker, UnitProxy defender);
    public abstract void EndTurn(UnitProxy unit);

    public abstract void DidAttack(UnitProxy attacker, UnitProxy defender);
    //Implemented
    public abstract void DidWait(UnitProxy unit);
    public abstract void DidKill(UnitProxy attacker, UnitProxy defender);
    public abstract void DidDefend(UnitProxy attacker, UnitProxy defender);

    public abstract string PrintDetails();

    public static Skill ReturnSkillByString(SkillClasses sClass){
        switch(sClass){
            case SkillClasses.HealWait: return new HealWait();
            default: return null;
        }
    }

    public enum Actions{
        BeginGame, WasAttacked, EndedTurn, DidAttack, DidWait, DidKill, DidDefend, None
    }

    public enum SkillClasses{
        HealWait, None
    }
}
