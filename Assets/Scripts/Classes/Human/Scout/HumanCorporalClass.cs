using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HumanCorporalClass : ClassNode
{
  public HumanCorporalClass(){
    whenToUpgrade = 6;
  }

  public override string ClassDesc()
  {
    return "+1 mv\n+1 atk";
  }

  public override string ClassName()
  {
      return "Corporal";
  }

  public override ClassNode GetParent(){
      return new HumanBaseScout();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ new HumanCaptainClass(), new HumanLieutenantClass() };
  }
 
  public override Unit UpgradeCharacter(Unit unit)
  {
      int spd = unit.GetMoveSpeed();
      unit.SetMoveSpeed(spd + 1);
      int atk = unit.GetAttack();
      unit.SetAttack(atk + 1);
      return unit;
  }
}
