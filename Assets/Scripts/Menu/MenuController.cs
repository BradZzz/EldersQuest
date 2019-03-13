using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
  public TextMeshProUGUI save1;
  public TextMeshProUGUI save2;
  public TextMeshProUGUI save3;

  void Awake()
  {
    Refresh();
  }

  void Refresh()
  {
    PrintSave("sv1", save1);
    PrintSave("sv2", save2);
    PrintSave("sv3", save3);

    GameMeta game = BaseSaver.GetGame();
    if (game == null) {
        Debug.Log("Game is null");
        BaseSaver.PutGame(new GameMeta());
    } else {
        Debug.Log("Game is not null");
    }
  }

  void PrintSave(string save, TextMeshProUGUI txt)
  {
    BaseSaver.PutSave(save);
    if (BaseSaver.GetPlayer() != null)
    {
      txt.text = BaseSaver.GetPlayer().ToString();
    }
    else
    {
      txt.text = "New Save";
    }
  }

  public void MoveToTutorial()
  {
    BaseSaver.PutSave("sv0");
    /*
     * Save the player demo objects
     */
    PlayerMeta player = BaseSaver.GetPlayer();
    player = new PlayerMeta();
    player.stats.dests = new string[] { "Dest1" };
    player.faction = Unit.FactionType.Human;
    player.characters = new Unit[]{ };
    BaseSaver.PutPlayer(player);
    player.characters = new List<Unit>(new Unit[]{ Unit.BuildInitial(player.faction,  Unit.UnitType.Soldier, BoardProxy.PLAYER_TEAM) }).ToArray();
    player.world = GameMeta.World.tutorial;
    BaseSaver.PutPlayer(player);
    //Load the scene
    SceneManager.LoadScene("MapScene");
  }

  public void SaveMoveToScene(string save)
  {
    BaseSaver.PutSave(save);
    /*
     * Save the player demo objects
     */
    PlayerMeta player = BaseSaver.GetPlayer();
    string nxtScene = "MapScene";
    if (player == null)
    {
      player = new PlayerMeta();
      player.stats.dests = new string[] { "Dest1" };
      BaseSaver.PutPlayer(player);
      nxtScene = "FactionScene";
    }
    PlaySelectSaveSound();
    //Load the scene
    SceneManager.LoadScene(nxtScene);
  }

  public void ResetAtSave(string save)
  {
    BaseSaver.PutSave(save);
    BaseSaver.ResetAtSave();
    Refresh();
  }

  public void ResetAll()
  {
    BaseSaver.ResetAll();
    Refresh();
  }

    #region SFX

    private void PlaySelectSaveSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(FMODPaths.SelectSaveSound);
    }

    #endregion

}
