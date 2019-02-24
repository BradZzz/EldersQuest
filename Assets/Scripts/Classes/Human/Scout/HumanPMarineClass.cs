using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HumanPMarineClass : ClassNode
{
  public HumanPMarineClass(){
    whenToUpgrade = StaticClassRef.LEVEL4;
  }

  public override string ClassDesc()
  {
    return "+1 atk rng\nAoeAtk";
  }

  public override string ClassName()
  {
      return "Plasma Marine";
  }

  public override ClassNode GetParent(){
      return new HumanMSergeantClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetAtkRange(unit.GetAtkRange() + 1);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("AoeAtk");
      unit.SetSkills(skills.ToArray());      
      return unit;
  }
}
