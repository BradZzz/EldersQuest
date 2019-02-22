using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WarpAtk : Skill
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
      List<TileProxy> availableTiles = BoardProxy.instance.GetOpenTiles();
      HelperScripts.Shuffle(availableTiles);

      if (availableTiles.Count > 0) {
          TileProxy oldTile = BoardProxy.instance.GetTileAtPosition(defender.GetPosition());
          availableTiles.Remove(oldTile);
          defender.ZapToTile(availableTiles[0], oldTile, "WarpAtk");

          //availableTiles[0].FloatUp(Skill.Actions.None, "whabam!", Color.blue, "WarpAtk");
          //availableTiles[0].ReceiveGridObjectProxy(defender);
          //oldTile.FloatUp(Skill.Actions.None, "poof", Color.cyan, "WarpAtk");
          //oldTile.RemoveGridObjectProxy(defender);
          defender.SnapToCurrentPosition(); 
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
      return "WarpAtk";
  }
}
