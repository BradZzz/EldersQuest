using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapNavigator : MonoBehaviour
{
  public GameObject[] dests;

  private List<string> destSave;
  private string selected;

  void Awake()
  {
    selected = "";
    PlayerMeta meta = BaseSaver.GetPlayer();
    destSave = new List<string>(meta.stats.dests);
  }

  void Start()
  {
    Color c = Color.white;
    foreach (GameObject dest in dests)
    {
      if (destSave.Contains(dest.name))
      {
        dest.SetActive(true);
        dest.transform.GetChild(0).gameObject.SetActive(false);
      }
      else
      {
        dest.SetActive(false);
      }
    }
  }

  public void PutSelect(string selected)
  {
    if (this.selected == selected)
    {
      PlayerMeta player = BaseSaver.GetPlayer();
      player.lastDest = selected;
      BaseSaver.PutPlayer(player);
      /*
        TODO: This is where the board eventually needs to be laoded.
        For now we have a blank board with a few chars
      */
      BaseSaver.PutBoard(MapStatic.ReturnTestBoardDests()[selected]);
      SceneManager.LoadScene("BattleScene");
    }
    else
    {
      if (this.selected.Length > 0)
      {
        ByName(this.selected).transform.GetChild(0).gameObject.SetActive(false);
      }
      this.selected = selected;
      ByName(this.selected).transform.GetChild(0).gameObject.SetActive(true);
    }
  }

  public GameObject ByName(string objName)
  {
      foreach (GameObject dest in dests)
      {
          if (dest.name.Equals(objName))
          {
             return dest;
          }
      }
      return null;
  }

  public string GetSelect()
  {
    return selected;
  }
}
