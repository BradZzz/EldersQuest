using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AoeAtk : Skill
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
      /* Injure everything else that isn't that defender */
      foreach(TileProxy tl in BoardProxy.instance.GetAllVisitableNodes(defender, value + 1, true)){
          tl.FloatUp("boom", Color.magenta);
          if (tl.HasUnit() && tl.GetUnit() != defender) {
              tl.GetUnit().IsAttacked(attacker,false);
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

  public override string PrintDetails(){
      return "AoeAtk";
  }
}
