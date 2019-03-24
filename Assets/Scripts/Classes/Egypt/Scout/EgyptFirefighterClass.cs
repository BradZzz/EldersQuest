using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EgyptFirefighterClass : ClassNode
{
  public EgyptFirefighterClass(){
    whenToUpgrade = StaticClassRef.LEVEL4;
  }

  public override string ClassDesc()
  {
    return "+1 atk trn\nSnowAtk\nSnowAtk";
  }

  public override string ClassName()
  {
      return "Firefighter";
  }

  public override ClassNode GetParent(){
      return new EgyptArsonistClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }
 
  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetTurnAttacks(unit.GetTurnAttacks() + 1);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("SnowAtk");
      skills.Add("SnowAtk");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}
