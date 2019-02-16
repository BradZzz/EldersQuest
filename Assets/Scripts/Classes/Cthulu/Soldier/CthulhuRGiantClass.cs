using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CthulhuRGiantClass : ClassNode
{
  public CthulhuRGiantClass(){
    whenToUpgrade = 6;
  }

  public override string ClassDesc()
  {
    return "+2 hp\nRageDef";
  }

  public override string ClassName()
  {
      return "Rage Giant";
  }

  public override ClassNode GetParent(){
      return new CthulhuGoliathClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }

 
  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetMaxHP(unit.GetMaxHP() + 2);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("RageDef");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}
