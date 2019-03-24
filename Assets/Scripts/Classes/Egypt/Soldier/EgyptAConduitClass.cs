using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EgyptAConduitClass : ClassNode
{
  public EgyptAConduitClass(){
    whenToUpgrade = StaticClassRef.LEVEL4;
  }

  public override string ClassDesc()
  {
      return "+2 atk";
  }

  public override string ClassName()
  {
      return "Aeon Conduit";
  }

  public override ClassNode GetParent(){
      return new EgyptHapyClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetAttack(unit.GetAttack() + 2);
      return unit;
  }
}
