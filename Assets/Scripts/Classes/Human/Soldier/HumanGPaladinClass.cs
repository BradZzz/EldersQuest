using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HumanGPaladinClass : ClassNode
{
  public HumanGPaladinClass(){
    whenToUpgrade = StaticClassRef.LEVEL3;
  }

  public override string ClassDesc()
  {
      return "+1 mv\n+1 mv trn\nBideWait";
  }

  public override string ClassName()
  {
      return "Glass Paladin";
  }

  public override ClassNode GetParent(){
      return new HumanPaladinClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ new HumanCCrusaderClass(), new HumanDCrusaderClass()  };
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetMoveSpeed(unit.GetMoveSpeed() + 1);
      unit.SetTurnMoves(unit.GetTurnMoves() + 1);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("BideWait");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}
