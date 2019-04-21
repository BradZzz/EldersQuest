using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HumanCaptainClass : ClassNode
{
  public HumanCaptainClass(){
    whenToUpgrade = StaticClassRef.LEVEL3;
  }

  public override string ClassDesc()
  {
      return "+3 hp";
  }

  public override string ClassName()
  {
      return "Captain";
  }

  public override ClassNode GetParent(){
      return new HumanCorporalClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ new HumanGeneralClass(), new HumanFlankCaptainClass() };
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetMaxHP(unit.GetMaxHP() + 3);
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
