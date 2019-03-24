using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CthulhuAVamprossClass : ClassNode
{
  public CthulhuAVamprossClass(){
    whenToUpgrade = StaticClassRef.LEVEL3;
  }

  public override string ClassDesc()
  {
    return "+1 hp\n+1 mv trn";
  }

  public override string ClassName()
  {
      return "Arch Vampross";
  }

  public override ClassNode GetParent(){
      return new CthulhuVamprossClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ new CthulhuVampireClass(), new CthulhuEssilexClass() };
  }

 
  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetMaxHP(unit.GetMaxHP() + 1);
      unit.SetTurnMoves(unit.GetTurnMoves() + 1);
      return unit;
  }
}
