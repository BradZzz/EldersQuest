using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FinalBaseIMessiah : ClassNode
{
  public FinalBaseIMessiah(){
    whenToUpgrade = StaticClassRef.LEVEL3;
  }

  public override string ClassDesc()
  {
    return "+1 hp\nFireAtk\nFireAtk";
  }

  public override string ClassName()
  {
      return "Incipient Messiah";
  }

  public override ClassNode GetParent(){
      return new FinalBaseDireOmen();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ new FinalBaseAntichrist() };
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetMaxHP(unit.GetMaxHP() + 1);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("FireAtk");
      skills.Add("FireAtk");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}
