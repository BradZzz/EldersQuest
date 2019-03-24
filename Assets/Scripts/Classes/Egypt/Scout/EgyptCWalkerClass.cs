using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EgyptCWalkerClass : ClassNode
{
  public EgyptCWalkerClass(){
    whenToUpgrade = StaticClassRef.LEVEL4;
  }

  public override string ClassDesc()
  {
    return "AegisTurn\nSnowMove";
  }

  public override string ClassName()
  {
      return "Frost Walker";
  }

  public override ClassNode GetParent(){
      return new EgyptSSenseiClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }
 
  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetMoveSpeed(unit.GetMoveSpeed() + 1);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("AegisTurn");
      skills.Add("SnowMove");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}
