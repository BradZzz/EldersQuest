using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class QuickenAlliesWait : Skill
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
              tl.FloatUp(Skill.Actions.DidAttack, "+move", Color.blue, "Quicken Allies");
              if (tl.HasUnit() && tl.GetUnit().GetData().GetTeam() == unit.GetData().GetTeam()) {
                  //TurnActions ta = tl.GetUnit().GetData().GetTurnActions();
                  //ta.SetMoves(ta.GetMoves() + 1);
                  tl.GetUnit().SetQuickened(true);
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
      return "Quickens allies in range on wait. " + ReturnBlurbByString(GetSkillGen()) + "" + ReturnBlurbByString(SkillGen.Wait);
  }

  public override string PrintStackDetails()
  {
      return ReturnStackTypeByString(Skill.SkillStack.rng);
  }

  public override SkillGen GetSkillGen()
  {
      return SkillGen.Quicken;
  }
}
