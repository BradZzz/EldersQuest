using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ThornDef : Skill
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
      foreach(TileProxy tl in BoardProxy.instance.GetAllVisitableNodes(defender, 2, true)){
          tl.FloatUp("thorn dmg", Color.green, TileProxy.ATK_WAIT);
          if (tl.HasUnit() && !tl == BoardProxy.instance.GetTileAtPosition(defender.GetPosition())) {
              if (tl.GetUnit().IsAttackedEnvironment(value)){
                  defender.GetData().SetLvl(defender.GetData().GetLvl() + 1);
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
      return "ThornDef";
  }
}
