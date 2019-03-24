using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EgyptBesClass : ClassNode
{
  public EgyptBesClass(){
    whenToUpgrade = StaticClassRef.LEVEL3;
  }

  public override string ClassDesc()
  {
      return "+1 mv\nAegisAlliesAtk";
  }

  public override string ClassName()
  {
      return "Bes";
  }

  public override ClassNode GetParent(){
      return new EgyptScionClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ new EgyptBMessiahClass(), new EgyptOracleClass() };
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetMoveSpeed(unit.GetMoveSpeed() + 1);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("AegisAlliesAtk");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}
