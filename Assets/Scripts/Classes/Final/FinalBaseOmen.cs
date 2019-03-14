using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FinalBaseOmen : ClassNode
{
  public FinalBaseOmen(){
    whenToUpgrade = StaticClassRef.LEVEL1;
  }

  public override string ClassDesc()
  {
    return "+1 mv\n+1 atk rng\nAoeAtk";
  }

  public override string ClassName()
  {
      return "Omen";
  }

  public override ClassNode GetParent(){
      return null;
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ new FinalBaseDireOmen() };
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetMoveSpeed(unit.GetMoveSpeed() + 1);
      unit.SetAtkRange(unit.GetAtkRange() + 1);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("AoeAtk");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}
