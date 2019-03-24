using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EgyptManglerClass : ClassNode
{
  public EgyptManglerClass(){
    whenToUpgrade = StaticClassRef.LEVEL2;
  }

  public override string ClassDesc()
  {
    return "+1 hp\nSnowMove";
  }

  public override string ClassName()
  {
      return "Mangler";
  }

  public override ClassNode GetParent(){
      return new EgyptBaseScout();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ new EgyptRChosenClass(), new EgyptSSenseiClass() };
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetMaxHP(unit.GetMaxHP() + 1);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("SnowMove");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}
