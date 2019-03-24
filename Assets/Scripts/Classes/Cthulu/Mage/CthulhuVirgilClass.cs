using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CthulhuVirgilClass : ClassNode
{
  public CthulhuVirgilClass(){
    whenToUpgrade = StaticClassRef.LEVEL4;
  }

  public override string ClassDesc()
  {
    return "BideKill\nHealKill";
  }

  public override string ClassName()
  {
      return "Virgil";
  }

  public override ClassNode GetParent(){
      return new CthulhuPitLordClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }
 
  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetAtkRange(unit.GetAtkRange() + 1);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("BideKill");
      skills.Add("HealKill");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}
