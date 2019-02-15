using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EgyptConjurerClass : ClassNode
{
  public EgyptConjurerClass(){
    whenToUpgrade = 7;
  }

  public override string ClassDesc()
  {
    return "+1 mv\nWarpAtk";
  }

  public override string ClassName()
  {
      return "Conjurer";
  }

  public override ClassNode GetParent(){
      return new EgyptBaseMage();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      int hp = unit.GetMaxHP();
      unit.SetMoveSpeed(unit.GetMoveSpeed() + 1);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("WarpAtk");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}
