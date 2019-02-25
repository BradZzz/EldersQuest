using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EgyptWMasterClass : ClassNode
{
  public EgyptWMasterClass(){
    whenToUpgrade = StaticClassRef.LEVEL4;
  }

  public override string ClassDesc()
  {
    return "AegisAlliesAtk\nWallMove";
  }

  public override string ClassName()
  {
      return "Wish Master";
  }

  public override ClassNode GetParent(){
      return new EgyptGenieClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }
 
  public override Unit UpgradeCharacter(Unit unit)
  {
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("AegisAlliesAtk");
      skills.Add("WallMove");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}
