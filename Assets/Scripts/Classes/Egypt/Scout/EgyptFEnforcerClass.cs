using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EgyptFEnforcerClass : ClassNode
{
  public EgyptFEnforcerClass(){
    whenToUpgrade = StaticClassRef.LEVEL4;
  }

  public override string ClassDesc()
  {
    return "FireAtk\nFireAtk\n+1 atk trn";
  }

  public override string ClassName()
  {
      return "Fire Enforcer";
  }

  public override ClassNode GetParent(){
      return new EgyptFBenderClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }
 
  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetTurnAttacks(unit.GetTurnAttacks() + 1);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("FireAtk");
      skills.Add("FireAtk");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}
