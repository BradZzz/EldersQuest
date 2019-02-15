using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CthulhuBaseScout : ClassNode
{
  public CthulhuBaseScout(){
    whenToUpgrade = 1;
  }

  public override string ClassDesc()
  {
    return "+2 mv\nHealWait";
  }

  public override string ClassName()
  {
      return "Scout";
  }

  public override ClassNode GetParent(){
      return null;
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ new CthulhuStolisClass(), new CthulhuTrollClass() };
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetMoveSpeed(unit.GetMoveSpeed() + 2);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("HealWait");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}
