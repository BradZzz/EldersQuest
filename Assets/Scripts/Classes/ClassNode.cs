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

    public static string GetClassHeirarchyString(ClassNode nde){
        string clssHier = "";
        List<string> parents = new List<string>();

        ClassNode thsNde = nde;
        parents.Add(thsNde.ClassName());
        while(thsNde.GetParent() != null){
            thsNde = thsNde.GetParent();
            parents.Add(thsNde.ClassName());
        }
        parents.Reverse();
        for(int i = 0; i<parents.Count; i++){
          if (i == parents.Count - 1) {
            clssHier+= "<color=#ff0000ff>" + i.ToString() + ") " + parents[i] + " </color>\n";
          } else {
            clssHier+= i.ToString() + ") " + parents[i] + "\n";
          }
        }

        return clssHier;
    }

    public static UnitProxy ComputeClassBaseUnit(ClassNode nde, Glossary glossy){
        ClassNode thsNde = nde;
        while(thsNde.GetParent() != null){
            thsNde = thsNde.GetParent();
        }
        Unit.UnitType typ = Unit.UnitType.Mage;
        if (thsNde.GetType().ToString().Contains("Scout")) {
            typ = Unit.UnitType.Scout;
        } else if (thsNde.GetType().ToString().Contains("Soldier")) {
            typ = Unit.UnitType.Soldier;
        }
        Unit.FactionType fac = Unit.FactionType.Cthulhu;
        if (thsNde.GetType().ToString().Contains("Human")) {
            fac = Unit.FactionType.Human;
        } else if (thsNde.GetType().ToString().Contains("Egypt")) {
            fac = Unit.FactionType.Egypt;
        }
        

        return ComputeClassBaseUnit(fac, typ, glossy);
    }

    public static string GetFactionFromClass(string clss){
        if (clss.Contains("Human")) {
            return "Human";
        }
        if (clss.Contains("Egypt")) {
            return "Egypt";
        }
        return "Cthulhu";
    }

    //Active units who get bonuses before battle
    public static Unit ApplyClassBonusesBattle(Unit battleUnit, Unit[] inactiveUnits){
        foreach(Unit aUnit in inactiveUnits){
            switch (aUnit.GetUnitType()) {
                case Unit.UnitType.Mage:break;
                case Unit.UnitType.Scout:battleUnit.SetMoveBuff(battleUnit.GetMoveBuff() + 1);break;
                case Unit.UnitType.Soldier:battleUnit.SetHpBuff(battleUnit.GetHpBuff() + 1);battleUnit.SetCurrHealth(battleUnit.GetMaxHP());break;
            }
        }
        return battleUnit;
    }

    //Inactive units who get bonuses after battle
    public static Unit ApplyClassBonusesInactive(Unit inactiveUnit, Unit[] inactiveUnits){
        foreach(Unit aUnit in inactiveUnits){
            switch (aUnit.GetUnitType()) {
                case Unit.UnitType.Mage:inactiveUnit.SetLvl(inactiveUnit.GetLvl() + 1);break;
                case Unit.UnitType.Scout:break;
                case Unit.UnitType.Soldier:break;
            }
        }
        return inactiveUnit;
    }

    public static string GetClassBonusString(Unit[] inactiveUnits){
        string unitStr = "";
        foreach(Unit aUnit in inactiveUnits){
            switch(aUnit.GetUnitType()){
                case Unit.UnitType.Mage:unitStr += "+1 EXP INACTIVE\n";break;
                case Unit.UnitType.Scout:unitStr += "+1 MV BATTLE\n";break;
                case Unit.UnitType.Soldier:unitStr += "+1 HP BATTLE\n";break;
            }
        }
        return unitStr;
    }

    public static string FormatClass(string clss){
        return clss.Replace("Base","").Replace("Class","").Replace("Egypt","").Replace("Cthulhu","").Replace("Human","");
    }

    //What to do with the unit object when the unit is upgraded
    public abstract Unit UpgradeCharacter(Unit unit);
    //What to do with the unit object when the unit is inactive
    public abstract Unit InactiveUpgradeCharacter(Unit unit);

    public string ClassDescFull(){
      return ClassName() + ": " + ClassDesc();
    }
    public abstract string ClassInactiveDesc();
    public abstract string ClassDesc();
    public abstract string ClassName();
    public int GetWhenToUpgrade(){
        return whenToUpgrade;
    }
}
