using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HumanGigaWizardClass : ClassNode
{
  public HumanGigaWizardClass(){
    whenToUpgrade = StaticClassRef.LEVEL4;
  }

  public override string ClassDesc()
  {
    return "+1 atk\nHealTurn";
  }

  public override string ClassName()
  {
      return "Giga Wizard";
  }

  public override ClassNode GetParent(){
      return new HumanWizardClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetAttack(unit.GetAttack() + 1);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("HealTurn");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}
