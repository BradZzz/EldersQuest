using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EgyptAMessiahClass : ClassNode
{
  public EgyptAMessiahClass(){
    whenToUpgrade = StaticClassRef.LEVEL4;
  }

  public override string ClassDesc()
  {
      return "QuickenAlliesAtk\nQuickenAlliesAtk\nQuickenAlliesAtk";
  }

  public override string ClassName()
  {
      return "Ash Messiah";
  }

  public override ClassNode GetParent(){
      return new EgyptAshClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("QuickenAlliesAtk");
      skills.Add("QuickenAlliesAtk");
      skills.Add("QuickenAlliesAtk");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}
