using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EgyptPChannelerClass : ClassNode
{
  public EgyptPChannelerClass(){
    whenToUpgrade = StaticClassRef.LEVEL4;
  }

  public override string ClassDesc()
  {
    return "+1 mv\nRageWait";
  }

  public override string ClassName()
  {
      return "Power Channeler";
  }

  public override ClassNode GetParent(){
      return new EgyptRChosenClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }
 
  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetMoveSpeed(unit.GetMoveSpeed() + 1);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("RageWait");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}
