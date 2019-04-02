using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RageMove : Skill
{
  public override void RouteBehavior(Actions action, UnitProxy u1, UnitProxy u2, List<TileProxy> path)
  {
      switch(action){
          case Actions.DidMove: DidMove(u1, path); break;
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
      int atkBuff = path.Count - 1;
      unit.GetData().SetTurnAttackBuff(unit.GetData().GetTurnAttackBuff() + atkBuff);
  }

  public override void DidWait(UnitProxy unit)
  {

  }

  public override void EndTurn(UnitProxy unit)
  {

  }

  public override SkillTypes[] GetSkillTypes()
  {
      return new SkillTypes[]{ SkillTypes.Offense };
  }

  public override string PrintDetails(){
      return "Rage self on move. Loses effect at end of turn. " + ReturnBlurbByString(GetSkillGen());
  }

  public override string PrintStackDetails()
  {
      return ReturnStackTypeByString(Skill.SkillStack.move);
  }

  public override SkillGen GetSkillGen()
  {
      return SkillGen.Rage;
  }
}
