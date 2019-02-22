using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HumanGeniusClass : ClassNode
{
  public HumanGeniusClass(){
    whenToUpgrade = StaticClassRef.LEVEL3;
  }

  public override string ClassDesc()
  {
    return "+2 hp\nAegisAlliesAtk";
  }

  public override string ClassName()
  {
      return "Genius";
  }

  public override ClassNode GetParent(){
      return new HumanTechnomancerClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetMaxHP(unit.GetMaxHP() + 2);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("AegisAlliesAtk");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}
