using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EgyptSActorClass : ClassNode
{
  public EgyptSActorClass(){
    whenToUpgrade = StaticClassRef.LEVEL4;
  }

  public override string ClassDesc()
  {
      return "BideKill\nRageKill";
  }

  public override string ClassName()
  {
      return "Slash Actor";
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
      skills.Add("BideKill");
      skills.Add("RageKill");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}
