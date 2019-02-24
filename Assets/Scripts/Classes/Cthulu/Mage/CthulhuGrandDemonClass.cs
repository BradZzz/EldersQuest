using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CthulhuGrandDemonClass : ClassNode
{
  public CthulhuGrandDemonClass(){
    whenToUpgrade = StaticClassRef.LEVEL3;
  }

  public override string ClassDesc()
  {
    return "+2 hp\nForceAtk";
  }

  public override string ClassName()
  {
      return "Grand Demon";
  }

  public override ClassNode GetParent(){
      return new CthulhuLesserDemonClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ new CthulhuDevilClass(), new CthulhuFaustusClass() };
  }
 
  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetMaxHP(unit.GetMaxHP() + 2);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("ForceAtk");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}
