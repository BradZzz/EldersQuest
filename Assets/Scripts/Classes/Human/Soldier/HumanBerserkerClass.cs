using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HumanBerserkerClass : ClassNode
{
  public HumanBerserkerClass(){
    whenToUpgrade = 5;
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
      return new ClassNode[]{ };
  }

 
  public override Unit UpgradeCharacter(Unit unit)
  {
      int spd = unit.GetMoveSpeed();
      unit.SetMoveSpeed(spd + 1);
      unit.SetTurnAttacks(unit.GetTurnAttacks() + 1);
      return unit;
  }
}
