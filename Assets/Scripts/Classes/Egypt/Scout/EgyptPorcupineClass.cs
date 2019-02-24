using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EgyptPorcupineClass : ClassNode
{
  public EgyptPorcupineClass(){
    whenToUpgrade = StaticClassRef.LEVEL4;
  }

  public override string ClassDesc()
  {
    return "+2 hp\nFireDef";
  }

  public override string ClassName()
  {
      return "Porcupine";
  }

  public override ClassNode GetParent(){
      return new EgyptFBenderClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }
 
  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetMaxHP(unit.GetMaxHP() + 2);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("FireDef");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}
