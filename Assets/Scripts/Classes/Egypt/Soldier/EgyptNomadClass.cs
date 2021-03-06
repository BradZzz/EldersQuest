﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EgyptNomadClass : ClassNode
{
  public EgyptNomadClass(){
    whenToUpgrade = StaticClassRef.LEVEL2;
  }

  public override string ClassDesc()
  {
    return "+1 atk\n+1 hp";
  }

  public override string ClassName()
  {
      return "Nomad";
  }

  public override ClassNode GetParent(){
      return new EgyptBaseSoldier();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ new EgyptAshClass(), new EgyptAnukeClass() };
  }

 
  public override Unit UpgradeCharacter(Unit unit)
  {
      int hp = unit.GetMaxHP();
      unit.SetMaxHP(hp + 1);
      unit.SetAttack(unit.GetAttack() + 1);
      return unit;
  }

  public override string ClassInactiveDesc(){
      return "+1 hp battle";
  }

  public override Unit InactiveUpgradeCharacter(Unit unit)
  {
      unit.SetHpBuffInactive(unit.GetHpBuff() + 1);
      return unit;
  }
}
