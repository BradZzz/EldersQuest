using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EgyptRSpectreClass : ClassNode
{
  public EgyptRSpectreClass(){
    whenToUpgrade = StaticClassRef.LEVEL4;
  }

  public override string ClassDesc()
  {
    return "+1 hp\nRageAlliesWait";
  }

  public override string ClassName()
  {
      return "Rage Spectre";
  }

  public override ClassNode GetParent(){
      return new EgyptGenieClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }
 
  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetMaxHP(unit.GetMaxHP() + 1);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("RageAlliesWait");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}
