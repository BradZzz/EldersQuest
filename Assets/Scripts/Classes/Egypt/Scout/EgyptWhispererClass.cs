using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EgyptWhispererClass : ClassNode
{
  public EgyptWhispererClass(){
    whenToUpgrade = StaticClassRef.LEVEL2;
  }

  public override string ClassDesc()
  {
    return "+1 hp\nFireMove";
  }

  public override string ClassName()
  {
      return "Whisperer";
  }

  public override ClassNode GetParent(){
      return new EgyptBaseScout();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ new EgyptRChosenClass(), new EgyptArsonistClass()};
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
