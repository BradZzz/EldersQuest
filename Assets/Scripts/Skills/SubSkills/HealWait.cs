using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HealWait : Skill
{
  public override void RouteBehavior(Actions action, UnitProxy u1, UnitProxy u2, List<TileProxy> path)
  {
      switch(action){
          case Actions.DidWait: DidWait(u1); break;
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

  }

  public override void DidMove(UnitProxy unit, List<TileProxy> path){
  
  }

  public override void DidWait(UnitProxy unit)
  {
       BoardProxy.instance.GetTileAtPosition(unit.GetPosition()).CreateAnimation(Glossary.fx.healSmoke);
       unit.HealUnit(value, Skill.Actions.None);
  }

  public override void EndTurn(UnitProxy unit)
  {

  }

  public override SkillTypes[] GetSkillTypes()
  {
      return new SkillTypes[]{ SkillTypes.Defense };
  }

  public override string PrintDetails(){
      return "Heal self on wait. " + ReturnBlurbByString(SkillGen.Heal) + " " + ReturnBlurbByString(SkillGen.Wait);
  }

  public override string PrintStackDetails()
  {
      return ReturnStackTypeByString(Skill.SkillStack.buff);
  }
}
