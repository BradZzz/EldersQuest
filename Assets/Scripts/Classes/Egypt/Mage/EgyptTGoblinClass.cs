using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EgyptTGoblinClass : ClassNode
{
  public EgyptTGoblinClass(){
    whenToUpgrade = StaticClassRef.LEVEL4;
  }

  public override string ClassDesc()
  {
    return "HealAtk\nBideWait";
  }

  public override string ClassName()
  {
      return "Treasure Goblin";
  }

  public override ClassNode GetParent(){
      return new EgyptKoboldClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }
 
  public override Unit UpgradeCharacter(Unit unit)
  {
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("HealAtk");
      skills.Add("BideWait");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}
