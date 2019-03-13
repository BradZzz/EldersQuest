using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HumanQEngineerClass : ClassNode
{
  public HumanQEngineerClass(){
    whenToUpgrade = StaticClassRef.LEVEL4;
  }

  public override string ClassDesc()
  {
    return "+1 mv\n+1 mv trn\nWallMove";
  }

  public override string ClassName()
  {
      return "Quake Engineer Marine";
  }

  public override ClassNode GetParent(){
      return new HumanMSergeantClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetMoveSpeed(unit.GetMoveSpeed() + 1);
      unit.SetTurnMoves(unit.GetTurnMoves() + 1);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("WallMove");
      unit.SetSkills(skills.ToArray());      
      return unit;
  }
}
