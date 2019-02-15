using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EgyptBaseScout : ClassNode
{
  public EgyptBaseScout(){
    whenToUpgrade = 1;
  }

  public override string ClassDesc()
  {
    return "+1 mv\nFireMove";
  }

  public override string ClassName()
  {
      return "Scout";
  }

  public override ClassNode GetParent(){
      return null;
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ new EgyptManglerClass(), new EgyptWhispererClass() };
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      int spd = unit.GetMoveSpeed();
      unit.SetMoveSpeed(spd + 1);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("FireMove");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}
