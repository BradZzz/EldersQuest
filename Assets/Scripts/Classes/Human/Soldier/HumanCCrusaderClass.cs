using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HumanCCrusaderClass : ClassNode
{
  public HumanCCrusaderClass(){
    whenToUpgrade = StaticClassRef.LEVEL4;
  }

  public override string ClassDesc()
  {
      return "+1 atk turn";
  }

  public override string ClassName()
  {
      return "Crystal Crusader";
  }

  public override ClassNode GetParent(){
      return new HumanGPaladinClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetTurnAttacks(unit.GetTurnAttacks() + 1);
      return unit;
  }
}
