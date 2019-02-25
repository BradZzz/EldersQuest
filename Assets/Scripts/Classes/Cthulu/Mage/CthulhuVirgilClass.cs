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
    return "+1 atk rng\nNullifyAtk";
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
      skills.Add("NullifyAtk");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}
