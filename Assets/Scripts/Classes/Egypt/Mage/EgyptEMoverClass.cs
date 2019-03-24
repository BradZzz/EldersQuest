using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EgyptEMoverClass : ClassNode
{
  public EgyptEMoverClass(){
    whenToUpgrade = StaticClassRef.LEVEL4;
  }

  public override string ClassDesc()
  {
    return "+3 hp";
  }

  public override string ClassName()
  {
      return "Earth Mover";
  }

  public override ClassNode GetParent(){
      return new EgyptGeomancerClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }
 
  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetMaxHP(unit.GetMaxHP() + 3);
      return unit;
  }
}
