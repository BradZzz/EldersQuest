using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class HighScoreMeta
{
  public int score;
  public GameMeta.World world;
  public Unit.FactionType faction;
  public string name;

  public static int GetCurrentScore(){
    int hScore = 0;
    PlayerMeta player = BaseSaver.GetPlayer();
    foreach(Unit unt in player.characters){
      hScore += unt.GetLvl();
    }
    return hScore;
  }

  public static void SaveCurrentScore(){
    GameMeta game = BaseSaver.GetGame();
    PlayerMeta player = BaseSaver.GetPlayer();

    List<HighScoreMeta> hScores = new List<HighScoreMeta>(game.scores);
    HighScoreMeta thisScore = new HighScoreMeta();
    thisScore.score = player.characters.Sum(pChar => pChar.GetLvl());
    thisScore.faction = player.faction;
    thisScore.world = player.world;
    thisScore.name = "tom";
    hScores.Add(thisScore);
    game.scores = hScores.ToArray();

    BaseSaver.PutGame(game);
  }

  public string ToString(){
      return "World: " + world.ToString() + " Faction: " + faction.ToString() + " Score: " + score.ToString();
  }
}
