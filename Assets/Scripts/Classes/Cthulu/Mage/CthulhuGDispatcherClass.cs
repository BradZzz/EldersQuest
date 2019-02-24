using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CthulhuGDispatcherClass : ClassNode
{
  public CthulhuGDispatcherClass(){
    whenToUpgrade = StaticClassRef.LEVEL4;
  }

  public override string ClassDesc()
  {
    return "AoeAtk\nSkeleKill";
  }

  public override string ClassName()
  {
      return "Grim Dispatcher";
  }

  public override ClassNode GetParent(){
      return new CthulhuFurymancerClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("AoeAtk");
      skills.Add("SkeleKill");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}
