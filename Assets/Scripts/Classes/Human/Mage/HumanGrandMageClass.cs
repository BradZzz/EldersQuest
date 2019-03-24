using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HumanGrandMageClass : ClassNode
{
  public HumanGrandMageClass(){
    whenToUpgrade = StaticClassRef.LEVEL4;
  }

  public override string ClassDesc()
  {
    return "+1 mv\n+1 atk trn";
  }

  public override string ClassName()
  {
      return "Grand Mage";
  }

  public override ClassNode GetParent(){
      return new HumanArchMageClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetTurnAttacks(unit.GetTurnAttacks() + 1);
      unit.SetMoveSpeed(unit.GetMoveSpeed() + 1);
      return unit;
  }
}
