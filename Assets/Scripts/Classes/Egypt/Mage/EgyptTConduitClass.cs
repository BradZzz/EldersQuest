using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EgyptTConduitClass : ClassNode
{
  public EgyptTConduitClass(){
    whenToUpgrade = StaticClassRef.LEVEL4;
  }

  public override string ClassDesc()
  {
    return "+3 atk rng";
  }

  public override string ClassName()
  {
      return "Thunder Conduit";
  }

  public override ClassNode GetParent(){
      return new EgyptElementalistClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }
 
  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetAtkRange(unit.GetAtkRange() + 3);
      return unit;
  }
}
