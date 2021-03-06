﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EgyptHMessiahClass : ClassNode
{
  public EgyptHMessiahClass(){
    whenToUpgrade = StaticClassRef.LEVEL4;
  }

  public override string ClassDesc()
  {
      return "+1 mv trn\n+1 hp\nThornDef";
  }

  public override string ClassName()
  {
      return "Hapy Messiah";
  }

  public override ClassNode GetParent(){
      return new EgyptHapyClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetTurnMoves(unit.GetTurnMoves() + 1);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("ThornDef");
      unit.SetSkills(skills.ToArray());
      return unit;
  }

  public override string ClassInactiveDesc(){
      return "ThornDef";
  }

  public override Unit InactiveUpgradeCharacter(Unit unit)
  {
      unit.SetSkillsBuffs(new string[]{ "ThornDef" });
      return unit;
  }
}
