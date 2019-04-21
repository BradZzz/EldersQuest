using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HumanRSamuraiClass : ClassNode
{
  public HumanRSamuraiClass(){
    whenToUpgrade = StaticClassRef.LEVEL4;
  }

  public override string ClassDesc()
  {
      return "+1 hp\n +1 atk rng";
  }

  public override string ClassName()
  {
      return "Rifle Samurai";
  }

  public override ClassNode GetParent(){
      return new HumanGDancerClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetAtkRange(unit.GetAtkRange() + 1);
      unit.SetMaxHP(unit.GetMaxHP() + 1);
      return unit;
  }

  public override string ClassInactiveDesc(){
      return "+1 atk battle";
  }

  public override Unit InactiveUpgradeCharacter(Unit unit)
  {
      unit.SetAttackBuff(unit.GetAttackBuff() + 1);
      return unit;
  }
}
