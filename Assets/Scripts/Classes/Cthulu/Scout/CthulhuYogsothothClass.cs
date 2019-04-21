using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CthulhuYogsothothClass : ClassNode
{
  public CthulhuYogsothothClass(){
    whenToUpgrade = StaticClassRef.LEVEL4;
  }

  public override string ClassDesc()
  {
    return "+1 hp\n+1 atk trn";
  }

  public override string ClassName()
  {
      return "Yog-Sothoth";
  }

  public override ClassNode GetParent(){
      return new CthulhuDEaterClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }
 
  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetTurnAttacks(unit.GetTurnAttacks() + 1);
      unit.SetMaxHP(unit.GetMaxHP());
      return unit;
  }

  public override string ClassInactiveDesc(){
      return "+2 mv battle";
  }

  public override Unit InactiveUpgradeCharacter(Unit unit)
  {
      unit.SetMoveBuff(unit.GetMoveBuff() + 2);
      return unit;
  }
}
