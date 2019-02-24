using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HumanLieutenantClass : ClassNode
{
  public HumanLieutenantClass(){
    whenToUpgrade = StaticClassRef.LEVEL3;
  }

  public override string ClassDesc()
  {
    return "+1 atk\n+1 mv trn";
  }

  public override string ClassName()
  {
      return "Lieutenant";
  }

  public override ClassNode GetParent(){
      return new HumanCorporalClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ new HumanArcaneCommanderClass(), new HumanTankCommanderClass() };
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetAttack(unit.GetAttack() + 1);
      unit.SetMoveSpeed(unit.GetMoveSpeed() + 1);
      return unit;
  }
}
