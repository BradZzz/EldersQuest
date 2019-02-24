using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EgyptAshClass : ClassNode
{
  public EgyptAshClass(){
    whenToUpgrade = StaticClassRef.LEVEL3;
  }

  public override string ClassDesc()
  {
      return "+1 atk\n+1 mv trn";
  }

  public override string ClassName()
  {
      return "Ash";
  }

  public override ClassNode GetParent(){
      return new EgyptNomadClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ new EgyptAMessiahClass(), new EgyptSActorClass() };
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetAttack(unit.GetAttack() + 1);
      unit.SetTurnMoves(unit.GetTurnMoves() + 1);
      return unit;
  }
}
