using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CthulhuFRiderClass : ClassNode
{
  public CthulhuFRiderClass(){
    whenToUpgrade = StaticClassRef.LEVEL4;
  }

  public override string ClassDesc()
  {
    return "+1 hp\nBideWait";
  }

  public override string ClassName()
  {
      return "Famine Rider";
  }

  public override ClassNode GetParent(){
      return new CthulhuPBeastClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }
 
  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetMaxHP(unit.GetMaxHP() + 1);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("BideWait");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}
