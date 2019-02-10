using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerMeta
{
  public Unit[] characters;
  public string[] items;
  public StatMeta stats;
  //The current or last destination
  public string lastDest;

  public PlayerMeta()
  {
    characters = new Unit[0];
    items = new string[0];
    stats = new StatMeta();
  }

  public override string ToString()
  {
      string buff = "Army: " + characters.Length + "\n";
      buff += "Battles: " + stats.dests.Length + "\n";
      return buff;
  }
}
