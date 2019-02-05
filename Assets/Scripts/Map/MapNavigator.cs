using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapNavigator : MonoBehaviour
{
  private List<string> destSave;
  private string selected;

  void Awake()
  {
    selected = "";
    destSave = new List<string>(new string[] { "Dest1", "Dest2" });
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
    this.selected = selected;
  }

  public string GetSelect()
  {
    return selected;
  }
}
