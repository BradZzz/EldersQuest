using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EgyptDjinnClass : ClassNode
{
  public EgyptDjinnClass(){
    whenToUpgrade = 7;
  }

  public override string ClassDesc()
  {
    return "+1 mv trn\nAoeAtk";
  }

  public override string ClassName()
  {
      return "Djinn";
  }

  public override ClassNode GetParent(){
      return new EgyptBaseMage();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }
 
  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetTurnMoves(unit.GetTurnMoves() + 1);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("AoeAtk");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}
