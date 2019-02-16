using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HumanCaptainClass : ClassNode
{
  public HumanCaptainClass(){
    whenToUpgrade = 12;
  }

  public override string ClassDesc()
  {
      return "RageMove";
  }

  public override string ClassName()
  {
      return "Captain";
  }

  public override ClassNode GetParent(){
      return new HumanCorporalClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("RageMove");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}
