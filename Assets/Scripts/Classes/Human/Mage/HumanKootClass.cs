using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HumanKootClass : ClassNode
{
  public HumanKootClass(){
    whenToUpgrade = StaticClassRef.LEVEL4;
  }

  public override string ClassDesc()
  {
    return "+1 mv trn\nBideKill";
  }

  public override string ClassName()
  {
      return "Koot";
  }

  public override ClassNode GetParent(){
      return new HumanTinkererClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetTurnMoves(unit.GetTurnMoves() + 1);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("BideKill");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}
