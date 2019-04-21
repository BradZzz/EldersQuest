using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CthulhuFurymancerClass : ClassNode
{
  public CthulhuFurymancerClass(){
    whenToUpgrade = StaticClassRef.LEVEL3;
  }

  public override string ClassDesc()
  {
    return "+1 mv\n+1 atk trn";
  }

  public override string ClassName()
  {
      return "Furymancer";
  }

  public override ClassNode GetParent(){
      return new CthulhuNecromancerClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ new CthulhuPGoblinClass(), new CthulhuGDispatcherClass() };
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetMoveSpeed(unit.GetMoveSpeed() + 1);
      unit.SetTurnAttacks(unit.GetTurnAttacks() + 1);
      return unit;
  }

  public override string ClassInactiveDesc(){
      return "+2 exp inactive";
  }

  public override Unit InactiveUpgradeCharacter(Unit unit)
  {
      unit.SetInactiveExpBuff(unit.GetInactiveExpBuff() + 2);
      return unit;
  }
}
