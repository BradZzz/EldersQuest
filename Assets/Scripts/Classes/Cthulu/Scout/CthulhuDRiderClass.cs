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
    return "QuickenAlliesWait\nQuickenAlliesWait";
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
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("QuickenAlliesWait");
      skills.Add("QuickenAlliesWait");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}
