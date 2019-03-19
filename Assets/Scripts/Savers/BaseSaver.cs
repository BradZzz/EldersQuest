using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSaver
{
  private static string GAME = "GAME";
  private static string SAVE_NUMBER = "SAVE_NUMBER";

  private static string PLAYER = "PLAYER";
  private static string BOARD = "BOARD";
  private static string BOARDS = "BOARDS";

  public static void ResetAll()
  {
    PlayerPrefs.DeleteAll();
  }

  public static void ResetAtSave()
  {
    PlayerPrefs.DeleteKey(AdjKy(PLAYER));
    PlayerPrefs.DeleteKey(AdjKy(BOARD));
    PlayerPrefs.DeleteKey(AdjKy(BOARDS));
  }

  public static string AdjKy(string key)
  {
    return key + GetSN();
  }

  /*
   * Set Save Number
   */ 
  public static string GetSN()
  {
    return PlayerPrefs.GetString(SAVE_NUMBER);
  }

  public static void PutSave(string save)
  {
    PlayerPrefs.SetString(SAVE_NUMBER, save);

    Debug.Log("Save set: " + save);
  }

  /*
   * Save Game
   */ 
  public static void PutGame(GameMeta game)
  {
    string json = JsonUtility.ToJson(game);
    PlayerPrefs.SetString(GAME, json);

    Debug.Log("GAME set: " + GAME + ":" + json);
  }


  public static GameMeta GetGame()
  {
    string json = PlayerPrefs.GetString(GAME);
    Debug.Log("GAME got: " + GAME + ":" + json);
    if (json == null)
    {
      return null;
    }
    return JsonUtility.FromJson<GameMeta>(json);
  }

  /*
   * Save Player
   */ 
  public static void PutPlayer(PlayerMeta player)
  {
    /*
      Here is where the classes and skills need to be looked at to 
      see if the player has seen anything new
    */
    GameMeta game = BaseSaver.GetGame();
    if (game != null) {
        List<string> classesSeen = new List<string>(game.classesSeen);
        List<string> skillsSeen = new List<string>(game.skillsSeen);
        foreach(Unit unt in player.characters) {
            if (!classesSeen.Contains(unt.GetCurrentClassString())) {
                classesSeen.Add(unt.GetCurrentClassString());
            }
            foreach(string skill in unt.GetSkills()){
                if (!skillsSeen.Contains(skill)) {
                    skillsSeen.Add(skill);
                }
            }
        }
        game.classesSeen = classesSeen.ToArray();
        game.skillsSeen = skillsSeen.ToArray();
        PutGame(game);
    }

    string json = JsonUtility.ToJson(player);
    PlayerPrefs.SetString(AdjKy(PLAYER), json);

    Debug.Log("PLAYER set: " + AdjKy(PLAYER) + ":" + json);
  }


  public static PlayerMeta GetPlayer()
  {
    string json = PlayerPrefs.GetString(AdjKy(PLAYER));
    Debug.Log("PLAYER got: " + AdjKy(PLAYER) + ":" + json);
    if (json == null)
    {
      return null;
    }
    return JsonUtility.FromJson<PlayerMeta>(json);
  }

  /*
   * Save Battle
   */ 
  public static void PutBoard(BoardMeta board)
  {
    string json = JsonUtility.ToJson(board);
    PlayerPrefs.SetString(AdjKy(BOARD), json);

    Debug.Log("BOARD set: " + AdjKy(BOARD) + ":" + json);
  }


  public static BoardMeta GetBoard()
  {
    string json = PlayerPrefs.GetString(AdjKy(BOARD));
    Debug.Log("BOARD got: " + AdjKy(BOARD) + ":" + json);
    if (json == null)
    {
      return null;
    }
    return JsonUtility.FromJson<BoardMeta>(json);
  }

  /*
   * Save Battles
   */
  public static void PutBoards(BoardMeta[] board)
  {
    string json = JsonHelper.ToJson(board);
    PlayerPrefs.SetString(AdjKy(BOARDS), json);

    Debug.Log("BOARDS set: " + AdjKy(BOARDS) + ":" + json);
  }


  public static BoardMeta[] GetBoards()
  {
    string json = PlayerPrefs.GetString(AdjKy(BOARDS));
    Debug.Log("BOARDS got: " + AdjKy(BOARDS) + ":" + json);
    if (json.Length == 0)
    {
      return null;
    }
    return JsonHelper.FromJson<BoardMeta>(json);
  }
}
