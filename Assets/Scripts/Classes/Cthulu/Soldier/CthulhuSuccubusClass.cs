using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CthulhuSuccubusClass : ClassNode
{
  public CthulhuSuccubusClass(){
    whenToUpgrade = StaticClassRef.LEVEL3;
  }

  public override string ClassDesc()
  {
    return "BideKill";
  }

  public override string ClassName()
  {
      return "Succubus";
  }

  public override ClassNode GetParent(){
      return new CthulhuVamprossClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ new CthulhuEveClass(), new CthulhuLilithClass() };
  }

 
  public override Unit UpgradeCharacter(Unit unit)
  {
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("BideKill");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}
