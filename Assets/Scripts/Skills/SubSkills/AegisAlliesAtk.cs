using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AegisAlliesAtk : Skill
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
      foreach(TileProxy tl in BoardProxy.instance.GetAllVisitableNodes(attacker, value + 1, true)){
          bool isAttacker = tl == BoardProxy.instance.GetTileAtPosition(attacker.GetPosition());
          if (!isAttacker) {
              tl.FloatUp(Skill.Actions.DidAttack, "+1 aegis allies", Color.blue, "Aegis Allies");
              if (tl.HasUnit() && tl.GetUnit().GetData().GetTeam() == attacker.GetData().GetTeam()) {
                  tl.GetUnit().GetData().SetAegis(true);
              }
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
      return "AegisAlliesAtk";
  }
}
