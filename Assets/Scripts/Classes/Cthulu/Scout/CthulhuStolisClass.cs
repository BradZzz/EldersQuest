using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CthulhuStolisClass : ClassNode
{
  public CthulhuStolisClass(){
    whenToUpgrade = 6;
  }

  public override string ClassDesc()
  {
    return "+2 hp\nBideKill";
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
      int maxHp = unit.GetMaxHP();
      unit.SetMaxHP(maxHp + 2);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("BideKill");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}
