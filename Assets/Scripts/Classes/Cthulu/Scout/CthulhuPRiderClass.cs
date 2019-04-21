using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CthulhuPRiderClass : ClassNode
{
  public CthulhuPRiderClass(){
    whenToUpgrade = StaticClassRef.LEVEL4;
  }

  public override string ClassDesc()
  {
    return "HobbleAtk\nSicklyAtk";
  }

  public override string ClassName()
  {
      return "Pestilence Rider";
  }

  public override ClassNode GetParent(){
      return new CthulhuApparitionClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }
 
  public override Unit UpgradeCharacter(Unit unit)
  {
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("HobbleAtk");
      skills.Add("SicklyAtk");
      unit.SetSkills(skills.ToArray());
      return unit;
  }

  public override string ClassInactiveDesc(){
      return "HobbleAtk";
  }

  public override Unit InactiveUpgradeCharacter(Unit unit)
  {
      unit.SetSkillsBuffs(new string[]{ "HobbleAtk" });
      return unit;
  }
}
