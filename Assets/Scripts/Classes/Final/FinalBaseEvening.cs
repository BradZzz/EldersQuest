using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FinalBaseEvening : ClassNode
{
  public FinalBaseEvening(){
    whenToUpgrade = StaticClassRef.LEVEL2;
  }

  public override string ClassDesc()
  {
    return "+2 hp\n+3 mv";
  }

  public override string ClassName()
  {
      return "Evening";
  }

  public override ClassNode GetParent(){
      return new FinalBaseTwilight();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ new FinalBaseDarkness() };
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetMoveSpeed(unit.GetMoveSpeed() + 3);
      unit.SetMaxHP(unit.GetMaxHP() + 2);
      return unit;
  }
}
