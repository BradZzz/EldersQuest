using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EgyptBMessiahClass : ClassNode
{
  public EgyptBMessiahClass(){
    whenToUpgrade = StaticClassRef.LEVEL4;
  }

  public override string ClassDesc()
  {
      return "+1 atk\n+1 hp\n+1 mv";
  }

  public override string ClassName()
  {
      return "Bes Messiah";
  }

  public override ClassNode GetParent(){
      return new EgyptBesClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetMoveSpeed(unit.GetMoveSpeed() + 1);
      unit.SetMaxHP(unit.GetMaxHP() + 1);
      unit.SetAttack(unit.GetAttack() + 1);
      return unit;
  }
}
