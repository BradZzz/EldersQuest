using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HumanBerserkerClass : ClassNode
{
  public HumanBerserkerClass(){
    whenToUpgrade = StaticClassRef.LEVEL2;
  }

  public override string ClassDesc()
  {
    return "+1 mv\n+1 atk trn";
  }

  public override string ClassName()
  {
      return "Berserker";
  }

  public override ClassNode GetParent(){
      return new HumanBaseSoldier();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ new HumanFMarineClass(), new HumanGDancerClass() };
  }

 
  public override Unit UpgradeCharacter(Unit unit)
  {
      int spd = unit.GetMoveSpeed();
      unit.SetMoveSpeed(spd + 1);
      unit.SetTurnAttacks(unit.GetTurnAttacks() + 1);
      return unit;
  }

  public override string ClassInactiveDesc(){
      return "+1 hp battle";
  }

  public override Unit InactiveUpgradeCharacter(Unit unit)
  {
      unit.SetHpBuffInactive(unit.GetHpBuff() + 1);
      return unit;
  }
}
