using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CthulhuGodzillaClass : ClassNode
{
  public CthulhuGodzillaClass(){
    whenToUpgrade = StaticClassRef.LEVEL4;
  }

  public override string ClassDesc()
  {
    return "+1 atk rng\nAoeAtk";
  }

  public override string ClassName()
  {
      return "Godzilla";
  }

  public override ClassNode GetParent(){
      return new CthulhuSBehemothClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetAtkRange(unit.GetAtkRange() + 1);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("AoeAtk");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}
