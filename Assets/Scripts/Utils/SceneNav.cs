using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNav : MonoBehaviour
{
  public void ResetAll()
  {
    BaseSaver.ResetAll();
  }

  public void MoveToScene(string scene)
  {
    SceneManager.LoadScene(scene);
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
      string[] dests = save == "sv1" ? new string[] { "Dest1" } : 
        (save == "sv2" ? new string[] { "Dest1", "Dest2" } : 
        new string[] { "Dest1", "Dest2", "Dest3" } );
      player.stats.dests = dests;
      BaseSaver.PutPlayer(player);
    }
    //Load the scene
    SceneManager.LoadScene("MapScene");
  }
}
