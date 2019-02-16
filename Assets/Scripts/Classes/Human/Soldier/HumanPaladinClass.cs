using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HumanPaladinClass : ClassNode
{
  public HumanPaladinClass(){
    whenToUpgrade = 6;
  }

  public override string ClassDesc()
  {
      return "+2hp";
  }

  public override string ClassName()
  {
      return "Paladin";
  }

  public override ClassNode GetParent(){
      return new HumanBaseSoldier();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ new HumanIPaladinClass(), new HumanGPaladinClass() };
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      int atk = unit.GetMaxHP();
      unit.SetMaxHP(atk + 2);
      return unit;
  }
}
