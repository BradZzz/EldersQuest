using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CthulhuNyarlathotepClass : ClassNode
{
  public CthulhuNyarlathotepClass(){
    whenToUpgrade = StaticClassRef.LEVEL4;
  }

  public override string ClassDesc()
  {
    return "+1 mv trn\nNullifyAtk";
  }

  public override string ClassName()
  {
      return "Nyarlathotep";
  }

  public override ClassNode GetParent(){
      return new CthulhuApparitionClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }
 
  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetTurnMoves(unit.GetTurnMoves() + 1);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("NullifyAtk");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}
