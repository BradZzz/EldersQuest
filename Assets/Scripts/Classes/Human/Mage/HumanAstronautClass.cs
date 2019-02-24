using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HumanAstronautClass : ClassNode
{
  public HumanAstronautClass(){
    whenToUpgrade = StaticClassRef.LEVEL4;
  }

  public override string ClassDesc()
  {
    return "+1 atk\nAegisAtk";
  }

  public override string ClassName()
  {
      return "Astronaut";
  }

  public override ClassNode GetParent(){
      return new HumanGeniusClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetAttack(unit.GetAttack() + 1);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("AegisAtk");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}
