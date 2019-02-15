using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticClassRef
{
    public static ClassNode GetClass(AvailableClasses cType){
        switch(cType){
            case AvailableClasses.HumanBaseMage: return new HumanBaseMage();
            case AvailableClasses.HumanIWarlockClass: return new HumanIWarlockClass();
            case AvailableClasses.HumanTechnomancerClass: return new HumanTechnomancerClass();

            case AvailableClasses.HumanBaseScout: return new HumanBaseScout();
            case AvailableClasses.HumanCorporalClass: return new HumanCorporalClass();
            case AvailableClasses.HumanFSergeantClass: return new HumanFSergeantClass();

            case AvailableClasses.HumanBaseSoldier: return new HumanBaseSoldier();
            case AvailableClasses.HumanBerserkerClass: return new HumanBerserkerClass();
            case AvailableClasses.HumanPaladinClass: return new HumanPaladinClass();
            default: return new HumanBaseSoldier();
        }
    }

    public enum AvailableClasses{
        HumanBaseMage, HumanIWarlockClass, HumanTechnomancerClass,
        HumanBaseScout, HumanCorporalClass, HumanFSergeantClass,
        HumanBaseSoldier, HumanBerserkerClass, HumanPaladinClass, None
    }
}
