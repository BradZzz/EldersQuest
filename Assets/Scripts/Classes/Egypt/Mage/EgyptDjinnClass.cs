using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EgyptDjinnClass : ClassNode
{
  public EgyptDjinnClass(){
    whenToUpgrade = StaticClassRef.LEVEL2;
  }

  public override string ClassDesc()
  {
    return "+1 atk rng\nAoeAtk";
  }

  public override string ClassName()
  {
      return "Djinn";
  }

  public override ClassNode GetParent(){
      return new EgyptBaseMage();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ new EgyptGenieClass(), new EgyptKoboldClass()};
  }
 
  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetAtkRange(unit.GetAtkRange() + 1);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("AoeAtk");
      unit.SetSkills(skills.ToArray());
      return unit;
  }

  public override string ClassInactiveDesc(){
      return "+1 exp inactive";
  }

  public override Unit InactiveUpgradeCharacter(Unit unit)
  {
      unit.SetInactiveExpBuff(unit.GetInactiveExpBuff() + 1);
      return unit;
  }
}
