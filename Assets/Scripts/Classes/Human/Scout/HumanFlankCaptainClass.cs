using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HumanFlankCaptainClass : ClassNode
{
  public HumanFlankCaptainClass(){
    whenToUpgrade = StaticClassRef.LEVEL4;
  }

  public override string ClassDesc()
  {
      return "AegisTurn";
  }

  public override string ClassName()
  {
      return "Flank Captain";
  }

  public override ClassNode GetParent(){
      return new HumanCaptainClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("AegisTurn");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}
