using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HumanDCrusaderClass : ClassNode
{
  public HumanDCrusaderClass(){
    whenToUpgrade = StaticClassRef.LEVEL4;
  }

  public override string ClassDesc()
  {
      return "+1 hp\nAoeAtk\nDivineMove";
  }

  public override string ClassName()
  {
      return "Divine Crusader";
  }

  public override ClassNode GetParent(){
      return new HumanGPaladinClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetMaxHP(unit.GetMaxHP() + 1);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("AoeAtk");
      skills.Add("DivineMove");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}
