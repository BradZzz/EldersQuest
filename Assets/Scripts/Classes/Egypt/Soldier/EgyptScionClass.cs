using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EgyptScionClass : ClassNode
{
  public EgyptScionClass(){
    whenToUpgrade = StaticClassRef.LEVEL2;
  }

  public override string ClassDesc()
  {
      return "Aegis Attack";
  }

  public override string ClassName()
  {
      return "Scion";
  }

  public override ClassNode GetParent(){
      return new EgyptBaseSoldier();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ new EgyptBesClass(), new EgyptHapyClass() };
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("AegisAtk");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}
