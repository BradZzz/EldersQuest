﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CthulhuDevilClass : ClassNode
{
  public CthulhuDevilClass(){
    whenToUpgrade = StaticClassRef.LEVEL4;
  }

  public override string ClassDesc()
  {
    return "+1 atk\nAegisTurn";
  }

  public override string ClassName()
  {
      return "Devil";
  }

  public override ClassNode GetParent(){
      return new CthulhuGrandDemonClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }
 
  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetAttack(unit.GetAttack() + 1);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("AegisTurn");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}