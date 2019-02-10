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

  public void SaveMoveToScene(string save)
  {
    BaseSaver.PutSave(save);
    /*
     * Save the player demo objects
     */
    PlayerMeta player = BaseSaver.GetPlayer();
    if (player == null)
    {
      player = new PlayerMeta();
      //string[] dests = save == "sv1" ? new string[] { "Dest1" } : 
      //  (save == "sv2" ? new string[] { "Dest1", "Dest2" } : 
      //  new string[] { "Dest1", "Dest2", "Dest3" } );
      //player.stats.dests = dests;
      player.characters = new Unit[]{ Unit.BuildInitial(Unit.UnitType.Mage, BoardProxy.PLAYER_TEAM) };
      player.stats.dests = new string[] { "Dest1" };
      BaseSaver.PutPlayer(player);
    }
    //Load the scene
    SceneManager.LoadScene("MapScene");
  }

  public void ResetAll()
  {
    BaseSaver.ResetAll();
    Refresh();
  }
}
