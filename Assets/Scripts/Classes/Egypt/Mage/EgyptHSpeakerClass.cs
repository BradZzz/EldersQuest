using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EgyptHSpeakerClass : ClassNode
{
  public EgyptHSpeakerClass(){
    whenToUpgrade = StaticClassRef.LEVEL4;
  }

  public override string ClassDesc()
  {
    return "AoeAtk\nFireAtk";
  }

  public override string ClassName()
  {
      return "Hell Speaker";
  }

  public override ClassNode GetParent(){
      return new EgyptGeomancerClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }
 
  public override Unit UpgradeCharacter(Unit unit)
  {
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("AoeAtk");
      skills.Add("FireAtk");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}
