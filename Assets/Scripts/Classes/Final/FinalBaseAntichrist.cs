using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FinalBaseAntichrist : ClassNode
{
  public FinalBaseAntichrist(){
    whenToUpgrade = StaticClassRef.LEVEL4;
  }

  public override string ClassDesc()
  {
    return "+1 hp\nHealTurn\nAegisTurn";
  }

  public override string ClassName()
  {
      return "Antichrist";
  }

  public override ClassNode GetParent(){
      return new FinalBaseDireOmen();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetMaxHP(unit.GetMaxHP() + 1);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("HealTurn");
      skills.Add("AegisTurn");
      unit.SetSkills(skills.ToArray());
      return unit;
  }

  public override string ClassInactiveDesc(){
      return "";
  }

  public override Unit InactiveUpgradeCharacter(Unit unit)
  {
      return unit;
  }
}
