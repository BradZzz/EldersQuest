using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StatMeta
{
  //Dests completed
  public string[] dests;
  //Quests completed
  public string[] quests;
  //Enemies Defeated
  public string[] enemies;

  public StatMeta()
  {
    dests = new string[0];
    quests = new string[0];
    enemies = new string[0];
  }
}
