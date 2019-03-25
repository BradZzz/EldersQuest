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
          "Natashia", "Bruce", "Silvio", "Paula", "Chris", "Olivia", "Byron", "Audrey", "Brier", "JoeBob", "Booster", "Tiger", "Champ", "Bullet", "Dale", "Fabio" };
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
}
