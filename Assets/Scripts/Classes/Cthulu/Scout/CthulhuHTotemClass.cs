using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CthulhuHTotemClass : ClassNode
{
  public CthulhuHTotemClass(){
    whenToUpgrade = StaticClassRef.LEVEL3;
  }

  public override string ClassDesc()
  {
    return "ThornDef\nThornDef";
  }

  public override string ClassName()
  {
      return "Hell Totemic";
  }

  public override ClassNode GetParent(){
      return new CthulhuStolisClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ new CthulhuDRiderClass(), new CthulhuPBearerClass() };
  }
 
  public override Unit UpgradeCharacter(Unit unit)
  {
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("ThornDef");
      skills.Add("ThornDef");
      unit.SetSkills(skills.ToArray());
      return unit;
  }

  public override string ClassInactiveDesc(){
      return "ThornDef";
  }

  public override Unit InactiveUpgradeCharacter(Unit unit)
  {
      unit.SetSkillsBuffs(new string[]{ "ThornDef" });
      return unit;
  }
}
