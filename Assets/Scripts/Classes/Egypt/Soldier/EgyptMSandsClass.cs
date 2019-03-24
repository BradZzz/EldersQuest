using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EgyptMSandsClass : ClassNode
{
  public EgyptMSandsClass(){
    whenToUpgrade = StaticClassRef.LEVEL4;
  }

  public override string ClassDesc()
  {
      return "BideAlliesAtk\nBideAlliesAtk";
  }

  public override string ClassName()
  {
      return "Master of Sands";
  }

  public override ClassNode GetParent(){
      return new EgyptAnukeClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("BideAlliesAtk");
      skills.Add("BideAlliesAtk");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}
