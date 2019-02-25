using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HealAlliesWait : Skill
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
          bool isAttacker = tl == BoardProxy.instance.GetTileAtPosition(unit.GetPosition());
          if (!isAttacker) {
              tl.FloatUp(Skill.Actions.DidAttack, "heal", Color.green, "Allies healed from another unit's attack");
              if (tl.HasUnit() && tl.GetUnit().GetData().GetTeam() == unit.GetData().GetTeam()) {
                    Debug.Log("Attempting to heal unit");
                    tl.GetUnit().HealUnit(1, Skill.Actions.DidAttack);
              }
          }
      }
  }

  public override void EndTurn(UnitProxy unit)
  {

  }

  public override SkillTypes[] GetSkillTypes()
  {
      return new SkillTypes[]{ SkillTypes.Defense };
  }

  public override string PrintDetails(){
      return "Heal nearby allies on wait. " + ReturnBlurbByString(SkillGen.Heal) + " " + ReturnBlurbByString(SkillGen.Wait);
  }

  public override string PrintStackDetails()
  {
      return ReturnStackTypeByString(Skill.SkillStack.rng);
  }
}
