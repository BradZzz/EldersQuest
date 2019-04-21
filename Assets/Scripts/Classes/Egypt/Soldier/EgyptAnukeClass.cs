using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EgyptAnukeClass : ClassNode
{
  public EgyptAnukeClass(){
    whenToUpgrade = StaticClassRef.LEVEL3;
  }

  public override string ClassDesc()
  {
      return "+2 hp\n+1 atk trn";
  }

  public override string ClassName()
  {
      return "Anuke";
  }

  public override ClassNode GetParent(){
      return new EgyptNomadClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ new EgyptAnMessiahClass(), new EgyptMSandsClass()};
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetMaxHP(unit.GetMaxHP() + 2);
      unit.SetTurnAttacks(unit.GetTurnAttacks() + 1);
      return unit;
  }

  public override string ClassInactiveDesc(){
      return "+1 atk turn battle";
  }

  public override Unit InactiveUpgradeCharacter(Unit unit)
  {
      unit.SetTurnAttacks(unit.GetTurnAttackBuff() + 1);
      return unit;
  }
}
