using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticClassRef
{
    public static int LEVEL1 = 2;
    public static int LEVEL2 = 5;
    public static int LEVEL3 = 11;
    public static int LEVEL4 = 27;

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
            case AvailableClasses.HumanPKnightClass: return new HumanPKnightClass();
            case AvailableClasses.HumanMineSweeperClass: return new HumanMineSweeperClass();
            case AvailableClasses.HumanPMarineClass: return new HumanPMarineClass();
            case AvailableClasses.HumanQEngineerClass: return new HumanQEngineerClass();

            case AvailableClasses.HumanBaseSoldier: return new HumanBaseSoldier();
            /* lvl 2 */
            case AvailableClasses.HumanBerserkerClass: return new HumanBerserkerClass();
            case AvailableClasses.HumanPaladinClass: return new HumanPaladinClass();
            /* lvl 3 */
            case AvailableClasses.HumanGPaladinClass: return new HumanGPaladinClass();
            case AvailableClasses.HumanIPaladinClass: return new HumanIPaladinClass();
            case AvailableClasses.HumanGDancerClass: return new HumanGDancerClass();
            case AvailableClasses.HumanFMarineClass: return new HumanFMarineClass();
            /* lvl 4 */
            case AvailableClasses.HumanBloodBenderClass: return new HumanBloodBenderClass();
            case AvailableClasses.HumanDMercenaryClass: return new HumanDMercenaryClass();
            case AvailableClasses.HumanGNinjaClass: return new HumanGNinjaClass();
            case AvailableClasses.HumanRSamuraiClass: return new HumanRSamuraiClass();
            case AvailableClasses.HumanInquisitorClass: return new HumanInquisitorClass();
            case AvailableClasses.HumanTormentorClass: return new HumanTormentorClass();
            case AvailableClasses.HumanCCrusaderClass: return new HumanCCrusaderClass();
            case AvailableClasses.HumanDCrusaderClass: return new HumanDCrusaderClass();

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
            /* lvl 4 */
            case AvailableClasses.EgyptVWhispererClass: return new EgyptVWhispererClass();
            case AvailableClasses.EgyptTConduitClass: return new EgyptTConduitClass();
            case AvailableClasses.EgyptEMoverClass: return new EgyptEMoverClass();
            case AvailableClasses.EgyptHSpeakerClass: return new EgyptHSpeakerClass();
            case AvailableClasses.EgyptWMasterClass: return new EgyptWMasterClass();
            case AvailableClasses.EgyptRSpectreClass: return new EgyptRSpectreClass();
            case AvailableClasses.EgyptBKoboldClass: return new EgyptBKoboldClass();
            case AvailableClasses.EgyptTGoblinClass: return new EgyptTGoblinClass();

            case AvailableClasses.EgyptBaseScout: return new EgyptBaseScout();
            /* lvl 2 */
            case AvailableClasses.EgyptManglerClass: return new EgyptManglerClass();
            case AvailableClasses.EgyptWhispererClass: return new EgyptWhispererClass();
            /* lvl 3 */
            case AvailableClasses.EgyptSSenseiClass: return new EgyptSSenseiClass();
            case AvailableClasses.EgyptFBenderClass: return new EgyptFBenderClass();
            case AvailableClasses.EgyptArsonistClass: return new EgyptArsonistClass();
            case AvailableClasses.EgyptRChosenClass: return new EgyptRChosenClass();
            /* lvl 4 */
            case AvailableClasses.EgyptPorcupineClass: return new EgyptPorcupineClass();
            case AvailableClasses.EgyptFEnforcerClass: return new EgyptFEnforcerClass();
            case AvailableClasses.EgyptCWalkerClass: return new EgyptCWalkerClass();
            case AvailableClasses.EgyptDCarrierClass: return new EgyptDCarrierClass();
            case AvailableClasses.EgyptPChannelerClass: return new EgyptPChannelerClass();
            case AvailableClasses.EgyptNSpeakerClass: return new EgyptNSpeakerClass();
            case AvailableClasses.EgyptFelonClass: return new EgyptFelonClass();
            case AvailableClasses.EgyptFirefighterClass: return new EgyptFirefighterClass();

            case AvailableClasses.EgyptBaseSoldier: return new EgyptBaseSoldier();
            /* lvl 2 */
            case AvailableClasses.EgyptNomadClass: return new EgyptNomadClass();
            case AvailableClasses.EgyptScionClass: return new EgyptScionClass();
            /* lvl 3 */
            case AvailableClasses.EgyptAshClass: return new EgyptAshClass();
            case AvailableClasses.EgyptAnukeClass: return new EgyptAnukeClass();
            case AvailableClasses.EgyptBesClass: return new EgyptBesClass();
            case AvailableClasses.EgyptHapyClass: return new EgyptHapyClass();
            /* lvl 4 */
            case AvailableClasses.EgyptAMessiahClass: return new EgyptAMessiahClass();
            case AvailableClasses.EgyptSActorClass: return new EgyptSActorClass();
            case AvailableClasses.EgyptAnMessiahClass: return new EgyptAnMessiahClass();
            case AvailableClasses.EgyptMSandsClass: return new EgyptMSandsClass();
            case AvailableClasses.EgyptBMessiahClass: return new EgyptBMessiahClass();
            case AvailableClasses.EgyptOracleClass: return new EgyptOracleClass();
            case AvailableClasses.EgyptHMessiahClass: return new EgyptHMessiahClass();
            case AvailableClasses.EgyptAConduitClass: return new EgyptAConduitClass();

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
            /* lvl 4 */
            case AvailableClasses.CthulhuDevilClass: return new CthulhuDevilClass();
            case AvailableClasses.CthulhuFaustusClass: return new CthulhuFaustusClass();
            case AvailableClasses.CthulhuJudasClass: return new CthulhuJudasClass();
            case AvailableClasses.CthulhuVirgilClass: return new CthulhuVirgilClass();
            case AvailableClasses.CthulhuUReaperClass: return new CthulhuUReaperClass();
            case AvailableClasses.CthulhuPApostleClass: return new CthulhuPApostleClass();
            case AvailableClasses.CthulhuPGoblinClass: return new CthulhuPGoblinClass();
            case AvailableClasses.CthulhuGDispatcherClass: return new CthulhuGDispatcherClass();

            case AvailableClasses.CthulhuBaseScout: return new CthulhuBaseScout();
            /* lvl 2 */
            case AvailableClasses.CthulhuStolisClass: return new CthulhuStolisClass();
            case AvailableClasses.CthulhuTrollClass: return new CthulhuTrollClass();
            /* lvl 3 */
            case AvailableClasses.CthulhuHTotemClass: return new CthulhuHTotemClass();
            case AvailableClasses.CthulhuApparitionClass: return new CthulhuApparitionClass();
            case AvailableClasses.CthulhuPBeastClass: return new CthulhuPBeastClass();
            case AvailableClasses.CthulhuDEaterClass: return new CthulhuDEaterClass();
            /* lvl 4 */
            case AvailableClasses.CthulhuPRiderClass: return new CthulhuPRiderClass();
            case AvailableClasses.CthulhuNyarlathotepClass: return new CthulhuNyarlathotepClass();
            case AvailableClasses.CthulhuDRiderClass: return new CthulhuDRiderClass();
            case AvailableClasses.CthulhuPBearerClass: return new CthulhuPBearerClass();
            case AvailableClasses.CthulhuFRiderClass: return new CthulhuFRiderClass();
            case AvailableClasses.CthulhuYigClass: return new CthulhuYigClass();
            case AvailableClasses.CthulhuAzathothClass: return new CthulhuAzathothClass();
            case AvailableClasses.CthulhuYogsothothClass: return new CthulhuYogsothothClass();

            case AvailableClasses.CthulhuBaseSoldier: return new CthulhuBaseSoldier();
            /* lvl 2 */
            case AvailableClasses.CthulhuVamprossClass: return new CthulhuVamprossClass();
            case AvailableClasses.CthulhuGoliathClass: return new CthulhuGoliathClass();
            /* lvl 3 */
            case AvailableClasses.CthulhuAVamprossClass: return new CthulhuAVamprossClass();
            case AvailableClasses.CthulhuSuccubusClass: return new CthulhuSuccubusClass();
            case AvailableClasses.CthulhuSBehemothClass: return new CthulhuSBehemothClass();
            case AvailableClasses.CthulhuRGiantClass: return new CthulhuRGiantClass();
            /* lvl 4 */
            case AvailableClasses.CthulhuVampireClass: return new CthulhuVampireClass();
            case AvailableClasses.CthulhuEssilexClass: return new CthulhuEssilexClass();
            case AvailableClasses.CthulhuLilithClass: return new CthulhuLilithClass();
            case AvailableClasses.CthulhuEveClass: return new CthulhuEveClass();
            case AvailableClasses.CthulhuGodzillaClass: return new CthulhuGodzillaClass();
            case AvailableClasses.CthulhuThingClass: return new CthulhuThingClass();
            case AvailableClasses.CthulhuWRiderClass: return new CthulhuWRiderClass();
            case AvailableClasses.CthulhuADeathClass: return new CthulhuADeathClass();

            /* Final */
            case AvailableClasses.FinalBaseAntichrist: return new FinalBaseAntichrist();
            case AvailableClasses.FinalBaseApocalypse: return new FinalBaseApocalypse();
            case AvailableClasses.FinalBaseChaos: return new FinalBaseChaos();
            case AvailableClasses.FinalBaseCollapse: return new FinalBaseCollapse();
            case AvailableClasses.FinalBaseDarkness: return new FinalBaseDarkness();
            case AvailableClasses.FinalBaseDireOmen: return new FinalBaseDireOmen();
            case AvailableClasses.FinalBaseDisorder: return new FinalBaseDisorder();
            case AvailableClasses.FinalBaseEternalDarkness: return new FinalBaseEternalDarkness();
            case AvailableClasses.FinalBaseEvening: return new FinalBaseEvening();
            case AvailableClasses.FinalBaseIMessiah: return new FinalBaseIMessiah();
            case AvailableClasses.FinalBaseOmen: return new FinalBaseOmen();
            case AvailableClasses.FinalBaseTwilight: return new FinalBaseTwilight();

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
        HumanPKnightClass, HumanMineSweeperClass, HumanPMarineClass, HumanQEngineerClass,

        /* Human Soldier */
        HumanBaseSoldier, HumanBerserkerClass, HumanPaladinClass,
        HumanGPaladinClass, HumanGDancerClass, HumanIPaladinClass, HumanFMarineClass,
        /* lvl 4 */
        HumanBloodBenderClass, HumanDMercenaryClass, HumanGNinjaClass, HumanRSamuraiClass,
        HumanInquisitorClass, HumanTormentorClass, HumanCCrusaderClass, HumanDCrusaderClass,

        /* Egypt Mage */
        EgyptBaseMage, EgyptConjurerClass, EgyptDjinnClass,
        EgyptGenieClass, EgyptElementalistClass, EgyptGeomancerClass, EgyptKoboldClass, 
        /* lvl 4 */
        EgyptVWhispererClass, EgyptTConduitClass, EgyptEMoverClass, EgyptHSpeakerClass,
        EgyptWMasterClass, EgyptRSpectreClass, EgyptBKoboldClass, EgyptTGoblinClass,

        /* Egypt Scout */
        EgyptBaseScout, EgyptManglerClass, EgyptWhispererClass,
        EgyptSSenseiClass, EgyptFBenderClass, EgyptArsonistClass, EgyptRChosenClass,
        /* lvl 4 */
        EgyptPorcupineClass, EgyptFEnforcerClass, EgyptCWalkerClass, EgyptDCarrierClass,
        EgyptPChannelerClass, EgyptNSpeakerClass, EgyptFelonClass, EgyptFirefighterClass,

        /* Egypt Soldier */
        EgyptBaseSoldier, EgyptNomadClass, EgyptScionClass, 
        EgyptAshClass, EgyptAnukeClass, EgyptBesClass, EgyptHapyClass,
        /* lvl 4 */
        EgyptAMessiahClass, EgyptSActorClass, EgyptAnMessiahClass, EgyptMSandsClass,
        EgyptBMessiahClass, EgyptOracleClass, EgyptHMessiahClass, EgyptAConduitClass,

        /* Cthulhu Mage */
        CthulhuBaseMage, CthulhuLesserDemonClass, CthulhuNecromancerClass,
        CthulhuLichClass, CthulhuPitLordClass, CthulhuFurymancerClass, CthulhuGrandDemonClass,
        /* lvl 4 */
        CthulhuDevilClass, CthulhuFaustusClass, CthulhuJudasClass, CthulhuVirgilClass,
        CthulhuUReaperClass, CthulhuPApostleClass, CthulhuPGoblinClass, CthulhuGDispatcherClass,

        /* Cthulhu Scout */
        CthulhuBaseScout, CthulhuStolisClass, CthulhuTrollClass,
        CthulhuHTotemClass, CthulhuApparitionClass, CthulhuPBeastClass, CthulhuDEaterClass,
        /* lvl 4 */
        CthulhuPRiderClass, CthulhuNyarlathotepClass, CthulhuDRiderClass, CthulhuPBearerClass,
        CthulhuFRiderClass, CthulhuYigClass, CthulhuAzathothClass, CthulhuYogsothothClass,

        /* Cthulhu Soldier */
        CthulhuBaseSoldier, CthulhuVamprossClass, CthulhuGoliathClass, 
        CthulhuAVamprossClass, CthulhuSuccubusClass, CthulhuSBehemothClass, CthulhuRGiantClass,
        /* lvl 4 */
        CthulhuVampireClass, CthulhuEssilexClass, CthulhuLilithClass, CthulhuEveClass,
        CthulhuGodzillaClass, CthulhuThingClass, CthulhuWRiderClass, CthulhuADeathClass,

        FinalBaseAntichrist, FinalBaseApocalypse, FinalBaseChaos, FinalBaseCollapse, FinalBaseDarkness,
        FinalBaseDireOmen, FinalBaseDisorder, FinalBaseEternalDarkness, FinalBaseEvening, FinalBaseIMessiah,
        FinalBaseOmen, FinalBaseTwilight,

        None
    }

    public static ClassNode GetClassByReference(string clss){
        return GetClass((AvailableClasses)Enum.Parse(typeof(AvailableClasses), clss));
    }

    public static string GetFullClassDescription(string clss){
      /*
        Make two classes. Figure out the differences between them:
        Atk, Atk rng, Atk trn, Mv, Mv trn, Hp, skills
      */
      ClassNode clssClss = GetClassByReference(clss);

      Unit u1 = new Unit();
      Unit u2 = new Unit();
      u2 = clssClss.UpgradeCharacter(u2);
      clssClss = clssClss.GetParent();
      while (clssClss != null) {
          u2 = clssClss.UpgradeCharacter(u2);
          clssClss = clssClss.GetParent();
      }


      string reStr = "";
      if (u1.GetAttack() != u2.GetAttack()) {
        reStr += (u2.GetAttack() > u1.GetAttack() ? "+" : "") + (u2.GetAttack() - u1.GetAttack()).ToString() + " atk. ";
      }
      if (u1.GetAtkRange() != u2.GetAtkRange()) {
        reStr += (u2.GetAtkRange() > u1.GetAtkRange() ? "+" : "") + (u2.GetAtkRange() - u1.GetAtkRange()).ToString() + " atk rng. ";
      }
      if (u1.GetTurnAttacks() != u2.GetTurnAttacks()) {
        reStr += (u2.GetTurnAttacks() > u1.GetTurnAttacks() ? "+" : "") + (u2.GetTurnAttacks() - u1.GetTurnAttacks()).ToString() + " atks trn. ";
      }
      if (u1.GetMoveSpeed() != u2.GetMoveSpeed()) {
        reStr += (u2.GetMoveSpeed() > u1.GetMoveSpeed() ? "+" : "") + (u2.GetMoveSpeed() - u1.GetMoveSpeed()).ToString() + " mv. ";
      }
      if (u1.GetTurnMoves() != u2.GetTurnMoves()) {
        reStr += (u2.GetTurnMoves() > u1.GetTurnMoves() ? "+" : "") + (u2.GetTurnMoves() - u1.GetTurnMoves()).ToString() + " mv trn. ";
      }
      if (u1.GetMaxHP() != u2.GetMaxHP()) {
        reStr += (u2.GetMaxHP() > u1.GetMaxHP() ? "+" : "") + (u2.GetMaxHP() - u1.GetMaxHP()).ToString() + " hp. ";
      }
      if (u2.GetSkills().Length > 0) {
        reStr += "Skills:";
        foreach(string skll in u2.GetSkills()){
          reStr += " " + skll;
        }
        reStr += ".";
      }
      return reStr;
    }
}
