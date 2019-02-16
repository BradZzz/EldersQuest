using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CthulhuHTotemClass : ClassNode
{
  public CthulhuHTotemClass(){
    whenToUpgrade = 6;
  }

  public override string ClassDesc()
  {
    return "-1 mv\nFireMove\nThornDef";
  }

  public override string ClassName()
  {
      return "Hell Totemic";
  }

  public override ClassNode GetParent(){
      return new CthulhuStolisClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }
 
  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetMoveSpeed(unit.GetMoveSpeed() - 1);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("FireMove");
      skills.Add("ThornDef");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}
