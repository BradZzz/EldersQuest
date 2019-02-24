using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EgyptEMoverClass : ClassNode
{
  public EgyptEMoverClass(){
    whenToUpgrade = StaticClassRef.LEVEL4;
  }

  public override string ClassDesc()
  {
    return "+1 hp\nFireDef";
  }

  public override string ClassName()
  {
      return "Earth Mover";
  }

  public override ClassNode GetParent(){
      return new EgyptGeomancerClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }
 
  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetMaxHP(unit.GetMaxHP() + 1);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("FireDef");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}
