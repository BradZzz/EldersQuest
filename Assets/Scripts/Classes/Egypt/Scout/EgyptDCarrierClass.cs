using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EgyptDCarrierClass : ClassNode
{
  public EgyptDCarrierClass(){
    whenToUpgrade = StaticClassRef.LEVEL4;
  }

  public override string ClassDesc()
  {
    return "+1 atk trn\nEnfeebleAtk";
  }

  public override string ClassName()
  {
      return "Disease Carrier";
  }

  public override ClassNode GetParent(){
      return new EgyptSSenseiClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }
 
  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetTurnAttacks(unit.GetTurnAttacks() + 1);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("EnfeebleAtk");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}
