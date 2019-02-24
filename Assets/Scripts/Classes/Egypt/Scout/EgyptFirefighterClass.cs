using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EgyptFirefighterClass : ClassNode
{
  public EgyptFirefighterClass(){
    whenToUpgrade = StaticClassRef.LEVEL4;
  }

  public override string ClassDesc()
  {
    return "+1 atk\n+1 mv trn";
  }

  public override string ClassName()
  {
      return "Firefighter";
  }

  public override ClassNode GetParent(){
      return new EgyptArsonistClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }
 
  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetAttack(unit.GetAttack() + 1);
      unit.SetMoveSpeed(unit.GetMoveSpeed() + 1);
      return unit;
  }
}
