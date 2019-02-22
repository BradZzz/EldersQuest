using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class GameMeta
{
  public static int AMOUNT_OF_LVLS = 10;

  public Unit.FactionType[] unlockedFactions;
  public HighScoreMeta[] scores;
  public World[] unlockedWorlds;

  public GameMeta()
  {
    unlockedFactions = new Unit.FactionType[]{ Unit.FactionType.Human };
    unlockedWorlds = new World[]{ World.nile };
    scores = new HighScoreMeta[]{ };
  }

  public bool GameEnded(){
    PlayerMeta player = BaseSaver.GetPlayer();
    return player.stats.dests.Length == AMOUNT_OF_LVLS;
  }

  public string EndGameDialog(){
    PlayerMeta player = BaseSaver.GetPlayer();
    GameMeta game = BaseSaver.GetGame();

    Unit.FactionType faction = player.faction;
    World world = player.world;

    string returnStr = "Congratulations! You have completed the campaign for faction: " + faction.ToString() + "!";

    if (world != World.candy) {
        List<World> unlockedWorldsLst = new List<World>(game.unlockedWorlds);
        List<Unit.FactionType> unlockedFactionLst = new List<Unit.FactionType>(game.unlockedFactions);
        switch(faction){
            case Unit.FactionType.Human:
              if (!unlockedWorldsLst.Contains(World.mountain)) {
                  unlockedWorldsLst.Add(World.mountain);
                  unlockedFactionLst.Add(Unit.FactionType.Egypt);
                  returnStr += "\n\n Unlocked faction: " + Unit.FactionType.Egypt.ToString() + "!";
              }
              break;
            case Unit.FactionType.Egypt:
              if (!unlockedWorldsLst.Contains(World.pyramid)) {
                  unlockedWorldsLst.Add(World.pyramid);
                  unlockedFactionLst.Add(Unit.FactionType.Cthulhu);
                  returnStr += "\n\n Unlocked faction: " + Unit.FactionType.Cthulhu.ToString() + "!";
              }
              break;
            case Unit.FactionType.Cthulhu:
              if (!unlockedWorldsLst.Contains(World.candy)) {
                  unlockedWorldsLst.Add(World.candy);
                  returnStr += "\n\n Unlocked world: " + World.candy.ToString() + "!";
              }
              break;
        }
        game.unlockedFactions = unlockedFactionLst.ToArray();
        game.unlockedWorlds = unlockedWorldsLst.ToArray();
    } else {
        returnStr += "\n\n The world has been saved! Your accomplishments have been logged in the annals history.";
    }
    List<HighScoreMeta> hScores = new List<HighScoreMeta>(game.scores);
    HighScoreMeta thisScore = new HighScoreMeta();
    thisScore.score = player.characters.Sum(pChar => pChar.GetLvl());
    thisScore.faction = player.faction;
    thisScore.world = player.world;
    thisScore.name = "abc";
    hScores.Add(thisScore);
    game.scores = hScores.ToArray();
    BaseSaver.PutGame(game);

    //Reset the current save now that the factions / worlds have been unlocked and highscores saved
    BaseSaver.ResetAtSave();
    return returnStr;
  }

  public enum World {
    nile, mountain, pyramid, candy, none
  }
}
