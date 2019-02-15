using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EgyptNomadClass : ClassNode
{
  public EgyptNomadClass(){
    whenToUpgrade = 6;
  }

  public override string ClassDesc()
  {
    return "+1 hp\n+1 atk";
  }

  public override string ClassName()
  {
      return "Nomad";
  }

  public override ClassNode GetParent(){
      return new EgyptBaseSoldier();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }

 
  public override Unit UpgradeCharacter(Unit unit)
  {
      int hp = unit.GetMaxHP();
      unit.SetMaxHP(hp + 1);
      unit.SetAttack(unit.GetAttack() + 1);
      return unit;
  }
}
