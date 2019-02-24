using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CthulhuLichClass : ClassNode
{
  public CthulhuLichClass(){
    whenToUpgrade = StaticClassRef.LEVEL3;
  }

  public override string ClassDesc()
  {
    return "+2 hp\nSkeleKill";
  }

  public override string ClassName()
  {
      return "Lich";
  }

  public override ClassNode GetParent(){
      return new CthulhuNecromancerClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ new CthulhuUReaperClass(), new CthulhuPApostleClass() };
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
