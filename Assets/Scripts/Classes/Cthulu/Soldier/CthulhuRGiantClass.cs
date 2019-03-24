using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CthulhuRGiantClass : ClassNode
{
  public CthulhuRGiantClass(){
    whenToUpgrade = StaticClassRef.LEVEL3;
  }

  public override string ClassDesc()
  {
    return "BideWait\nRageDef";
  }

  public override string ClassName()
  {
      return "Rage Giant";
  }

  public override ClassNode GetParent(){
      return new CthulhuGoliathClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ new CthulhuADeathClass(), new CthulhuWRiderClass()};
  }

 
  public override Unit UpgradeCharacter(Unit unit)
  {
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("RageDef");
      skills.Add("BideWait");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}
