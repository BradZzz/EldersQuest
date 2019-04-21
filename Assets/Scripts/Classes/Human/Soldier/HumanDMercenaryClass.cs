using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HumanDMercenaryClass : ClassNode
{
  public HumanDMercenaryClass(){
    whenToUpgrade = StaticClassRef.LEVEL4;
  }

  public override string ClassDesc()
  {
      return "+1 atk\n+1 mv";
  }

  public override string ClassName()
  {
      return "Dire Mercenary";
  }

  public override ClassNode GetParent(){
      return new HumanFMarineClass();
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

  public override string ClassInactiveDesc(){
      return "+1 atk turn battle";
  }

  public override Unit InactiveUpgradeCharacter(Unit unit)
  {
      unit.SetTurnAttackBuff(unit.GetTurnAttackBuff() + 1);
      return unit;
  }
}
