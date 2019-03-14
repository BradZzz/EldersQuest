using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FinalBaseChaos : ClassNode
{
  public FinalBaseChaos(){
    whenToUpgrade = StaticClassRef.LEVEL2;
  }

  public override string ClassDesc()
  {
    return "SnowMove\nSnowMove\n+2 hp";
  }

  public override string ClassName()
  {
      return "Chaos";
  }

  public override ClassNode GetParent(){
      return new FinalBaseDisorder();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ new FinalBaseCollapse() };
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetMaxHP(unit.GetMaxHP() + 2);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("SnowMove");
      skills.Add("SnowMove");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}
