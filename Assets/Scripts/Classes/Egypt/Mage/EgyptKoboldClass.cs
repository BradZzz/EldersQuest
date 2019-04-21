using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EgyptKoboldClass : ClassNode
{
  public EgyptKoboldClass(){
    whenToUpgrade = StaticClassRef.LEVEL3;
  }

  public override string ClassDesc()
  {
    return "+2 hp\nThornDef";
  }

  public override string ClassName()
  {
      return "Kobold";
  }

  public override ClassNode GetParent(){
      return new EgyptDjinnClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ new EgyptTGoblinClass(), new EgyptBKoboldClass() };
  }
 
  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetMaxHP(unit.GetMaxHP() + 2);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("ThornDef");
      unit.SetSkills(skills.ToArray());
      return unit;
  }

  public override string ClassInactiveDesc(){
      return "+1 atk range battle";
  }

  public override Unit InactiveUpgradeCharacter(Unit unit)
  {
      unit.SetAttackRngBuff(unit.GetAttackBuff() + 1);
      return unit;
  }
}
