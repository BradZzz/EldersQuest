//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class CharStatCalculator
//{
//    public enum UnitType
//    {
//        Mage, Scout, Soldier, None
//    };

//    public Unit BuildInitial(UnitType type, int team)
//    {
//        string uName = GetRandomName();
//        switch (type)
//        {
//          case UnitType.Mage: 
//            return new Unit(uName, uName, 1, team, 3, 2, 3, new TurnActionsBasicUnit());
//          case UnitType.Scout: 
//            return new Unit(uName, uName, 1, team, 3, 1, 3, new TurnActionsBasicUnit(2,1));
//          case UnitType.Soldier: 
//            return new Unit(uName, uName, 1, team, 4, 1, 2, new TurnActionsBasicUnit(1,2));
//          default:
//            return new Unit();
//        }
//    }

//    public static string GetCharacterDesc(UnitType type)
//    {
//        switch (type)
//        {
//          case UnitType.Mage: 
//            return "The Mage has +1 atk pwr.";
//          case UnitType.Scout: 
//            return "The Scout has +1 move per turn.";
//          case UnitType.Soldier: 
//            return "The Soldier has +1 atk per turn";
//          default:
//            return "";
//        }
//    }
  
//    public static string GetRandomName()
//    {
//        string[] firsts = new string[]{ "Phil", "Marla", "Steve" };
//        string[] lasts = new string[]{ "Hitshard", "Sweetcakes", "RoboDictator" };
//        return firsts[Random.Range(0, firsts.Length-1)] + " " + lasts[Random.Range(0, lasts.Length-1)];
//    }
//}
