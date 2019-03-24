using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CthulhuStolisClass : ClassNode
{
  public CthulhuStolisClass(){
    whenToUpgrade = StaticClassRef.LEVEL2;
  }

  public override string ClassDesc()
  {
    return "+1 hp\nBideWait";
  }

  public override string ClassName()
  {
      return "Stolis";
  }

  public override ClassNode GetParent(){
      return new CthulhuBaseScout();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ new CthulhuApparitionClass(), new CthulhuHTotemClass() };
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetMaxHP(unit.GetMaxHP() + 1);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("BideWait");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}
