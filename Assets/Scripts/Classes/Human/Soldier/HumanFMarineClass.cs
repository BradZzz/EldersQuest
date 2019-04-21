using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HumanFMarineClass : ClassNode
{
  public HumanFMarineClass(){
    whenToUpgrade = StaticClassRef.LEVEL3;
  }

  public override string ClassDesc()
  {
      return "+1 atk trn";
  }

  public override string ClassName()
  {
      return "Forward Marine";
  }

  public override ClassNode GetParent(){
      return new HumanBerserkerClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ new HumanBloodBenderClass(), new HumanDMercenaryClass() };
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetTurnAttacks(unit.GetTurnAttacks() + 1);
      return unit;
  }

  public override string ClassInactiveDesc(){
      return "+1 atk turn battle";
  }

  public override Unit InactiveUpgradeCharacter(Unit unit)
  {
      unit.SetTurnAttackBuff(unit.GetTurnAttackBuff() + 1);
      return unit;
  }
}
