using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CthulhuApparitionClass : ClassNode
{
  public CthulhuApparitionClass(){
    whenToUpgrade = 6;
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
      return new ClassNode[]{ };
  }
 
  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetMaxHP(unit.GetMaxHP() + 1);
      unit.SetTurnAttacks(unit.GetTurnAttacks() + 1);
      return unit;
  }
}
