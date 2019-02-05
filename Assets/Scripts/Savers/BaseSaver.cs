using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSaver
{
  private static string SAVE_NUMBER = "SAVE_NUMBER";

  private static string PLAYER = "PLAYER";

  public static string adjKy(string key)
  {
    return key + getSN();
  }

  public static string getSN()
  {
    return PlayerPrefs.GetString(SAVE_NUMBER);
  }

  public static void putSave(string save)
  {
    PlayerPrefs.SetString(SAVE_NUMBER, save);

    Debug.Log("Save set: " + save);
  }

  public static void putPlayer(PlayerMeta player)
  {
    string json = JsonUtility.ToJson(player);
    PlayerPrefs.SetString(adjKy(PLAYER), json);

    Debug.Log("PLAYER set: " + adjKy(PLAYER) + ":" + json);
  }


  public static PlayerMeta getPlayer()
  {
    string json = PlayerPrefs.GetString(adjKy(PLAYER));
    Debug.Log("PLAYER got: " + adjKy(PLAYER) + ":" + json);
    if (json == null)
    {
      return null;
    }
    return JsonUtility.FromJson<PlayerMeta>(json);
  }
}
