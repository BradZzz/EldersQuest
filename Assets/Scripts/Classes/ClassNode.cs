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
          case Unit.FactionType.None: 
            switch(uType){
              case Unit.UnitType.Mage: return new FinalBaseOmen();
              case Unit.UnitType.Scout: return new FinalBaseDisorder();
              case Unit.UnitType.Soldier: return new FinalBaseTwilight();
            }
            break;
        }
        return new FinalBaseOmen();
    }

    public static UnitProxy ComputeClassBaseUnit(Unit.FactionType fType, Unit.UnitType uType, Glossary glossy){
        ClassNode rNode = new HumanBaseSoldier();
        switch(fType) {
          case Unit.FactionType.Human: 
            switch(uType){
              case Unit.UnitType.Mage: return glossy.humanMage;
              case Unit.UnitType.Scout: return glossy.humanScout;
              case Unit.UnitType.Soldier: return glossy.humanSoldier;
            }
            break;
          case Unit.FactionType.Egypt: 
            switch(uType){
              case Unit.UnitType.Mage: return glossy.egyptMage;
              case Unit.UnitType.Scout: return glossy.egyptScout;
              case Unit.UnitType.Soldier: return glossy.egyptSoldier;
            }
            break;
          case Unit.FactionType.Cthulhu: 
            switch(uType){
              case Unit.UnitType.Mage: return glossy.cthulhuMage;
              case Unit.UnitType.Scout: return glossy.cthulhuScout;
              case Unit.UnitType.Soldier: return glossy.cthulhuSoldier;
            }
            break;
          case Unit.FactionType.None: 
            switch(uType){
              case Unit.UnitType.Mage: return glossy.finalRed;
              case Unit.UnitType.Scout: return glossy.finalBlue;
              case Unit.UnitType.Soldier: return glossy.finalBlack;
            }
            break;
        }
        return glossy.finalBlack;
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
