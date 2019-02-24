using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CthulhuJudasClass : ClassNode
{
  public CthulhuJudasClass(){
    whenToUpgrade = StaticClassRef.LEVEL4;
  }

  public override string ClassDesc()
  {
    return "FireMove\nRageMove";
  }

  public override string ClassName()
  {
      return "Judas";
  }

  public override ClassNode GetParent(){
      return new CthulhuPitLordClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }
 
  public override Unit UpgradeCharacter(Unit unit)
  {
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("FireMove");
      skills.Add("RageMove");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}
