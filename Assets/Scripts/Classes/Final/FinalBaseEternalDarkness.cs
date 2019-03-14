using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FinalBaseEternalDarkness : ClassNode
{
  public FinalBaseEternalDarkness(){
    whenToUpgrade = StaticClassRef.LEVEL4;
  }

  public override string ClassDesc()
  {
    return "+2 hp\nHealTurn\nHealTurn";
  }

  public override string ClassName()
  {
      return "Eternal Darkness";
  }

  public override ClassNode GetParent(){
      return new FinalBaseDarkness();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{  };
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetMaxHP(unit.GetMaxHP() + 2);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("HealTurn");
      skills.Add("HealTurn");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}
