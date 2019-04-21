using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CthulhuPBeastClass : ClassNode
{
  public CthulhuPBeastClass(){
    whenToUpgrade = StaticClassRef.LEVEL3;
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
      return new ClassNode[]{ new CthulhuFRiderClass(), new CthulhuYigClass() };
  }
 
  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetMaxHP(unit.GetMaxHP() + 1);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("RageAlliesWait");
      unit.SetSkills(skills.ToArray());
      return unit;
  }

  public override string ClassInactiveDesc(){
      return "+2 hp battle";
  }

  public override Unit InactiveUpgradeCharacter(Unit unit)
  {
      unit.SetHpBuffInactive(unit.GetHpBuff() + 2);
      return unit;
  }
}
