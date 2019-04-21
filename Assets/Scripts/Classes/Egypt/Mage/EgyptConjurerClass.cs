using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EgyptConjurerClass : ClassNode
{
  public EgyptConjurerClass(){
    whenToUpgrade = StaticClassRef.LEVEL2;
  }

  public override string ClassDesc()
  {
    return "+1 mv\nFireAtk";
  }

  public override string ClassName()
  {
      return "Conjurer";
  }

  public override ClassNode GetParent(){
      return new EgyptBaseMage();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ new EgyptElementalistClass(), new EgyptGeomancerClass()};
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetMoveSpeed(unit.GetMoveSpeed() + 1);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("FireAtk");
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
