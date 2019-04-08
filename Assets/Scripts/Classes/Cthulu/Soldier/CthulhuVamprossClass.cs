using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CthulhuVamprossClass : ClassNode
{
  public CthulhuVamprossClass(){
    whenToUpgrade = StaticClassRef.LEVEL2;
  }

  public override string ClassDesc()
  {
    return "+2 hp\nHealTurn";
  }

  public override string ClassName()
  {
      return "Vampross";
  }

  public override ClassNode GetParent(){
      return new CthulhuBaseSoldier();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ new CthulhuAVamprossClass(), new CthulhuSuccubusClass() };
  }

 
  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetMaxHP(unit.GetMaxHP() + 2);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("HealTurn");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}
