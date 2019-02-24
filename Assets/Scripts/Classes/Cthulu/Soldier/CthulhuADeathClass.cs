using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CthulhuADeathClass : ClassNode
{
  public CthulhuADeathClass(){
    whenToUpgrade = StaticClassRef.LEVEL4;
  }

  public override string ClassDesc()
  {
    return "+1 atk\nBideKill\nBideKill";
  }

  public override string ClassName()
  {
      return "Angel of Death";
  }

  public override ClassNode GetParent(){
      return new CthulhuRGiantClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }

 
  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetAttack(unit.GetAttack() + 1);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("BideKill");
      skills.Add("BideKill");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}
