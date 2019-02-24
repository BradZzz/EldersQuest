﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticClassRef
{
    public static int LEVEL1 = 2;
    public static int LEVEL2 = 5;
    public static int LEVEL3 = 9;
    public static int LEVEL4 = 16;

    public static ClassNode GetClass(AvailableClasses cType){
        switch(cType){
            /* Human */
            case AvailableClasses.HumanBaseMage: return new HumanBaseMage();
            /* lvl 2 */
            case AvailableClasses.HumanIWarlockClass: return new HumanIWarlockClass();
            case AvailableClasses.HumanTechnomancerClass: return new HumanTechnomancerClass();
            /* lvl 3 */
            case AvailableClasses.HumanWizardClass: return new HumanWizardClass();
            case AvailableClasses.HumanGeniusClass: return new HumanGeniusClass();
            case AvailableClasses.HumanTinkererClass: return new HumanTinkererClass();
            case AvailableClasses.HumanArchMageClass: return new HumanArchMageClass();
            /* lvl 4 */
            case AvailableClasses.HumanGigaWizardClass: return new HumanGigaWizardClass();
            case AvailableClasses.HumanIceWizardClass: return new HumanIceWizardClass();
            case AvailableClasses.HumanGrandMageClass: return new HumanGrandMageClass();
            case AvailableClasses.HumanMetalmancerClass: return new HumanMetalmancerClass();
            case AvailableClasses.HumanBramblelockClass: return new HumanBramblelockClass();
            case AvailableClasses.HumanKootClass: return new HumanKootClass();
            case AvailableClasses.HumanAstronautClass: return new HumanAstronautClass();
            case AvailableClasses.HumanDoctorClass: return new HumanDoctorClass();

            case AvailableClasses.HumanBaseScout: return new HumanBaseScout();
            /* lvl 2 */
            case AvailableClasses.HumanCorporalClass: return new HumanCorporalClass();
            case AvailableClasses.HumanFSergeantClass: return new HumanFSergeantClass();
            /* lvl 3 */
            case AvailableClasses.HumanMSergeantClass: return new HumanMSergeantClass();
            case AvailableClasses.HumanCSergeantClass: return new HumanCSergeantClass();
            case AvailableClasses.HumanLieutenantClass: return new HumanLieutenantClass();
            case AvailableClasses.HumanCaptainClass: return new HumanCaptainClass();
            /* lvl 4 */
            case AvailableClasses.HumanGeneralClass: return new HumanGeneralClass();
            case AvailableClasses.HumanFlankCaptainClass: return new HumanFlankCaptainClass();
            case AvailableClasses.HumanArcaneCommanderClass: return new HumanArcaneCommanderClass();
            case AvailableClasses.HumanTankCommanderClass: return new HumanTankCommanderClass();
            case AvailableClasses.HumanPrismKnightClass: return new HumanPKnightClass();
            case AvailableClasses.HumanMineSweeperClass: return new HumanMineSweeperClass();
            case AvailableClasses.HumanPlasmaMarineClass: return new HumanPMarineClass();
            case AvailableClasses.HumanQuakeEngineerClass: return new HumanQEngineerClass();

            case AvailableClasses.HumanBaseSoldier: return new HumanBaseSoldier();
            /* lvl 2 */
            case AvailableClasses.HumanBerserkerClass: return new HumanBerserkerClass();
            case AvailableClasses.HumanPaladinClass: return new HumanPaladinClass();
            /* lvl 3 */
            case AvailableClasses.HumanGPaladinClass: return new HumanGPaladinClass();
            case AvailableClasses.HumanIPaladinClass: return new HumanIPaladinClass();
            case AvailableClasses.HumanGDancerClass: return new HumanGDancerClass();
            case AvailableClasses.HumanFMarineClass: return new HumanFMarineClass();

            /* Egypt */
            case AvailableClasses.EgyptBaseMage: return new EgyptBaseMage();
            /* lvl 2 */
            case AvailableClasses.EgyptConjurerClass: return new EgyptConjurerClass();
            case AvailableClasses.EgyptDjinnClass: return new EgyptDjinnClass();
            /* lvl 3 */
            case AvailableClasses.EgyptGenieClass: return new EgyptGenieClass();
            case AvailableClasses.EgyptElementalistClass: return new EgyptElementalistClass();
            case AvailableClasses.EgyptGeomancerClass: return new EgyptGeomancerClass();
            case AvailableClasses.EgyptKoboldClass: return new EgyptKoboldClass();


            case AvailableClasses.EgyptBaseScout: return new EgyptBaseScout();
            /* lvl 2 */
            case AvailableClasses.EgyptManglerClass: return new EgyptManglerClass();
            case AvailableClasses.EgyptWhispererClass: return new EgyptWhispererClass();
            /* lvl 3 */
            case AvailableClasses.EgyptSSenseiClass: return new EgyptSSenseiClass();
            case AvailableClasses.EgyptFBenderClass: return new EgyptFBenderClass();
            case AvailableClasses.EgyptArsonistClass: return new EgyptArsonistClass();
            case AvailableClasses.EgyptRChosenClass: return new EgyptRChosenClass();

            case AvailableClasses.EgyptBaseSoldier: return new EgyptBaseSoldier();
            /* lvl 2 */
            case AvailableClasses.EgyptNomadClass: return new EgyptNomadClass();
            case AvailableClasses.EgyptScionClass: return new EgyptScionClass();
            /* lvl 3 */
            case AvailableClasses.EgyptAshClass: return new EgyptAshClass();
            case AvailableClasses.EgyptAnukeClass: return new EgyptAnukeClass();
            case AvailableClasses.EgyptBesClass: return new EgyptBesClass();
            case AvailableClasses.EgyptHapyClass: return new EgyptHapyClass();

            /* Cthulhu */
            case AvailableClasses.CthulhuBaseMage: return new CthulhuBaseMage();
            /* lvl 2 */
            case AvailableClasses.CthulhuLesserDemonClass: return new CthulhuLesserDemonClass();
            case AvailableClasses.CthulhuNecromancerClass: return new CthulhuNecromancerClass();
            /* lvl 3 */
            case AvailableClasses.CthulhuLichClass: return new CthulhuLichClass();
            case AvailableClasses.CthulhuPitLordClass: return new CthulhuPitLordClass();
            case AvailableClasses.CthulhuFurymancerClass: return new CthulhuFurymancerClass();
            case AvailableClasses.CthulhuGrandDemonClass: return new CthulhuGrandDemonClass();

            case AvailableClasses.CthulhuBaseScout: return new CthulhuBaseScout();
            /* lvl 2 */
            case AvailableClasses.CthulhuStolisClass: return new CthulhuStolisClass();
            case AvailableClasses.CthulhuTrollClass: return new CthulhuTrollClass();
            /* lvl 3 */
            case AvailableClasses.CthulhuHTotemClass: return new CthulhuHTotemClass();
            case AvailableClasses.CthulhuApparitionClass: return new CthulhuApparitionClass();
            case AvailableClasses.CthulhuPBeastClass: return new CthulhuPBeastClass();
            case AvailableClasses.CthulhuDEaterClass: return new CthulhuDEaterClass();

            case AvailableClasses.CthulhuBaseSoldier: return new CthulhuBaseSoldier();
            /* lvl 2 */
            case AvailableClasses.CthulhuVamprossClass: return new CthulhuVamprossClass();
            case AvailableClasses.CthulhuGoliathClass: return new CthulhuGoliathClass();
            /* lvl 3 */
            case AvailableClasses.CthulhuAVamprossClass: return new CthulhuAVamprossClass();
            case AvailableClasses.CthulhuSuccubusClass: return new CthulhuSuccubusClass();
            case AvailableClasses.CthulhuSBehemothClass: return new CthulhuSBehemothClass();
            case AvailableClasses.CthulhuRGiantClass: return new CthulhuRGiantClass();
            default: return new HumanBaseSoldier();
        }
    }

    public enum AvailableClasses{
        /* Human Mage */
        HumanBaseMage, HumanIWarlockClass, HumanTechnomancerClass,
        HumanWizardClass, HumanGeniusClass, HumanArchMageClass, HumanTinkererClass,
        /* lvl 4 */
        HumanGigaWizardClass, HumanIceWizardClass, HumanGrandMageClass, HumanMetalmancerClass,
        HumanBramblelockClass, HumanKootClass, HumanAstronautClass, HumanDoctorClass,

        /* Human Scout */
        HumanBaseScout, HumanCorporalClass, HumanFSergeantClass,
        HumanMSergeantClass, HumanCSergeantClass, HumanLieutenantClass, HumanCaptainClass,
        /* lvl 4 */
        HumanGeneralClass, HumanFlankCaptainClass, HumanArcaneCommanderClass, HumanTankCommanderClass,
        HumanPrismKnightClass, HumanMineSweeperClass, HumanPlasmaMarineClass, HumanQuakeEngineerClass,

        /* Human Soldier */
        HumanBaseSoldier, HumanBerserkerClass, HumanPaladinClass,
        HumanGPaladinClass, HumanGDancerClass, HumanIPaladinClass, HumanFMarineClass,

        /* Egypt Mage */
        EgyptBaseMage, EgyptConjurerClass, EgyptDjinnClass,
        EgyptGenieClass, EgyptElementalistClass, EgyptGeomancerClass, EgyptKoboldClass, 

        /* Egypt Scout */
        EgyptBaseScout, EgyptManglerClass, EgyptWhispererClass,
        EgyptSSenseiClass, EgyptFBenderClass, EgyptArsonistClass, EgyptRChosenClass,

        /* Egypt Soldier */
        EgyptBaseSoldier, EgyptNomadClass, EgyptScionClass, 
        EgyptAshClass, EgyptAnukeClass, EgyptBesClass, EgyptHapyClass,

        /* Cthulhu Mage */
        CthulhuBaseMage, CthulhuLesserDemonClass, CthulhuNecromancerClass,
        CthulhuLichClass, CthulhuPitLordClass, CthulhuFurymancerClass, CthulhuGrandDemonClass,

        /* Cthulhu Scout */
        CthulhuBaseScout, CthulhuStolisClass, CthulhuTrollClass,
        CthulhuHTotemClass, CthulhuApparitionClass, CthulhuPBeastClass, CthulhuDEaterClass,

        /* Cthulhu Soldier */
        CthulhuBaseSoldier, CthulhuVamprossClass, CthulhuGoliathClass, 
        CthulhuAVamprossClass, CthulhuSuccubusClass, CthulhuSBehemothClass, CthulhuRGiantClass,

        None
    }
}
