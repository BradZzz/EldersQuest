using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RageAlliesWait : Skill
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
              tl.FloatUp(Skill.Actions.DidWait, "+rage", Color.yellow, "+1 atk allies from waiting");
              //tl.CreateAnimation(Glossary.fx.fireShield, AnimationInteractionController.NO_WAIT);
              if (tl.HasUnit() && tl.GetUnit().GetData().GetTeam() == unit.GetData().GetTeam()) {
                  tl.GetUnit().ReceiveAtkBuff(1);
              }
          }
      }
  }

  public override void EndTurn(UnitProxy unit)
  {

  }

  public override SkillTypes[] GetSkillTypes()
  {
      return new SkillTypes[]{ SkillTypes.Offense };
  }

  public override string PrintDetails(){
      return "Rage allies on wait. " + ReturnBlurbByString(GetSkillGen()) + " " + ReturnBlurbByString(SkillGen.Wait);
  }

  public override string PrintStackDetails()
  {
      return ReturnStackTypeByString(Skill.SkillStack.rng);
  }

  public override SkillGen GetSkillGen()
  {
      return SkillGen.Rage;
  }
}
