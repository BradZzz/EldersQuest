using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HumanPMarineClass : ClassNode
{
  public HumanPMarineClass(){
    whenToUpgrade = StaticClassRef.LEVEL4;
  }

  public override string ClassDesc()
  {
    return "HealTurn\nAoeAtk\nAoeAtk";
  }

  public override string ClassName()
  {
      return "Plasma Marine";
  }

  public override ClassNode GetParent(){
      return new HumanMSergeantClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("HealTurn");
      skills.Add("AoeAtk");
      skills.Add("AoeAtk");
      unit.SetSkills(skills.ToArray());      
      return unit;
  }

  public override string ClassInactiveDesc(){
      return "+2 mv battle";
  }

  public override Unit InactiveUpgradeCharacter(Unit unit)
  {
      unit.SetMoveBuff(unit.GetMoveBuff() + 2);
      return unit;
  }
}
