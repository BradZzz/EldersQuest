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
    return "WallMove\nForceAtk";
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
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("ForceAtk");
      skills.Add("WallMove");
      unit.SetSkills(skills.ToArray());      
      return unit;
  }
}
