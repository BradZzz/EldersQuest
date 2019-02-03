using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelController : MonoBehaviour
{
  public static PanelController instance;

  public GameObject ImagePanel;
  public GameObject NamePanel;

  private void Awake()
  {
    instance = this;
  }

  private void Start()
  {
    NamePanel.GetComponent<TextMeshProUGUI>().text = "";
    ImagePanel.GetComponent<Image>().enabled = false;
  }

  public void SwitchCharImages(Sprite sprite)
  {
    if (sprite == null)
    {
      ImagePanel.GetComponent<Image>().enabled = false;
    }
    else
    {
      ImagePanel.GetComponent<Image>().enabled = true;
      ImagePanel.GetComponent<Image>().sprite = sprite;
    }
  }

  public void SwitchCharName(string name)
  {
    NamePanel.GetComponent<TextMeshProUGUI>().text = name;
  }
}
