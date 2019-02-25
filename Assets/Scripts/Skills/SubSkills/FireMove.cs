using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FireMove : Skill
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
      //path.RemoveAt(0);
      if (path == null || path.Count == 0) {
          return;
      }
      path.RemoveAt(0);
      foreach(TileProxy tile in path) {
          tile.SetTurnsOnFire(value * 2, unit);
      }
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
      return "Fire tiles generated on tiles where unit moved. " + ReturnBlurbByString(SkillGen.Fire);
  }

  public override string PrintStackDetails()
  {
      return ReturnStackTypeByString(Skill.SkillStack.buff);
  }
}
