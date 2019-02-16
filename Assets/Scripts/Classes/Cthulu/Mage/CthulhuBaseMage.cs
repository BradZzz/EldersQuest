using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CthulhuBaseMage : ClassNode
{
  public CthulhuBaseMage(){
    whenToUpgrade = StaticClassRef.LEVEL1;
  }

  public override string ClassDesc()
  {
    return "+1 mv\n+1 atk rng";
  }

  public override string ClassName()
  {
      return "Mage";
  }

  public override ClassNode GetParent(){
      return null;
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ new CthulhuLesserDemonClass(), new CthulhuNecromancerClass() };
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetMoveSpeed(unit.GetMoveSpeed() + 1);
      unit.SetAtkRange(unit.GetAtkRange() + 1);
      return unit;
  }
}
