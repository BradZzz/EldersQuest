using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EgyptVWhispererClass : ClassNode
{
  public EgyptVWhispererClass(){
    whenToUpgrade = StaticClassRef.LEVEL4;
  }

  public override string ClassDesc()
  {
    return "+2 atk trn";
  }

  public override string ClassName()
  {
      return "Void Whisperer";
  }

  public override ClassNode GetParent(){
      return new EgyptElementalistClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }
 
  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetTurnAttacks(unit.GetTurnAttacks() + 2);
      return unit;
  }
}
