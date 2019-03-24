using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HumanArchMageClass : ClassNode
{
  public HumanArchMageClass(){
    whenToUpgrade = StaticClassRef.LEVEL3;
  }

  public override string ClassDesc()
  {
    return "FireAtk\nFireAtk\n+1 atk rng";
  }

  public override string ClassName()
  {
      return "Archmage";
  }

  public override ClassNode GetParent(){
      return new HumanIWarlockClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ new HumanGrandMageClass(), new HumanMetalmancerClass() };
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetAtkRange(unit.GetAtkRange() + 1);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("FireAtk");
      skills.Add("FireAtk");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}
