using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HumanIceWizardClass : ClassNode
{
  public HumanIceWizardClass(){
    whenToUpgrade = StaticClassRef.LEVEL4;
  }

  public override string ClassDesc()
  {
    return "AegisAtk\nSnowAtk\nSnowAtk";
  }

  public override string ClassName()
  {
      return "Ice Wizard";
  }

  public override ClassNode GetParent(){
      return new HumanWizardClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("AegisAtk");
      skills.Add("SnowAtk");
      skills.Add("SnowAtk");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}
