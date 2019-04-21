using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HumanTinkererClass : ClassNode
{
  public HumanTinkererClass(){
    whenToUpgrade = StaticClassRef.LEVEL3;
  }

  public override string ClassDesc()
  {
    return "+1 atk trn\n+2 mv";
  }

  public override string ClassName()
  {
      return "Tinkerer";
  }

  public override ClassNode GetParent(){
      return new HumanTechnomancerClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ new HumanBramblelockClass(), new HumanKootClass() };
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetTurnAttacks(unit.GetTurnAttacks() + 1);
      unit.SetMoveSpeed(unit.GetMoveSpeed() + 2);
      return unit;
  }

  public override string ClassInactiveDesc(){
      return "+1 atk turn battle";
  }

  public override Unit InactiveUpgradeCharacter(Unit unit)
  {
      unit.SetTurnAttackBuff(unit.GetTurnAttackBuff() + 1);
      return unit;
  }
}
