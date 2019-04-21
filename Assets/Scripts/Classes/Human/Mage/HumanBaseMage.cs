using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HumanBaseMage : ClassNode
{
  public HumanBaseMage(){
    whenToUpgrade = StaticClassRef.LEVEL1;
  }

  public override string ClassDesc()
  {
    return "+1 atk\n+1 atk rng";
  }

  public override string ClassName()
  {
      return "Mage";
  }

  public override ClassNode GetParent(){
      return null;
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ new HumanIWarlockClass(), new HumanTechnomancerClass() };
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      int atk = unit.GetAttack();
      unit.SetAttack(atk + 1);
      unit.SetAtkRange(unit.GetAtkRange() + 1);
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
