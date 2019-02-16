using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CthulhuTrollClass : ClassNode
{
  public CthulhuTrollClass(){
    whenToUpgrade = StaticClassRef.LEVEL2;
  }

  public override string ClassDesc()
  {
    return "+1 hp\nHealTurn";
  }

  public override string ClassName()
  {
      return "Troll";
  }

  public override ClassNode GetParent(){
      return new CthulhuBaseScout();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ new CthulhuPBeastClass(), new CthulhuDEaterClass() };
  }
 
  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetMaxHP(unit.GetMaxHP() + 1);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("HealTurn");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}
