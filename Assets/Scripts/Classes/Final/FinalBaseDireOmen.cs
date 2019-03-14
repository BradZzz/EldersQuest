using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FinalBaseDireOmen : ClassNode
{
  public FinalBaseDireOmen(){
    whenToUpgrade = StaticClassRef.LEVEL2;
  }

  public override string ClassDesc()
  {
    return "+1 hp\n+2 atk rng";
  }

  public override string ClassName()
  {
      return "Dire Omen";
  }

  public override ClassNode GetParent(){
      return null;
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ new HumanIWarlockClass() };
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetMaxHP(unit.GetMaxHP() + 1);
      unit.SetAtkRange(unit.GetAtkRange() + 2);
      return unit;
  }
}
