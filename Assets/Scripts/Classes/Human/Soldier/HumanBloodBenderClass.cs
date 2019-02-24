using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HumanBloodBenderClass : ClassNode
{
  public HumanBloodBenderClass(){
    whenToUpgrade = StaticClassRef.LEVEL4;
  }

  public override string ClassDesc()
  {
      return "BideKill\nHealKill";
  }

  public override string ClassName()
  {
      return "Blood Bender";
  }

  public override ClassNode GetParent(){
      return new HumanFMarineClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("BideKill");
      skills.Add("HealKill");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}
