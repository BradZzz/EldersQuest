using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CthulhuSBehemothClass : ClassNode
{
  public CthulhuSBehemothClass(){
    whenToUpgrade = StaticClassRef.LEVEL3;
  }

  public override string ClassDesc()
  {
    return "+2 hp\nEnfeebleAtk";
  }

  public override string ClassName()
  {
      return "Sludge Behemoth";
  }

  public override ClassNode GetParent(){
      return new CthulhuGoliathClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ new CthulhuGodzillaClass(), new CthulhuThingClass()};
  }

 
  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetMaxHP(unit.GetMaxHP() + 2);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("EnfeebleAtk");
      unit.SetSkills(skills.ToArray());
      return unit;
  }

  public override string ClassInactiveDesc(){
      return "EnfeebleAtk";
  }

  public override Unit InactiveUpgradeCharacter(Unit unit)
  {
      unit.SetSkillsBuffs(new string[]{ "EnfeebleAtk" });
      return unit;
  }
}
