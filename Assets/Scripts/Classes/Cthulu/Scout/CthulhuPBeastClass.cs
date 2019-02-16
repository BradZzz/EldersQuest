using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CthulhuPBeastClass : ClassNode
{
  public CthulhuPBeastClass(){
    whenToUpgrade = 6;
  }

  public override string ClassDesc()
  {
    return "+1 hp\nRageAlliesWait";
  }

  public override string ClassName()
  {
      return "Pit Beast";
  }

  public override ClassNode GetParent(){
      return new CthulhuTrollClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }
 
  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetMaxHP(unit.GetMaxHP() + 1);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("RageAlliesWait");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}
