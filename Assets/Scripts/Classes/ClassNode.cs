using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class ClassNode
{
    [SerializeField]
    protected static int whenToUpgrade;

    public abstract ClassNode GetParent();
    
    public abstract ClassNode[] GetChildren();

    public static ClassNode ComputeClassObject(Unit.FactionType fType, Unit.UnitType uType){
        ClassNode rNode = new HumanBaseSoldier();
        switch(fType) {
          case Unit.FactionType.Human: 
            switch(uType){
              case Unit.UnitType.Mage: return new HumanBaseMage();
              case Unit.UnitType.Scout: return new HumanBaseScout();
              case Unit.UnitType.Soldier: return new HumanBaseSoldier();
            }
            break;
          case Unit.FactionType.Egypt: 
            switch(uType){
              case Unit.UnitType.Mage: return new EgyptBaseMage();
              case Unit.UnitType.Scout: return new EgyptBaseScout();
              case Unit.UnitType.Soldier: return new EgyptBaseSoldier();
            }
            break;
          case Unit.FactionType.Cthulhu: 
            switch(uType){
              case Unit.UnitType.Mage: return new CthulhuBaseMage();
              case Unit.UnitType.Scout: return new CthulhuBaseScout();
              case Unit.UnitType.Soldier: return new CthulhuBaseSoldier();
            }
            break;
        }
        return new HumanBaseSoldier();
    }

    public abstract Unit UpgradeCharacter(Unit unit);
    public string ClassDescFull(){
      return ClassName() + ": " + ClassDesc();
    }
    public abstract string ClassDesc();
    public abstract string ClassName();
    public int GetWhenToUpgrade(){
        return whenToUpgrade;
    }
}
