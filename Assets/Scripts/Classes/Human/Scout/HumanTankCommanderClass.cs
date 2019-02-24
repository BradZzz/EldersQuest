using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HumanTankCommanderClass : ClassNode
{
  public HumanTankCommanderClass(){
    whenToUpgrade = StaticClassRef.LEVEL4;
  }

  public override string ClassDesc()
  {
    return "+2 hp\nAegisWait";
  }

  public override string ClassName()
  {
      return "Tank Commander";
  }

  public override ClassNode GetParent(){
      return new HumanLieutenantClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetMaxHP(unit.GetMaxHP() + 2);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("AegisWait");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}
