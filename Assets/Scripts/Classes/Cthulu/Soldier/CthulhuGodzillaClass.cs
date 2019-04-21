using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CthulhuGodzillaClass : ClassNode
{
  public CthulhuGodzillaClass(){
    whenToUpgrade = StaticClassRef.LEVEL4;
  }

  public override string ClassDesc()
  {
    return "+2 mv trn\nRageMove";
  }

  public override string ClassName()
  {
      return "Godzilla";
  }

  public override ClassNode GetParent(){
      return new CthulhuSBehemothClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetTurnMoves(unit.GetTurnMoves() + 2);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("RageMove");
      unit.SetSkills(skills.ToArray());
      return unit;
  }

  public override string ClassInactiveDesc(){
      return "EnfeebleAtk";
  }

  public override Unit InactiveUpgradeCharacter(Unit unit)
  {
      unit.SetSkillsBuffs(new string[]{ "EnfeebleAtk" });
      return unit;
  }
}
