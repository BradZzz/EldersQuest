using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CthulhuBaseSoldier : ClassNode
{
  public CthulhuBaseSoldier(){
    whenToUpgrade = StaticClassRef.LEVEL1;
  }

  public override string ClassDesc()
  {
    return "+1 mv\n+1 atk trn\nHealKill";
  }

  public override string ClassName()
  {
      return "Soldier";
  }

  public override ClassNode GetParent(){
      return null;
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ new CthulhuGoliathClass(), new CthulhuVamprossClass() };
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetTurnAttacks(unit.GetTurnAttacks() + 1);
      unit.SetMoveSpeed(unit.GetMoveSpeed() + 1);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("HealKill");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}
