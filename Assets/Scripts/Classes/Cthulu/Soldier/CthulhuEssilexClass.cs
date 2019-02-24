using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CthulhuEssilexClass : ClassNode
{
  public CthulhuEssilexClass(){
    whenToUpgrade = StaticClassRef.LEVEL4;
  }

  public override string ClassDesc()
  {
    return "+1 mv\nBideKill";
  }

  public override string ClassName()
  {
      return "Essilex";
  }

  public override ClassNode GetParent(){
      return new CthulhuAVamprossClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }

 
  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetMoveSpeed(unit.GetMoveSpeed() + 1);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("BideKill");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}
