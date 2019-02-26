using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HumanGNinjaClass : ClassNode
{
  public HumanGNinjaClass(){
    whenToUpgrade = StaticClassRef.LEVEL4;
  }

  public override string ClassDesc()
  {
      return "+1 mv\n+1 mv trn";
  }

  public override string ClassName()
  {
      return "Gun Ninja";
  }

  public override ClassNode GetParent(){
      return new HumanGDancerClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetMoveSpeed(unit.GetMoveSpeed() + 1);
      unit.SetTurnMoves(unit.GetTurnMoves() + 1);
      return unit;
  }
}
