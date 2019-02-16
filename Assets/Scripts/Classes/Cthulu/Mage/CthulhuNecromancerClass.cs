using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CthulhuNecromancerClass : ClassNode
{
  public CthulhuNecromancerClass(){
    whenToUpgrade = StaticClassRef.LEVEL2;
  }

  public override string ClassDesc()
  {
    return "+2 hp\nSkeleKill";
  }

  public override string ClassName()
  {
      return "Necromancer";
  }

  public override ClassNode GetParent(){
      return new CthulhuBaseMage();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ new CthulhuLichClass(), new CthulhuFurymancerClass() };
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      int hp = unit.GetMaxHP();
      unit.SetMaxHP(hp + 2);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("SkeleKill");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}
