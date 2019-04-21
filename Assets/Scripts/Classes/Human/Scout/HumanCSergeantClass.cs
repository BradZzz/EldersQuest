using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HumanCSergeantClass : ClassNode
{
  public HumanCSergeantClass(){
    whenToUpgrade = StaticClassRef.LEVEL3;
  }

  public override string ClassDesc()
  {
    return "+1 hp\nHealAlliesAtk";
  }

  public override string ClassName()
  {
      return "Command Sergeant";
  }

  public override ClassNode GetParent(){
      return new HumanFSergeantClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ new HumanPKnightClass(), new HumanMineSweeperClass() };
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetMaxHP(unit.GetMaxHP() + 1);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("HealAlliesAtk");
      unit.SetSkills(skills.ToArray());
      return unit;
  }

  public override string ClassInactiveDesc(){
      return "HealKill";
  }

  public override Unit InactiveUpgradeCharacter(Unit unit)
  {
      unit.SetSkillsBuffs(new string[]{ "HealKill" });
      return unit;
  }
}
