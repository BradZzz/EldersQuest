using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CthulhuNyarlathotepClass : ClassNode
{
  public CthulhuNyarlathotepClass(){
    whenToUpgrade = StaticClassRef.LEVEL4;
  }

  public override string ClassDesc()
  {
    return "+1 atk trn\n+1 mv";
  }

  public override string ClassName()
  {
      return "Nyarlathotep";
  }

  public override ClassNode GetParent(){
      return new CthulhuApparitionClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }
 
  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetTurnAttacks(unit.GetTurnAttacks() + 1);
      unit.SetMoveSpeed(unit.GetMoveSpeed() + 1);
      return unit;
  }
}
