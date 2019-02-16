using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EgyptManglerClass : ClassNode
{
  public EgyptManglerClass(){
    whenToUpgrade = StaticClassRef.LEVEL2;
  }

  public override string ClassDesc()
  {
    return "+1 mv trn";
  }

  public override string ClassName()
  {
      return "Mangler";
  }

  public override ClassNode GetParent(){
      return new EgyptBaseScout();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ new EgyptFBenderClass(), new EgyptSSenseiClass() };
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetTurnMoves(unit.GetTurnMoves() + 1);
      return unit;
  }
}
