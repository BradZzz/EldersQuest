using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HumanMSergeantClass : ClassNode
{
  public HumanMSergeantClass(){
    whenToUpgrade = StaticClassRef.LEVEL3;
  }

  public override string ClassDesc()
  {
    return "+1 atk\n+1 mv";
  }

  public override string ClassName()
  {
      return "Master Sergeant";
  }

  public override ClassNode GetParent(){
      return new HumanFSergeantClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ new HumanPMarineClass(), new HumanQEngineerClass() };
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetAttack(unit.GetAttack() + 1);
      unit.SetMoveSpeed(unit.GetMoveSpeed() + 1);
      return unit;
  }

  public override string ClassInactiveDesc(){
      return "+2 mv battle";
  }

  public override Unit InactiveUpgradeCharacter(Unit unit)
  {
      unit.SetMoveBuff(unit.GetMoveBuff() + 2);
      return unit;
  }
}
