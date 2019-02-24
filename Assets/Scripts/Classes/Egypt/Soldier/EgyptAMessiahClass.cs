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
      return "AegisAtk\n+1 atk\n+1 mv";
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
      unit.SetAttack(unit.GetAttack() + 1);
      unit.SetMoveSpeed(unit.GetMoveSpeed() + 1);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("AegisAtk");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}
