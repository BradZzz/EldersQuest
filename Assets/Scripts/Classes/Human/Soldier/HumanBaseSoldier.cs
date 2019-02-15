using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HumanBaseSoldier : ClassNode
{
  public HumanBaseSoldier(){
    whenToUpgrade = 1;
  }

  public override string ClassDesc()
  {
    return "+1 atk\n+1 hp";
  }

  public override string ClassName()
  {
      return "Soldier";
  }

  public override ClassNode GetParent(){
      return null;
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ new HumanBerserkerClass(), new HumanPaladinClass() };
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetAttack(unit.GetAttack() + 1);
      unit.SetMaxHP(unit.GetMaxHP() + 1);
      return unit;
  }
}
