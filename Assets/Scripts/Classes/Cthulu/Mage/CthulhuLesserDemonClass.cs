using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CthulhuLesserDemonClass : ClassNode
{
  public CthulhuLesserDemonClass(){
    whenToUpgrade = StaticClassRef.LEVEL2;
  }

  public override string ClassDesc()
  {
    return "FireKill\n+1 atk rng";
  }

  public override string ClassName()
  {
      return "Lesser Demon";
  }

  public override ClassNode GetParent(){
      return new CthulhuBaseMage();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ new CthulhuGrandDemonClass(), new CthulhuPitLordClass() };
  }
 
  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetAtkRange(unit.GetAtkRange() + 1);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("FireKill");
      unit.SetSkills(skills.ToArray());
      return unit;
  }

  public override string ClassInactiveDesc(){
      return "+1 exp inactive";
  }

  public override Unit InactiveUpgradeCharacter(Unit unit)
  {
      unit.SetInactiveExpBuff(unit.GetInactiveExpBuff() + 1);
      return unit;
  }
}
