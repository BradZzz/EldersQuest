using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HumanTechnomancerClass : ClassNode
{
  public HumanTechnomancerClass(){
    whenToUpgrade = StaticClassRef.LEVEL2;
  }

  public override string ClassDesc()
  {
    return "+1 atk rng\nForceAtk";
  }

  public override string ClassName()
  {
      return "Technomancer";
  }

  public override ClassNode GetParent(){
      return new HumanBaseMage();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ new HumanTinkererClass(), new HumanGeniusClass() };
  }
 
  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetAtkRange(unit.GetAtkRange() + 1);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("ForceAtk");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}
