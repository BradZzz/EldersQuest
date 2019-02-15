using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
  Pulls attacked enemy in
*/

[Serializable]
public class VoidAtk : Skill
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
      TileProxy defTile = BoardProxy.instance.GetTileAtPosition(defender.GetPosition());
      TileProxy atkTile = BoardProxy.instance.GetTileAtPosition(attacker.GetPosition());

      Path<TileProxy> path = BoardProxy.instance.GetPath(atkTile, defTile, defender, true);
      foreach (TileProxy tl in path)
      {
          if (tl != defTile && tl != atkTile) {
              tl.ReceiveGridObjectProxy(defender);
              defTile.RemoveGridObjectProxy(defender);
              defender.SnapToCurrentPosition();
              break;
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
      return "VoidAtk";
  }
}
