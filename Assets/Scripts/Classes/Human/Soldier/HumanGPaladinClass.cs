using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HumanGPaladinClass : ClassNode
{
  public HumanGPaladinClass(){
    whenToUpgrade = 12;
  }

  public override string ClassDesc()
  {
      return "+2 mv/BideWait";
  }

  public override string ClassName()
  {
      return "Glass Paladin";
  }

  public override ClassNode GetParent(){
      return new HumanPaladinClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetMoveSpeed(unit.GetMoveSpeed() + 2);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("BideWait");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}
