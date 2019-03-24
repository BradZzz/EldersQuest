using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EgyptPChannelerClass : ClassNode
{
  public EgyptPChannelerClass(){
    whenToUpgrade = StaticClassRef.LEVEL4;
  }

  public override string ClassDesc()
  {
    return "WarpAtk\nRageWait";
  }

  public override string ClassName()
  {
      return "Power Channeler";
  }

  public override ClassNode GetParent(){
      return new EgyptRChosenClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }
 
  public override Unit UpgradeCharacter(Unit unit)
  {
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("WarpAtk");
      skills.Add("RageWait");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}
