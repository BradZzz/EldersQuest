using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AegisAtk : Skill
{
  public override void RouteBehavior(Actions action, UnitProxy u1, UnitProxy u2, List<TileProxy> path)
  {
      switch(action){
          case Actions.DidAttack: DidAttack(u1, u2); break;
          default: return;
      }
  }

  public override void BeginningGame(UnitProxy unit)
  {

  }

  public override void DidAttack(UnitProxy attacker, UnitProxy defender)
  {
      BoardProxy.instance.GetTileAtPosition(attacker.GetPosition()).CreateAnimation(Glossary.fx.fireShield, AnimationInteractionController.ATK_WAIT);
      attacker.SetAegis(true, AnimationInteractionController.ATK_WAIT);
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
      return "Provides an Aegis shield to self on successful attack. " + ReturnBlurbByString(SkillGen.Aegis);
  }

  public override string PrintStackDetails()
  {
      return ReturnStackTypeByString(Skill.SkillStack.nostack);
  }
}
