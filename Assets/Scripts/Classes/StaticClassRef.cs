using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticClassRef
{
    public static ClassNode GetClass(AvailableClasses cType){
        switch(cType){
            /* Human */
            case AvailableClasses.HumanBaseMage: return new HumanBaseMage();
            case AvailableClasses.HumanIWarlockClass: return new HumanIWarlockClass();
            case AvailableClasses.HumanTechnomancerClass: return new HumanTechnomancerClass();

            case AvailableClasses.HumanBaseScout: return new HumanBaseScout();
            case AvailableClasses.HumanCorporalClass: return new HumanCorporalClass();
            case AvailableClasses.HumanFSergeantClass: return new HumanFSergeantClass();

            case AvailableClasses.HumanBaseSoldier: return new HumanBaseSoldier();
            case AvailableClasses.HumanBerserkerClass: return new HumanBerserkerClass();
            case AvailableClasses.HumanPaladinClass: return new HumanPaladinClass();

            /* Egypt */
            case AvailableClasses.EgyptBaseMage: return new EgyptBaseMage();
            case AvailableClasses.EgyptConjurerClass: return new EgyptConjurerClass();
            case AvailableClasses.EgyptDjinnClass: return new EgyptDjinnClass();

            case AvailableClasses.EgyptBaseScout: return new EgyptBaseScout();
            case AvailableClasses.EgyptManglerClass: return new EgyptManglerClass();
            case AvailableClasses.EgyptWhispererClass: return new EgyptWhispererClass();

            case AvailableClasses.EgyptBaseSoldier: return new EgyptBaseSoldier();
            case AvailableClasses.EgyptNomadClass: return new EgyptNomadClass();
            case AvailableClasses.EgyptScionClass: return new EgyptScionClass();

            /* Cthulhu */
            case AvailableClasses.CthulhuBaseMage: return new CthulhuBaseMage();
            case AvailableClasses.CthulhuLesserDemonClass: return new CthulhuLesserDemonClass();
            case AvailableClasses.CthulhuNecromancerClass: return new CthulhuNecromancerClass();

            case AvailableClasses.CthulhuBaseScout: return new CthulhuBaseScout();
            case AvailableClasses.CthulhuStolisClass: return new CthulhuStolisClass();
            case AvailableClasses.CthulhuTrollClass: return new CthulhuTrollClass();

            case AvailableClasses.CthulhuBaseSoldier: return new CthulhuBaseSoldier();
            case AvailableClasses.CthulhuVamprossClass: return new CthulhuVamprossClass();
            case AvailableClasses.CthulhuGoliathClass: return new CthulhuGoliathClass();
            default: return new HumanBaseSoldier();
        }
    }

    public enum AvailableClasses{
        HumanBaseMage, HumanIWarlockClass, HumanTechnomancerClass,
        HumanBaseScout, HumanCorporalClass, HumanFSergeantClass,
        HumanBaseSoldier, HumanBerserkerClass, HumanPaladinClass, 

        EgyptBaseMage, EgyptConjurerClass, EgyptDjinnClass,
        EgyptBaseScout, EgyptManglerClass, EgyptWhispererClass,
        EgyptBaseSoldier, EgyptNomadClass, EgyptScionClass, 

        CthulhuBaseMage, CthulhuLesserDemonClass, CthulhuNecromancerClass,
        CthulhuBaseScout, CthulhuStolisClass, CthulhuTrollClass,
        CthulhuBaseSoldier, CthulhuVamprossClass, CthulhuGoliathClass, 

        None
    }
}
