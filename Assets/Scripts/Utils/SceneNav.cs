using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNav : MonoBehaviour
{
  public void MoveToScene(string scene)
  {
    SceneManager.LoadScene(scene);
  }

  public void SaveMoveToScene(string save)
  {
    BaseSaver.putSave(save);
    /*
     * Save the player demo objects
     */
    PlayerMeta player = BaseSaver.getPlayer();
    if (player == null)
    {
      player = new PlayerMeta();
      string[] dests = save == "sv1" ? new string[] { "Dest1" } : 
        (save == "sv2" ? new string[] { "Dest1", "Dest2" } : 
        new string[] { "Dest1", "Dest2", "Dest3" } );
      player.stats.dests = dests;
      BaseSaver.putPlayer(player);
    }
    //Load the scene
    SceneManager.LoadScene("MapScene");
  }
}
