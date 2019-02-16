using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HumanGDancerClass : ClassNode
{
  public HumanGDancerClass(){
    whenToUpgrade = StaticClassRef.LEVEL3;
  }

  public override string ClassDesc()
  {
      return "+1 atk\nForceAtk";
  }

  public override string ClassName()
  {
      return "Gun Dancer";
  }

  public override ClassNode GetParent(){
      return new HumanBerserkerClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetAttack(unit.GetAttack() + 1);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("ForceAtk");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}
