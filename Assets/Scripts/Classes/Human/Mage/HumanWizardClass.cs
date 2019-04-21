using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HumanWizardClass : ClassNode
{
  public HumanWizardClass(){
    whenToUpgrade = StaticClassRef.LEVEL3;
  }

  public override string ClassDesc()
  {
    return "+2 atk";
  }

  public override string ClassName()
  {
      return "Wizard";
  }

  public override ClassNode GetParent(){
      return new HumanIWarlockClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ new HumanGigaWizardClass(), new HumanIceWizardClass() };
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetAttack(unit.GetAttack() + 2);
      return unit;
  }

  public override string ClassInactiveDesc(){
      return "+1 atk battle";
  }

  public override Unit InactiveUpgradeCharacter(Unit unit)
  {
      unit.SetAttackBuff(unit.GetAttackBuff() + 1);
      return unit;
  }
}
