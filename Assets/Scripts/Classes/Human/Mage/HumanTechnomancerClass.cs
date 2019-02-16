using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HumanTechnomancerClass : ClassNode
{
  public HumanTechnomancerClass(){
    whenToUpgrade = StaticClassRef.LEVEL2;
  }

  public override string ClassDesc()
  {
    return "+1 mv trn\n+1 atk";
  }

  public override string ClassName()
  {
      return "Technomancer";
  }

  public override ClassNode GetParent(){
      return new HumanBaseMage();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ new HumanTinkererClass(), new HumanGeniusClass() };
  }
 
  public override Unit UpgradeCharacter(Unit unit)
  {
      int atk = unit.GetAttack();
      unit.SetAttack(atk + 1);
      unit.SetTurnMoves(unit.GetTurnMoves() + 1);
      return unit;
  }
}
