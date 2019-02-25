using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EgyptSSenseiClass : ClassNode
{
  public EgyptSSenseiClass(){
    whenToUpgrade = StaticClassRef.LEVEL3;
  }

  public override string ClassDesc()
  {
    return "+1 mv\nSnowMove";
  }

  public override string ClassName()
  {
      return "Ice Sensei";
  }

  public override ClassNode GetParent(){
      return new EgyptManglerClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ new EgyptCWalkerClass(), new EgyptDCarrierClass() };
  }
 
  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetMoveSpeed(unit.GetMoveSpeed() + 1);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("SnowMove");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}
