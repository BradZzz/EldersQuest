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
    return "FireAtk\nHealWait";
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
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("FireAtk");
      skills.Add("HealWait");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}
