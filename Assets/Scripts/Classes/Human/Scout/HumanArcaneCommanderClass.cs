using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HumanArcaneCommanderClass : ClassNode
{
  public HumanArcaneCommanderClass(){
    whenToUpgrade = StaticClassRef.LEVEL4;
  }

  public override string ClassDesc()
  {
    return "DivineMove\nDivineMove";
  }

  public override string ClassName()
  {
      return "Arcane Commander";
  }

  public override ClassNode GetParent(){
      return new HumanLieutenantClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("DivineMove");
      skills.Add("DivineMove");
      unit.SetSkills(skills.ToArray());
      return unit;
  }

  public override string ClassInactiveDesc(){
      return "AegisWait";
  }

  public override Unit InactiveUpgradeCharacter(Unit unit)
  {
      unit.SetSkillsBuffs(new string[]{ "AegisWait" });
      return unit;
  }
}
