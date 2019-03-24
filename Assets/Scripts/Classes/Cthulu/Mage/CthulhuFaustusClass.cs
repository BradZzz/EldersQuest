using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CthulhuFaustusClass : ClassNode
{
  public CthulhuFaustusClass(){
    whenToUpgrade = StaticClassRef.LEVEL4;
  }

  public override string ClassDesc()
  {
    return "BideKill\nVoidAtk";
  }

  public override string ClassName()
  {
      return "Faustus";
  }

  public override ClassNode GetParent(){
      return new CthulhuGrandDemonClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }
 
  public override Unit UpgradeCharacter(Unit unit)
  {
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("VoidAtk");
      skills.Add("BideKill");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}
