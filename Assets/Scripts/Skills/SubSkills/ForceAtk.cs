using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
  Pushes attacked enemy away
*/

[Serializable]
public class ForceAtk : Skill
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
      Vector3Int posAtk = attacker.GetPosition();
      Vector3Int posDef = defender.GetPosition();

      Vector3Int diff = new Vector3Int(posAtk.x - posDef.x,posAtk.y - posDef.y,posAtk.z - posDef.z);

      Debug.Log("Force atk: ");
      Debug.Log("posAtk: " + posAtk.ToString());
      Debug.Log("posDef: " + posDef.ToString());
      Debug.Log("diff: " + diff.ToString());
      if (diff.x > 0) {
        //Defender to the right of the attacker
        if (diff.y > 0) {
          Debug.Log("Right and Above");
          //Defender is above attacker
          //Move the defender up and to the right
          posDef.x-=1;
          posDef.y-=1;
        } else if (diff.y < 0) {
          Debug.Log("Right and Below");
          //Defender is below attacker
          //Move the defender down and to the right
          posDef.x-=1;
          posDef.y+=1;
        } else {
          Debug.Log("Right");
          posDef.x-=1;
        }
      } else if (diff.x < 0) {
        //Defender to the left of the attacker
        if (diff.y > 0) {
          Debug.Log("Left and Above");
          //Defender is above attacker
          //Move the defender up and to the left
          posDef.x+=1;
          posDef.y-=1;
        } else if (diff.y < 0) {
          Debug.Log("Left and Below");
          //Defender is below attacker
          //Move the defender down and to the left
          posDef.x+=1;
          posDef.y+=1;
        } else {
          Debug.Log("Left");
          posDef.x+=1;
        }
      } else {
        //Defender is right below or above attacker
        if (diff.y > 0) {
          Debug.Log("Above");
          //Defender is above attacker
          //Move defender up
          posDef.y-=1;
        } else if (diff.y < 0) {
          Debug.Log("Below");
          //Defender is below attacker
          //Move defender down
          posDef.y+=1;
        }
      }
      Debug.Log("posDef: " + posDef.ToString());
      TileProxy nwDefTile = BoardProxy.instance.GetTileAtPosition(posDef);
      if (nwDefTile != null && !nwDefTile.HasObstruction()) {
          TileProxy oldTile = BoardProxy.instance.GetTileAtPosition(defender.GetPosition());
      
          defender.ZapToTile(nwDefTile, oldTile, "ForceAtk");

          //nwDefTile.ReceiveGridObjectProxy(defender);
          //nwDefTile.FloatUp(Skill.Actions.None, "whabam!", Color.blue, "ForceAtk");
          //oldTile.RemoveGridObjectProxy(defender);
          //oldTile.FloatUp(Skill.Actions.None, "poof", Color.cyan, "ForceAtk");
          //defender.SnapToCurrentPosition();   
      } else if (nwDefTile != null) {
          Debug.Log("Character was slammed into obstacle! Might want to do something here.");
      }
  }

  //IEnumerator MoveDefender(UnitProxy unit, TileProxy oTl, TileProxy nTl){
  //        yield return new WaitForSeconds(.2f);
  //        oTl.RemoveGridObjectProxy(unit);
  //        oTl.FloatUp(Skill.Actions.DidAttack, "poof", Color.cyan, "ForceAtk");
  //        nTl.ReceiveGridObjectProxy(unit);
  //        nTl.FloatUp(Skill.Actions.DidAttack, "whabam!", Color.blue, "ForceAtk");
  //        unit.SnapToCurrentPosition();   
  //}

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

  public override SkillTypes[] GetSkillTypes()
  {
      return new SkillTypes[]{ SkillTypes.Utility };
  }

  public override string PrintDetails(){
      return "Force enemy unit on attack. " + ReturnBlurbByString(GetSkillGen());
  }

  public override string PrintStackDetails()
  {
      return ReturnStackTypeByString(Skill.SkillStack.nostack);
  }

  public override SkillGen GetSkillGen()
  {
      return SkillGen.Force;
  }
}
