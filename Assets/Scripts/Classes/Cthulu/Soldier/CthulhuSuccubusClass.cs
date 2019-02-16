using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CthulhuSuccubusClass : ClassNode
{
  public CthulhuSuccubusClass(){
    whenToUpgrade = 6;
  }

  public override string ClassDesc()
  {
    return "+1 hp\nBideKill";
  }

  public override string ClassName()
  {
      return "Succubus";
  }

  public override ClassNode GetParent(){
      return new CthulhuVamprossClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }

 
  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetMaxHP(unit.GetMaxHP() + 1);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("BideKill");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}
