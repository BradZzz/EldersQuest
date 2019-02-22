using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EgyptFBenderClass : ClassNode
{
  public EgyptFBenderClass(){
    whenToUpgrade = StaticClassRef.LEVEL3;
  }

  public override string ClassDesc()
  {
    return "+1 hp\nFireDef";
  }

  public override string ClassName()
  {
      return "Whisperer";
  }

  public override ClassNode GetParent(){
      return new EgyptManglerClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }
 
  public override Unit UpgradeCharacter(Unit unit)
  {
      int hp = unit.GetMaxHP();
      unit.SetMaxHP(hp + 1);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("FireDef");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}
