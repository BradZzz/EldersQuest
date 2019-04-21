using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FinalBaseDisorder : ClassNode
{
  public FinalBaseDisorder(){
    whenToUpgrade = StaticClassRef.LEVEL1;
  }

  public override string ClassDesc()
  {
    return "+2 hp\n+2 mv";
  }

  public override string ClassName()
  {
      return "Disorder";
  }

  public override ClassNode GetParent(){
      return null;
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ new FinalBaseChaos() };
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetMoveSpeed(unit.GetMoveSpeed() + 2);
      unit.SetMaxHP(unit.GetMaxHP() + 2);
      return unit;
  }

  public override string ClassInactiveDesc(){
      return "";
  }


  public override Unit InactiveUpgradeCharacter(Unit unit)
  {
      return unit;
  }
}
