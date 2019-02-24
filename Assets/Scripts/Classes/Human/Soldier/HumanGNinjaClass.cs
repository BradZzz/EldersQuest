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
      return "+3 mv";
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
      unit.SetMoveSpeed(unit.GetMoveSpeed() + 3);
      return unit;
  }
}
