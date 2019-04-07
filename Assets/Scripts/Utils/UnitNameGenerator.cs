using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitNameGenerator : MonoBehaviour
{
    public static string GenerateRandomName(List<string> dontPick, Unit.FactionType faction)
    {
        string gotName = "";
        switch(faction){
          case Unit.FactionType.Human:
            gotName = GetRandomNameHuman();
            while (dontPick.Contains(gotName))
            {
                gotName = GetRandomNameHuman();
            }
          break;
          case Unit.FactionType.Egypt:
            gotName = GetRandomNameEgypt();
            while (dontPick.Contains(gotName))
            {
                gotName = GetRandomNameEgypt();
            }
          break;
          case Unit.FactionType.Cthulhu:
            gotName = GetRandomNameCthulhu();
            while (dontPick.Contains(gotName))
            {
                gotName = GetRandomNameCthulhu();
            }
          break;
          case Unit.FactionType.None:
            gotName = GetRandomNameFinal();
            while (dontPick.Contains(gotName))
            {
                gotName = GetRandomNameFinal();
            }
          break;
        }
        return gotName;
    }
  
    static string GetRandomNameCthulhu()
    {
        string[] firsts = new string[]{ "Bhagoguboit", "Dathanep", "Delig", "Eph-bhac", "Hogyi-bhogu", "Ibbbommepho", "Ilot-egar", "Kig'thol", "Phathotha",
          "Phlasth", "Rna-ug", "Udagnekr", "Yaarlol-igha", "Yi'achol", "Zatlicho", "Cy'krndatsota", "Dhlla", "Egugugo", "Ep'sakiloth", "Hac-kegall", "Marura-dac",
          "Ph-og", "Ralacthat", "Sigugonihal", "Tho'rthacha", "Yit'krh", "Kekeha", "Oru'otho", "Otaclaste", "Sholaneh", "Th-ga" };
        string[] lasts = new string[]{ "theDestroyer", "oftheVoid", "theEternal", "theDivider", "ofthePlague", "Doomstead", "ofthePit", "theTempter",
          "Child-Eater", "Nightbringer", "Shadowslice", "Sinpeddler", "Deviltickler", "Nightshade", "Twilight", "Souleater", "Virgineater", "Pit-tempter", 
          "Poisonips", "Virginpoisoner", "Puppytickler", "Kittenmolester", "Toenibbler", "FaceEater", "BadChoiceMaker", "PoopFlinger" };
        return firsts[UnityEngine.Random.Range(0, firsts.Length)] + " " + lasts[UnityEngine.Random.Range(0, lasts.Length)];
    }

    static string GetRandomNameEgypt()
    {
        string[] firsts = new string[]{ "Abar", "Aahotepre", "Ahmes", "Akhraten", "Amasis", "Anat-her", "Ankhtifi", "Babaef", "Beketaten", "Caesarion",
          "Dakhamunzu", "Eurydice", "Gautseshen", "Haremakhet", "Harwa", "Hemetre", "Herihor", "Hornakht", "Inkaef", "Ipu", "Iynefer", "Kaaper", "Khaba", 
          "Kheti", "Lagus", "Maatkare", "Magas", "Manetho", "Menwi", "Minmose", "Nastasen", "Nebtu", "Nefertari", "Nitocris", "Osorkon", "Pabasa", "Pentu",
          "Potasimto", "Puimre", "Qen", "Rahotep", "Ramsesses", "Sabef", "Scota", "Senusret", "Setut", "Sosibius", "Taharqa",
          "Tentkheta", "Thutmose", "Tutankhamun", "Wadjitefni", "Wenennefer", "Ya'ammu", "Yakareb", "Zannanza", "Falcolm" };
        string[] lasts = new string[]{ "ofNubia", "I", "II", "III", "IV", "ofTryphaena", "theElder", "Pogonius", "Flopperfeet", "Catstein",
          "ofCyrene", "oftheSands", "Pyramidstein", "Sandford", "Falafelburg", "sonofHapu", "ofNaucratis", "Shemai", "Menkheperre", "Meritmut", "ofMatia",
          "Kalu", "Ini", "Tenry", "Duneman", "Oasister", "Scorpupunch", "Mummykink", "Scorpeon", "Gaddafi" };
        return firsts[UnityEngine.Random.Range(0, firsts.Length)] + " " + lasts[UnityEngine.Random.Range(0, lasts.Length)];
    }

    static string GetRandomNameHuman()
    {
        string[] firsts = new string[]{ "Phil", "Marla", "Steve", "Gary", "Phil", "Cindy", "Reginald", "Herbert", "Alphonse", "Gloria", "Bertram", "Silvia", 
          "Natashia", "Bruce", "Silvio", "Paula", "Chris", "Olivia", "Byron", "Audrey", "Brier", "JoeBob", "Booster", "Tiger", "Champ", "Bullet", "Dale", "Fabio",
           "Scooter" };
        string[] lasts = new string[]{ "Hitshard", "Sweetcakes", "Robobot", "Chipcheeks", "Nitro", "Flavortown", "Spinkick", "Everyman", "Walkshard", "Rocketshark", 
          "Looselips", "Karatease", "Danceswiftly", "Smoulderlust", "Vandersmoot", "Judosmith", "Eagletigerbear", "Fancypants", "Fancycheeks", "Doughnutface", "Cupcake",
          "Sparklepants", "Firefart", "Lightningtoes", "Tiddlewinks", "Turbomanian", "Earnhardt.jr", "ColaDrinker", "Hamburgler", "Diabetoes", "Bolton" };
        return firsts[UnityEngine.Random.Range(0, firsts.Length)] + " " + lasts[UnityEngine.Random.Range(0, lasts.Length)];
    }

    static string GetRandomNameFinal()
    {
        string[] firsts = new string[]{ "Susie" };
        string[] lasts = new string[]{ "Primus", "Secundus", "Tribus" };
        return firsts[UnityEngine.Random.Range(0, firsts.Length)] + " " + lasts[UnityEngine.Random.Range(0, lasts.Length)];
    }

    /*
      Catch phrases can contain as much as 20 characters
    */
    public static string GenerateRandomCatchphrase(Skill.Actions act, Unit.FactionType faction)
    {
        switch(faction){
          case Unit.FactionType.Human:
            return GetHumanCatchphrase(act);
          case Unit.FactionType.Egypt:
            return GetEgyptCatchphrase(act);
          case Unit.FactionType.Cthulhu:
            return GetCthulhuCatchphrase(act);
          default:
            return GetFinalCatchphrase(act);
        }
    }

    /*

    Humans say badass murica things

    */
    static string GetHumanCatchphrase(Skill.Actions act)
    {
        string[] phrases = new string[] { "yo" };
        switch(act){
            //OnSelected
            case Skill.Actions.None: phrases = new string[] { "roger", "listening", "standing by", "yo", "copy that", "moving", "walking",
              "hostiles sighted", "investigating", "engaging" }; break;
            case Skill.Actions.DidAttack: phrases = new string[] { "die!", "firing", "eat this!", "burn!", "murica!", "apple pies!", "neutralizing", 
              "aiming", "get down!", "boom", "dodge this", "suck on this", "for freedom!", "target acquired" }; break;
            case Skill.Actions.DidDefend: phrases = new string[] { "ouch!", "medic!", "taking damage", "oof", "mommy!", "oops..", "shields down", 
              "taking fire", "hamburgers...", "that'll scar", "don't let me die!", "i see the light", "under attack" }; break;
            case Skill.Actions.DidKill: phrases = new string[] { "hoo rah!", "take that!", "enemy down", "who's next?", "and stay down!", "via con dios" }; break;
        }
        HelperScripts.Shuffle(phrases);
        return phrases[0];
    }

    /*

    Egypt says badass culty things

    */
    static string GetEgyptCatchphrase(Skill.Actions act)
    {
        string[] phrases = new string[] { "phsaw" };
        switch(act){
            //OnSelected
            case Skill.Actions.None: phrases = new string[] { "navigating", "moving", "master?", "yes", "i obey", "for egypt", "swiftly",
              "understood", "flying", "i am commanded", "by the wings of Ra" }; break;
            case Skill.Actions.DidAttack: phrases = new string[] { "sand attack", "scarab stab", "titan punch", "fire jab",
              "whirlwind", "breath of horus", "the styx breathes", "release your soul", "give me your breath", "see your sins",
              "breathe death", "the earth taketh", "fire and sand" }; break;
            case Skill.Actions.DidDefend: phrases = new string[] { "impossible!", "witchcraft!", "mummies...", "unravelled", "beat down",
              "mortal wounds", "unholy beast", "i curse thee", "avenge me", "phsaw" }; break;
            case Skill.Actions.DidKill: phrases = new string[] { "rest", "to eternity", "immense power", "sun's blessing", "back to the earth", 
              "eternal peace", "sacrifice made", "death to interlopers" }; break;
        }
        HelperScripts.Shuffle(phrases);
        return phrases[0];
    }

    /*

    Cthulhu says evil things in latin

    */
    static string GetCthulhuCatchphrase(Skill.Actions act)
    {
        string[] phrases = new string[] { "gurgle" };
        switch(act){
            //OnSelected
            case Skill.Actions.None: phrases = new string[] { "*gurgle*", "*squish*", "*belch*", "opus dei", "ora pro nobis", "nocte", 
              "satanas graditur", "imperium", "quia sanguinem", "virgineo removete" }; break;
            case Skill.Actions.DidAttack: phrases = new string[] { "sssss", "*squaaaak*", "omnibus idem", "fiat tenebris", "nova sanguinem", 
              "lunam in potestatem", "marcescet", "infirma ad vescendum" }; break;
            case Skill.Actions.DidDefend: phrases = new string[] { "*plop*", "*oop*", "opere et veritate", "fortis et liber", "pax aeterna", 
              "deus videt omnia", "maledicentibus vobis", "omnes nos", "per vires sanguis" }; break;
            case Skill.Actions.DidKill: phrases = new string[] { "obiit", "odi et amo", "oleum camino", "omnia cum deo", "omnia omnibus", 
              "ordo ab chao", "orbis unum", "excelsior", "per festum", "inanis est", "pars requiescant in", "purgare per tenebras" }; break;
        }
        HelperScripts.Shuffle(phrases);
        return phrases[0];
    }

    static string GetFinalCatchphrase(Skill.Actions act)
    {
        string[] phrases = new string[] { "death..." };
        switch(act){
            //OnSelected
            case Skill.Actions.None: phrases = new string[] { "doom...", "la la la", "shiny", "cute", "where are you guys?" }; break;
            case Skill.Actions.DidAttack: phrases = new string[] { ":)", "are you mommy?", "you'll be my toy", "i see you", "peekaboo" };break;
            case Skill.Actions.DidDefend: phrases = new string[] { ":|", "ouchie", "that hurts", "that's not fair", "i'll tell on you" };break;
            case Skill.Actions.DidKill: phrases = new string[] { ";)", "death is my present", "let's play again", "that was fun", "i'm bored with you" };break;
        }
        HelperScripts.Shuffle(phrases);
        return phrases[0];
    }
}
