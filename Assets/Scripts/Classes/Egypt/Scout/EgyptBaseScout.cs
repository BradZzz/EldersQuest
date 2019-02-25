using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EgyptBaseScout : ClassNode
{
  public EgyptBaseScout(){
    whenToUpgrade = StaticClassRef.LEVEL1;
  }

  public override string ClassDesc()
  {
    return "+2 mv trn";
  }

  public override string ClassName()
  {
      return "Scout";
  }

  public override ClassNode GetParent(){
      return null;
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ new EgyptManglerClass(), new EgyptWhispererClass() };
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetTurnMoves(unit.GetTurnMoves() + 2);
      return unit;
  }
}
