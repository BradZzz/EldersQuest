using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HealWait : Skill
{
  public override void RouteBehavior(Actions action, UnitProxy u1, UnitProxy u2)
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

  public override void DidDefend(UnitProxy attacker, UnitProxy defender)
  {

  }

  public override void DidKill(UnitProxy attacker, UnitProxy defender)
  {

  }

  public override void DidWait(UnitProxy unit)
  {
       Debug.Log("DidWait");
       int nwHlth = unit.GetData().GetCurrHealth() + value;
       unit.GetData().SetCurrHealth(nwHlth > unit.GetData().mxHlth ? unit.GetData().mxHlth : nwHlth);
  }

  public override void EndTurn(UnitProxy unit)
  {

  }

  public override void WasAttacked(UnitProxy attacker, UnitProxy defender)
  {

  }

  public override string PrintDetails(){
    return "HealWait";
  }
}
