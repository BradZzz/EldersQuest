using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CthulhuApparitionClass : ClassNode
{
  public CthulhuApparitionClass(){
    whenToUpgrade = StaticClassRef.LEVEL3;
  }

  public override string ClassDesc()
  {
    return "+1 hp\n+1 atk trn";
  }

  public override string ClassName()
  {
      return "Apparition";
  }

  public override ClassNode GetParent(){
      return new CthulhuStolisClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ new CthulhuNyarlathotepClass(), new CthulhuPRiderClass() };
  }
 
  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetMaxHP(unit.GetMaxHP() + 1);
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
