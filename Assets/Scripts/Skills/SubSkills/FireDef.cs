using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FireDef : Skill
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
      foreach(TileProxy tl in BoardProxy.instance.GetAllVisitableNodes(defender, value, true)){
          if (!tl.HasObstacle() && tl != BoardProxy.instance.GetTileAtPosition(defender.GetPosition())) {
              tl.SetTurnsOnFire(2, defender);
          }
      }
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
      return new SkillTypes[]{ SkillTypes.Utility };
  }

  public override string PrintDetails(){
      return "Fire tiles on hit from enemy. " + ReturnBlurbByString(SkillGen.Fire);
  }

  public override string PrintStackDetails()
  {
      return ReturnStackTypeByString(Skill.SkillStack.rng);
  }
}
