using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EgyptHapyClass : ClassNode
{
  public EgyptHapyClass(){
    whenToUpgrade = StaticClassRef.LEVEL3;
  }

  public override string ClassDesc()
  {
      return "+1 hp\nThornDef";
  }

  public override string ClassName()
  {
      return "Hapy";
  }

  public override ClassNode GetParent(){
      return new EgyptScionClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ new EgyptHMessiahClass(), new EgyptAConduitClass() };
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetMaxHP(unit.GetMaxHP() + 1);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("ThornDef");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}
