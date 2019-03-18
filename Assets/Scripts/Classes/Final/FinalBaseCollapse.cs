using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FinalBaseCollapse : ClassNode
{
  public FinalBaseCollapse(){
    whenToUpgrade = StaticClassRef.LEVEL3;
  }

  public override string ClassDesc()
  {
    return "+1 hp\n+1 atk trn\n+1 mv trn";
  }

  public override string ClassName()
  {
      return "Collapse";
  }

  public override ClassNode GetParent(){
      return new FinalBaseChaos();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ new FinalBaseApocalypse() };
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetTurnMoves(unit.GetTurnMoves() + 1);
      unit.SetMaxHP(unit.GetMaxHP() + 1);
      unit.SetTurnAttacks(unit.GetTurnAttacks() + 1);
      return unit;
  }
}
