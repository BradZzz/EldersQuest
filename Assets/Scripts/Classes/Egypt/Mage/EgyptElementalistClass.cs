using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EgyptElementalistClass : ClassNode
{
  public EgyptElementalistClass(){
    whenToUpgrade = StaticClassRef.LEVEL3;
  }

  public override string ClassDesc()
  {
    return "+1 mv trn\n+1 hp";
  }

  public override string ClassName()
  {
      return "Elementalist";
  }

  public override ClassNode GetParent(){
      return new EgyptConjurerClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ new EgyptVWhispererClass(), new EgyptTConduitClass() };
  }
 
  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetTurnMoves(unit.GetTurnMoves() + 1);
      unit.SetMaxHP(unit.GetMaxHP() + 1);
      return unit;
  }

  public override string ClassInactiveDesc(){
      return "+2 exp inactive";
  }

  public override Unit InactiveUpgradeCharacter(Unit unit)
  {
      unit.SetInactiveExpBuff(unit.GetInactiveExpBuff() + 2);
      return unit;
  }
}
