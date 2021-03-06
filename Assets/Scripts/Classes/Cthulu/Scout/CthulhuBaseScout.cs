﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CthulhuBaseScout : ClassNode
{
  public CthulhuBaseScout(){
    whenToUpgrade = StaticClassRef.LEVEL1;
  }

  public override string ClassDesc()
  {
    return "+1 mv trn\nHealWait";
  }

  public override string ClassName()
  {
      return "Scout";
  }

  public override ClassNode GetParent(){
      return null;
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ new CthulhuStolisClass(), new CthulhuTrollClass() };
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetTurnMoves(unit.GetTurnMoves() + 1);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("HealWait");
      unit.SetSkills(skills.ToArray());
      return unit;
  }

  public override string ClassInactiveDesc(){
      return "+1 mv battle";
  }

  public override Unit InactiveUpgradeCharacter(Unit unit)
  {
      unit.SetMoveBuff(unit.GetMoveBuff() + 1);
      return unit;
  }
}
