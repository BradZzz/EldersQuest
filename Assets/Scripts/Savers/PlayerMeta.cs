using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerMeta
{
  public GameMeta.World world;
  public Unit.FactionType faction;
  public Unit[] characters;
  public string[] items;
  public StatMeta stats;
  //The current or last destination
  public string lastDest;

  public PlayerMeta()
  {
    world = GameMeta.World.nile;
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
