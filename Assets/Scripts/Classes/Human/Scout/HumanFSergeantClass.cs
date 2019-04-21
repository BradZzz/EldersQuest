using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HumanFSergeantClass : ClassNode
{
  public HumanFSergeantClass(){
    whenToUpgrade = StaticClassRef.LEVEL2;
  }

  public override string ClassDesc()
  {
    return "+1 hp\nHealAlliesAtk";
  }

  public override string ClassName()
  {
      return "Field Sergeant";
  }

  public override ClassNode GetParent(){
      return new HumanBaseScout();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ new HumanCSergeantClass(), new HumanMSergeantClass() };
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      int maxHp = unit.GetMaxHP();
      unit.SetMaxHP(maxHp + 1);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("HealAlliesAtk");
      unit.SetSkills(skills.ToArray());
      return unit;
  }

  public override string ClassInactiveDesc(){
      return "+1 mv battle";
  }

  public override Unit InactiveUpgradeCharacter(Unit unit)
  {
      unit.SetMoveBuff(unit.GetMoveBuff() + 1);
      return unit;
  }
}
