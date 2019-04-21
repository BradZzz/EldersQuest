using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CthulhuDRiderClass : ClassNode
{
  public CthulhuDRiderClass(){
    whenToUpgrade = StaticClassRef.LEVEL4;
  }

  public override string ClassDesc()
  {
    return "EnfeebleEnemiesWait\nEnfeebleEnemiesWait\n+1 atk trn";
  }

  public override string ClassName()
  {
      return "Death Rider";
  }

  public override ClassNode GetParent(){
      return new CthulhuHTotemClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }
 
  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetTurnAttacks(unit.GetTurnAttacks() + 1);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("EnfeebleEnemiesWait");
      skills.Add("EnfeebleEnemiesWait");
      unit.SetSkills(skills.ToArray());
      return unit;
  }

  public override string ClassInactiveDesc(){
      return "ThornDef";
  }

  public override Unit InactiveUpgradeCharacter(Unit unit)
  {
      unit.SetSkillsBuffs(new string[]{ "ThornDef" });
      return unit;
  }
}
