using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EgyptGeomancerClass : ClassNode
{
  public EgyptGeomancerClass(){
    whenToUpgrade = StaticClassRef.LEVEL3;
  }

  public override string ClassDesc()
  {
    return "+1 atk rng\nFireAtk";
  }

  public override string ClassName()
  {
      return "Geomancer";
  }

  public override ClassNode GetParent(){
      return new EgyptConjurerClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ new EgyptEMoverClass(), new EgyptHSpeakerClass() };
  }
 
  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetAtkRange(unit.GetAtkRange() + 1);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("FireAtk");
      unit.SetSkills(skills.ToArray());
      return unit;
  }

  public override string ClassInactiveDesc(){
      return "FireAtk";
  }

  public override Unit InactiveUpgradeCharacter(Unit unit)
  {
      unit.SetSkillsBuffs(new string[]{ "FireAtk" });
      return unit;
  }
}
