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
    PlayerMeta meta = BaseSaver.getPlayer();
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
      SceneManager.LoadScene("BattleScene");
    }
    else
    {
      GameObject dest;
      if (this.selected.Length > 0)
      {
        dest = GameObject.Find(this.selected);
        dest.transform.GetChild(0).gameObject.SetActive(false);
      }
      this.selected = selected;
      dest = GameObject.Find(this.selected);
      dest.transform.GetChild(0).gameObject.SetActive(true);
    }
  }

  public string GetSelect()
  {
    return selected;
  }
}
