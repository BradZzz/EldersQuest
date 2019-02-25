﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HighScoreMeta
{
  public int score;
  public GameMeta.World world;
  public Unit.FactionType faction;
  public string name;

  public string ToString(){
      return "World: " + world.ToString() + " Faction: " + faction.ToString() + "Score: " + score.ToString();
  }
}
