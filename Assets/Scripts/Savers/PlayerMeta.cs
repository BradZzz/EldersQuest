using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerMeta
{
  public CharMeta[] characters;
  public string[] items;
  public StatMeta stats;
  //The current or last destination
  public string lastDest;

  public PlayerMeta()
  {
    characters = new CharMeta[0];
    items = new string[0];
    stats = new StatMeta();
  }
}
