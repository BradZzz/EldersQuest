using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HumanIWarlockClass : ClassNode
{
  public HumanIWarlockClass(){
    whenToUpgrade = StaticClassRef.LEVEL2;
  }

  public override string ClassDesc()
  {
    return "+1 hp\n+1 mv trn";
  }

  public override string ClassName()
  {
      return "Inifinity Warlock";
  }

  public override ClassNode GetParent(){
      return new HumanBaseMage();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ new HumanWizardClass(), new HumanArchMageClass() };
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      int hp = unit.GetMaxHP();
      unit.SetMaxHP(hp + 1);
      unit.SetTurnMoves(unit.GetTurnMoves() + 1);
      return unit;
  }

  public override string ClassInactiveDesc(){
      return "+1 exp inactive";
  }

  public override Unit InactiveUpgradeCharacter(Unit unit)
  {
      unit.SetInactiveExpBuff(unit.GetInactiveExpBuff() + 1);
      return unit;
  }
}
