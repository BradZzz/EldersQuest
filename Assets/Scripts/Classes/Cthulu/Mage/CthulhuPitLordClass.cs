using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CthulhuPitLordClass : ClassNode
{
  public CthulhuPitLordClass(){
    whenToUpgrade = 12;
  }

  public override string ClassDesc()
  {
    return "+1 atk rng\nFireKill";
  }

  public override string ClassName()
  {
      return "Pit Lord";
  }

  public override ClassNode GetParent(){
      return new CthulhuLesserDemonClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }
 
  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetAtkRange(unit.GetAtkRange() + 1);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("FireKill");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}
