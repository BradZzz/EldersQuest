using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HumanMineSweeperClass : ClassNode
{
  public HumanMineSweeperClass(){
    whenToUpgrade = StaticClassRef.LEVEL4;
  }

  public override string ClassDesc()
  {
    return "+1 hp\nWallMove";
  }

  public override string ClassName()
  {
      return "Mine Sweeper";
  }

  public override ClassNode GetParent(){
      return new HumanCSergeantClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetMaxHP(unit.GetMaxHP() + 1);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("WallMove");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}
