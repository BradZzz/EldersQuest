using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HealTurn : Skill
{
  public override void RouteBehavior(Actions action, UnitProxy u1, UnitProxy u2, List<TileProxy> path)
  {
      switch(action){
          case Actions.EndedTurn: EndTurn(u1); break;
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

  public override void DidWait(UnitProxy unit)
  {

  }

  public override void DidMove(UnitProxy unit, List<TileProxy> path){
  
  }

  public override void EndTurn(UnitProxy unit)
  {
       int nwHlth = unit.GetData().GetCurrHealth() + value;
       nwHlth = nwHlth > unit.GetData().GetMaxHP() ? unit.GetData().GetMaxHP() : nwHlth;
       if (nwHlth != unit.GetData().GetCurrHealth()) {
         unit.GetData().SetCurrHealth(nwHlth);
         unit.FloatNumber(value, Color.green);
       }
  }

  public override string PrintDetails(){
      return "HealTurn";
  }
}
