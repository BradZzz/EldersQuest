using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AegisBegin : Skill
{
  public override void RouteBehavior(Actions action, UnitProxy u1, UnitProxy u2, List<TileProxy> path)
  {
      switch(action){
          case Actions.BeginGame: BeginningGame(u1); break;
          default: return;
      }
  }

  public override void BeginningGame(UnitProxy unit)
  {
      BoardProxy.instance.GetTileAtPosition(unit.GetPosition()).CreateAnimation(Glossary.fx.fireShield);
      unit.SetAegis(true);
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

  }

  public override void EndTurn(UnitProxy unit)
  {

  }

  public override SkillTypes[] GetSkillTypes()
  {
      return new SkillTypes[]{ SkillTypes.Defense };
  }

  public override string PrintDetails(){
      return "Provides an Aegis shield at the beginning of battle. " + ReturnBlurbByString(GetSkillGen());
  }

  public override string PrintStackDetails()
  {
      return ReturnStackTypeByString(Skill.SkillStack.nostack);
  }

  public override SkillGen GetSkillGen()
  {
      return SkillGen.Aegis;
  }
}
