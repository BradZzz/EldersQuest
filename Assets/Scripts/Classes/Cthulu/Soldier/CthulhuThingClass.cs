using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CthulhuThingClass : ClassNode
{
  public CthulhuThingClass(){
    whenToUpgrade = StaticClassRef.LEVEL4;
  }

  public override string ClassDesc()
  {
    return "+3 hp\nNullifyAtk";
  }

  public override string ClassName()
  {
      return "Thing";
  }

  public override ClassNode GetParent(){
      return new CthulhuSBehemothClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }

 
  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetMaxHP(unit.GetMaxHP() + 3);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("NullifyAtk");
      unit.SetSkills(skills.ToArray());
      return unit;
  }

  public override string ClassInactiveDesc(){
      return "NullifyAtk";
  }

  public override Unit InactiveUpgradeCharacter(Unit unit)
  {
      unit.SetSkillsBuffs(new string[]{ "NullifyAtk" });
      return unit;
  }
}
