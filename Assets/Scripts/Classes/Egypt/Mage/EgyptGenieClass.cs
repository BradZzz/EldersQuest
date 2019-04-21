using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EgyptGenieClass : ClassNode
{
  public EgyptGenieClass(){
    whenToUpgrade = StaticClassRef.LEVEL3;
  }

  public override string ClassDesc()
  {
    return "+2 mv\n+1 atk";
  }

  public override string ClassName()
  {
      return "Genie";
  }

  public override ClassNode GetParent(){
      return new EgyptDjinnClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ new EgyptWMasterClass(), new EgyptRSpectreClass() };
  }
 
  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetMoveSpeed(unit.GetMoveSpeed() + 2);
      unit.SetAttack(unit.GetAttack() + 1);
      return unit;
  }

  public override string ClassInactiveDesc(){
      return "+2 exp inactive";
  }

  public override Unit InactiveUpgradeCharacter(Unit unit)
  {
      unit.SetInactiveExpBuff(unit.GetInactiveExpBuff() + 2);
      return unit;
  }
}
