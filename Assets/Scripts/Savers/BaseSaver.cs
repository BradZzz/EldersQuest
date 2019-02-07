using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSaver
{
  private static string SAVE_NUMBER = "SAVE_NUMBER";

  private static string PLAYER = "PLAYER";
  private static string BOARD = "BOARD";

  public static void ResetAll()
  {
    PlayerPrefs.DeleteAll();
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
   * Save Player
   */ 
  public static void PutPlayer(PlayerMeta player)
  {
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
}
