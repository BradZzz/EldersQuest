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
       int nwHlth = attacker.GetData().GetCurrHealth() + value;
       nwHlth = nwHlth > attacker.GetData().GetMaxHP() ? attacker.GetData().GetMaxHP() : nwHlth;
       if (nwHlth != attacker.GetData().GetCurrHealth()) {
         attacker.GetData().SetCurrHealth(nwHlth);
         attacker.FloatNumber(value, Color.green);
       }
  }

  public override void DidWait(UnitProxy unit)
  {

  }

  public override void DidMove(UnitProxy unit, List<TileProxy> path){
  
  }

  public override void EndTurn(UnitProxy unit)
  {

  }

  public override string PrintDetails(){
      return "HealKill";
  }
}
