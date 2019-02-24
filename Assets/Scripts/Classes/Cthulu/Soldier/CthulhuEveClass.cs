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
    return "+1 hp\nBideAlliesWait";
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
      unit.SetMaxHP(unit.GetMaxHP() + 1);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("BideAlliesWait");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}
