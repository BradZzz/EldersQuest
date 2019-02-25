﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CthulhuWRiderClass : ClassNode
{
  public CthulhuWRiderClass(){
    whenToUpgrade = StaticClassRef.LEVEL4;
  }

  public override string ClassDesc()
  {
    return "+1 mv trn\nRageWait";
  }

  public override string ClassName()
  {
      return "War Rider";
  }

  public override ClassNode GetParent(){
      return new CthulhuRGiantClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }

 
  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetTurnMoves(unit.GetTurnMoves() + 1);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("RageWait");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}