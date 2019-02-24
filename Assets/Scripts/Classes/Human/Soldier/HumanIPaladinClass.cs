using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HumanIPaladinClass : ClassNode
{
  public HumanIPaladinClass(){
    whenToUpgrade = StaticClassRef.LEVEL3;
  }

  public override string ClassDesc()
  {
      return "+2 hp\nVoidAtk";
  }

  public override string ClassName()
  {
      return "Iron Paladin";
  }

  public override ClassNode GetParent(){
      return new HumanPaladinClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ new HumanInquisitorClass(), new HumanTormentorClass() };
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetMaxHP(unit.GetMaxHP() + 2);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("VoidAtk");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}
