using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EgyptBaseSoldier : ClassNode
{
  public EgyptBaseSoldier(){
    whenToUpgrade = StaticClassRef.LEVEL1;
  }

  public override string ClassDesc()
  {
    return "+1 hp\nAegisBegin";
  }

  public override string ClassName()
  {
      return "Soldier";
  }

  public override ClassNode GetParent(){
      return null;
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ new EgyptNomadClass(), new EgyptScionClass() };
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetMaxHP(unit.GetMaxHP() + 1);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("AegisBegin");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}
