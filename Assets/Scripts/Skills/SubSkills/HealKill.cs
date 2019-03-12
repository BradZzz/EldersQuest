using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HealKill : Skill
{
  public override void RouteBehavior(Actions action, UnitProxy u1, UnitProxy u2, List<TileProxy> path)
  {
      switch(action){
          case Actions.DidKill: DidKill(u1, u2); break;
          default: return;
      }
  }

  public override void BeginningGame(UnitProxy unit)
  {

  }

  public override void DidAttack(UnitProxy attacker, UnitProxy defender)
  {

  }

  public override void DidKill(UnitProxy attacker, UnitProxy defender)
  {
       BoardProxy.instance.GetTileAtPosition(attacker.GetPosition()).CreateAnimation(Glossary.fx.healSmoke);
       attacker.HealUnit(value, Skill.Actions.DidAttack);
  }

  public override void DidWait(UnitProxy unit)
  {

  }

  public override void DidMove(UnitProxy unit, List<TileProxy> path){
  
  }

  public override void EndTurn(UnitProxy unit)
  {

  }

  public override SkillTypes[] GetSkillTypes()
  {
      return new SkillTypes[]{ SkillTypes.Defense };
  }

  public override string PrintDetails(){
      return "Heal self on successful enemy kill. " + ReturnBlurbByString(SkillGen.Heal);
  }

  public override string PrintStackDetails()
  {
      return ReturnStackTypeByString(Skill.SkillStack.buff);
  }
}
