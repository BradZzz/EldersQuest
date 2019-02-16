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
      return new ClassNode[]{ };
  }
 
  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetMoveSpeed(unit.GetMoveSpeed() + 1);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("SicklyAtk");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}
