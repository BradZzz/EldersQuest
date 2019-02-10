//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//[Serializable]
//public class CharMeta
//{
//  //The name of the character (for reference in the glossary)
//  public string name = "Snoopy Bot";
//  //Unit type
//  public CharStatCalculator.UnitType uType;
//  //The lvl of the character
//  public int lvl = 1;
//  //Any additional meta we might want to add we can store here
//  public string meta;

//  public CharMeta()
//  {
//    uType = CharStatCalculator.UnitType.Soldier;
//    name = "Snoopy Bot";
//    lvl = 1;
//  }

//  public CharMeta(string name, int lvl)
//  {
//    this.name = name;
//    this.lvl = lvl;
//  }

//  public CharMeta(UnitProxy unit)
//  {
//    name = unit.GetData().characterMoniker;
//    lvl = unit.GetData().GetLvl();
//  }

//  public CharMeta(CharMeta unit)
//  {
//    name = unit.name;
//    lvl = unit.lvl;
//  }
//}
