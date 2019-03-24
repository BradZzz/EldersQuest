using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CthulhuADeathClass : ClassNode
{
  public CthulhuADeathClass(){
    whenToUpgrade = StaticClassRef.LEVEL4;
  }

  public override string ClassDesc()
  {
    return "+5 mv";
  }

  public override string ClassName()
  {
      return "Angel of Death";
  }

  public override ClassNode GetParent(){
      return new CthulhuRGiantClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }

 
  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetMoveSpeed(unit.GetMoveSpeed() + 5);
      return unit;
  }
}
