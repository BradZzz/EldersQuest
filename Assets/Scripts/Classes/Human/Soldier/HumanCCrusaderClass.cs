using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HumanCCrusaderClass : ClassNode
{
  public HumanCCrusaderClass(){
    whenToUpgrade = StaticClassRef.LEVEL4;
  }

  public override string ClassDesc()
  {
      return "HealAlliesAtk/nBideKill";
  }

  public override string ClassName()
  {
      return "Crystal Crusader";
  }

  public override ClassNode GetParent(){
      return new HumanGPaladinClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("HealAlliesAtk");
      skills.Add("BideKill");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}
