using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FinalBaseDarkness : ClassNode
{
  public FinalBaseDarkness(){
    whenToUpgrade = StaticClassRef.LEVEL3;
  }

  public override string ClassDesc()
  {
    return "+1 atk\nAegisTurn";
  }

  public override string ClassName()
  {
      return "Darkness";
  }

  public override ClassNode GetParent(){
      return new FinalBaseEvening();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ new FinalBaseEternalDarkness() };
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetAttack(unit.GetAttack() + 1);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("AegisTurn");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}
