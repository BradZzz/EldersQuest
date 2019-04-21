using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CthulhuEveClass : ClassNode
{
  public CthulhuEveClass(){
    whenToUpgrade = StaticClassRef.LEVEL4;
  }

  public override string ClassDesc()
  {
    return "BideAlliesWait\nBideAlliesWait";
  }

  public override string ClassName()
  {
      return "Eve";
  }

  public override ClassNode GetParent(){
      return new CthulhuSuccubusClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }

 
  public override Unit UpgradeCharacter(Unit unit)
  {
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("BideAlliesWait");
      skills.Add("BideAlliesWait");
      unit.SetSkills(skills.ToArray());
      return unit;
  }

  public override string ClassInactiveDesc(){
      return "HealWait";
  }

  public override Unit InactiveUpgradeCharacter(Unit unit)
  {
      unit.SetSkillsBuffs(new string[]{ "HealWait" });
      return unit;
  }
}
