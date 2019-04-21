using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EgyptPorcupineClass : ClassNode
{
  public EgyptPorcupineClass(){
    whenToUpgrade = StaticClassRef.LEVEL4;
  }

  public override string ClassDesc()
  {
    return "HealAtk\nFireDef";
  }

  public override string ClassName()
  {
      return "Porcupine";
  }

  public override ClassNode GetParent(){
      return new EgyptFBenderClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }
 
  public override Unit UpgradeCharacter(Unit unit)
  {
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("FireDef");
      skills.Add("HealAtk");
      unit.SetSkills(skills.ToArray());
      return unit;
  }

  public override string ClassInactiveDesc(){
      return "FireDef. FireDef";
  }


  public override Unit InactiveUpgradeCharacter(Unit unit)
  {
      unit.SetSkillsBuffs(new string[]{ "FireDef", "FireDef" });
      return unit;
  }
}
