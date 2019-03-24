using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HumanGeneralClass : ClassNode
{
  public HumanGeneralClass(){
    whenToUpgrade = StaticClassRef.LEVEL4;
  }

  public override string ClassDesc()
  {
      return "+1 atk trn\n+1 atk";
  }

  public override string ClassName()
  {
      return "General";
  }

  public override ClassNode GetParent(){
      return new HumanCaptainClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetAttack(unit.GetAttack() + 1);
      unit.SetTurnAttacks(unit.GetTurnAttacks() + 1);
      return unit;
  }
}
