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
    return "AoeAtk\n+1 atk rng";
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
      unit.SetAtkRange(unit.GetAtkRange() + 1);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("AoeAtk");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}
