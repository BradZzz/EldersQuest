using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BideAlliesWait : Skill
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
      foreach(TileProxy tl in BoardProxy.instance.GetAllVisitableNodes(unit, value + 1, true)){
          bool isUnit = tl == BoardProxy.instance.GetTileAtPosition(unit.GetPosition());
          if (!isUnit) {
              tl.FloatUp(Skill.Actions.DidWait, "+bide", Color.green, "+1 hp allies from waiting");
              if (tl.HasUnit() && tl.GetUnit().GetData().GetTeam() == unit.GetData().GetTeam()) {
                  tl.GetUnit().ReceiveHPBuff(1);
              }
          }
      }
  }

  public override void EndTurn(UnitProxy unit)
  {

  }

  public override string PrintDetails(){
      return "BideAlliesWait";
  }
}
