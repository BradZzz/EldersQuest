using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EgyptSSenseiClass : ClassNode
{
  public EgyptSSenseiClass(){
    whenToUpgrade = 6;
  }

  public override string ClassDesc()
  {
    return "+1 mv\nFireMove";
  }

  public override string ClassName()
  {
      return "Whisperer";
  }

  public override ClassNode GetParent(){
      return new EgyptManglerClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }
 
  public override Unit UpgradeCharacter(Unit unit)
  {
      int hp = unit.GetMaxHP();
      unit.SetMaxHP(hp + 1);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("FireMove");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}
