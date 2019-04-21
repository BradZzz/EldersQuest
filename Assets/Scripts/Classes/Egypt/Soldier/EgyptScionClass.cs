using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EgyptScionClass : ClassNode
{
  public EgyptScionClass(){
    whenToUpgrade = StaticClassRef.LEVEL2;
  }

  public override string ClassDesc()
  {
      return "+1 atk trn\nAegisAtk";
  }

  public override string ClassName()
  {
      return "Scion";
  }

  public override ClassNode GetParent(){
      return new EgyptBaseSoldier();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ new EgyptBesClass(), new EgyptHapyClass() };
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetTurnAttacks(unit.GetTurnAttacks() + 1);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("AegisAtk");
      unit.SetSkills(skills.ToArray());
      return unit;
  }

  public override string ClassInactiveDesc(){
      return "+1 hp battle";
  }

  public override Unit InactiveUpgradeCharacter(Unit unit)
  {
      unit.SetHpBuffInactive(unit.GetHpBuff() + 1);
      return unit;
  }
}
