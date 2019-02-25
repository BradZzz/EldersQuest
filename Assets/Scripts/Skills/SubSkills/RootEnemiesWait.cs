using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RootEnemiesWait : Skill
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
      TileProxy uTile = BoardProxy.instance.GetTileAtPosition(unit.GetPosition());
      foreach(TileProxy tl in BoardProxy.instance.GetAllVisitableNodes(unit, value + 1, true)){
          bool isAttacker = tl == uTile;
          if (!isAttacker) {
              tl.FloatUp(Skill.Actions.DidAttack, "rooted", Color.grey, "Player rooted to position from atk");
              if (tl.HasUnit() && tl.GetUnit().GetData().GetTeam() != unit.GetData().GetTeam()) {
                  tl.GetUnit().GetData().GetTurnActions().RootForTurns(value);
              }
          }
      }
  }

  public override void EndTurn(UnitProxy unit)
  {

  }

  public override SkillTypes[] GetSkillTypes()
  {
      return new SkillTypes[]{ SkillTypes.Utility };
  }

  public override string PrintDetails(){
      return "Root enemies on wait. " + ReturnBlurbByString(SkillGen.Root) + " " + ReturnBlurbByString(SkillGen.Wait);
  }

  public override string PrintStackDetails()
  {
      return ReturnStackTypeByString(Skill.SkillStack.rng);
  }
}
