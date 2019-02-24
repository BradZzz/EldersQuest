using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EgyptNSpeakerClass : ClassNode
{
  public EgyptNSpeakerClass(){
    whenToUpgrade = StaticClassRef.LEVEL4;
  }

  public override string ClassDesc()
  {
    return "+1 atk\nAegisKill";
  }

  public override string ClassName()
  {
      return "Nexus Speaker";
  }

  public override ClassNode GetParent(){
      return new EgyptRChosenClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }
 
  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetAttack(unit.GetAttack() + 1);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("AegisKill");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}
