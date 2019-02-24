using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CthulhuPApostleClass : ClassNode
{
  public CthulhuPApostleClass(){
    whenToUpgrade = StaticClassRef.LEVEL4;
  }

  public override string ClassDesc()
  {
    return "+2 atk\n+1 mv";
  }

  public override string ClassName()
  {
      return "Pact Apostle";
  }

  public override ClassNode GetParent(){
      return new CthulhuLichClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetAttack(unit.GetAttack() + 2);
      unit.SetMoveSpeed(unit.GetMoveSpeed() + 1);
      return unit;
  }
}
