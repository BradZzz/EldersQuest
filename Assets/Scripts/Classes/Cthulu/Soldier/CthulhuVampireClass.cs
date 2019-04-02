using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CthulhuVampireClass : ClassNode
{
  public CthulhuVampireClass(){
    whenToUpgrade = StaticClassRef.LEVEL4;
  }

  public override string ClassDesc()
  {
    return "WispKill\nWispKill";
  }

  public override string ClassName()
  {
      return "Vampire";
  }

  public override ClassNode GetParent(){
      return new CthulhuAVamprossClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }

 
  public override Unit UpgradeCharacter(Unit unit)
  {
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("WispKill");
      skills.Add("WispKill");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}
