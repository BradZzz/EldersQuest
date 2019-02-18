using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RootAtk : Skill
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
      BoardProxy.instance.GetTileAtPosition(defender.GetPosition()).FloatUp(Skill.Actions.DidAttack, "rooted", Color.grey, "Player rooted to position from atk");
      defender.GetData().GetTurnActions().RootForTurns(value);
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
      return "RootAtk";
  }
}
