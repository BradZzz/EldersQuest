using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CthulhuLesserDemonClass : ClassNode
{
  public CthulhuLesserDemonClass(){
    whenToUpgrade = 7;
  }

  public override string ClassDesc()
  {
    return "FireKill\nFireMove";
  }

  public override string ClassName()
  {
      return "Lesser Demon";
  }

  public override ClassNode GetParent(){
      return new CthulhuBaseMage();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ new CthulhuGrandDemonClass(), new CthulhuPitLordClass() };
  }
 
  public override Unit UpgradeCharacter(Unit unit)
  {
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("FireMove");
      skills.Add("FireKill");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}
