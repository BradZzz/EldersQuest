using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HumanBramblelockClass : ClassNode
{
  public HumanBramblelockClass(){
    whenToUpgrade = StaticClassRef.LEVEL4;
  }

  public override string ClassDesc()
  {
    return "+2 hp\nWallMove";
  }

  public override string ClassName()
  {
      return "Bramblelock";
  }

  public override ClassNode GetParent(){
      return new HumanTinkererClass();
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
