using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HumanMetalmancerClass : ClassNode
{
  public HumanMetalmancerClass(){
    whenToUpgrade = StaticClassRef.LEVEL4;
  }

  public override string ClassDesc()
  {
    return "+2 hp\nFireAtk";
  }

  public override string ClassName()
  {
      return "Metalmancer";
  }

  public override ClassNode GetParent(){
      return new HumanArchMageClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("FireAtk");
      unit.SetSkills(skills.ToArray());
      unit.SetMaxHP(unit.GetMaxHP() + 2);
      return unit;
  }

  public override string ClassInactiveDesc(){
      return "+3 hp battle";
  }

  public override Unit InactiveUpgradeCharacter(Unit unit)
  {
      unit.SetHpBuffInactive(unit.GetHpBuff() + 3);
      return unit;
  }
}
