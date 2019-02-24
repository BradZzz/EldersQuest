using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HumanGeneralClass : ClassNode
{
  public HumanGeneralClass(){
    whenToUpgrade = StaticClassRef.LEVEL4;
  }

  public override string ClassDesc()
  {
      return "+1 atk rng\nRageDef";
  }

  public override string ClassName()
  {
      return "Captain";
  }

  public override ClassNode GetParent(){
      return new HumanCaptainClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetAtkRange(unit.GetAtkRange() + 1);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("RageDef");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}
