using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HumanTormentorClass : ClassNode
{
  public HumanTormentorClass(){
    whenToUpgrade = StaticClassRef.LEVEL4;
  }

  public override string ClassDesc()
  {
      return "+1 atk\nSicklyAtk";
  }

  public override string ClassName()
  {
      return "Tormentor";
  }

  public override ClassNode GetParent(){
      return new HumanIPaladinClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetAttack(unit.GetAttack() + 1);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("SicklyAtk");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}
