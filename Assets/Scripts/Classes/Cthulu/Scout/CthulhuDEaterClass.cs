using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CthulhuDEaterClass : ClassNode
{
  public CthulhuDEaterClass(){
    whenToUpgrade = StaticClassRef.LEVEL3;
  }

  public override string ClassDesc()
  {
    return "+1 mv\nSicklyAtk";
  }

  public override string ClassName()
  {
      return "Dream Eater";
  }

  public override ClassNode GetParent(){
      return new CthulhuTrollClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ new CthulhuAzathothClass(), new CthulhuYogsothothClass() };
  }
 
  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetMoveSpeed(unit.GetMoveSpeed() + 1);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("SicklyAtk");
      unit.SetSkills(skills.ToArray());
      return unit;
  }

  public override string ClassInactiveDesc(){
      return "+2 mv battle";
  }

  public override Unit InactiveUpgradeCharacter(Unit unit)
  {
      unit.SetMoveBuff(unit.GetMoveBuff() + 2);
      return unit;
  }
}
