using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EgyptAnMessiahClass : ClassNode
{
  public EgyptAnMessiahClass(){
    whenToUpgrade = StaticClassRef.LEVEL4;
  }

  public override string ClassDesc()
  {
      return "+1 hp\n+1 atk trn";
  }

  public override string ClassName()
  {
      return "Anuke Messiah";
  }

  public override ClassNode GetParent(){
      return new EgyptAnukeClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetMaxHP(unit.GetMaxHP() + 1);
      unit.SetTurnAttacks(unit.GetTurnAttacks() + 1);
      return unit;
  }
}
