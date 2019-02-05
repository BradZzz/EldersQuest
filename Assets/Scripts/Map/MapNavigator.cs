using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapNavigator : MonoBehaviour
{
  private List<string> destSave;
  private string selected;

  void Awake()
  {
    selected = "";
    destSave = new List<string>(new string[] { "Dest1", "Dest2", "Dest3" });
  }

  void Start()
  {
    Color c = Color.white;
    int cnt = 1;
    string nme = "Dest" + cnt.ToString();
    GameObject pnt = GameObject.Find(nme);
    while (pnt)
    {
      if (destSave.Contains(nme))
      {
        pnt.SetActive(true);
        pnt.transform.GetChild(0).gameObject.SetActive(false);
      }
      else
      {
        pnt.SetActive(false);
      }
      cnt++;
      nme = "Dest" + cnt.ToString();
      pnt = GameObject.Find(nme);
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
