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
  public GameObject HealthPanel;

  private void Awake()
  {
    instance = this;
  }

  private void Start()
  {
    NamePanel.GetComponent<TextMeshProUGUI>().text = "";
    ImagePanel.GetComponent<Image>().enabled = false;
    HealthPanel.SetActive(false);
  }

  public void SwitchCharImages(Character panelChar)
  {
    Sprite img = panelChar == null ? null : panelChar.south;
    if (img == null)
    {
      ImagePanel.GetComponent<Image>().enabled = false;
    }
    else
    {
      ImagePanel.GetComponent<Image>().enabled = true;
      ImagePanel.GetComponent<Image>().sprite = img;
    }
  }

  public void SwitchCharName(Character panelChar)
  {
    string chrName = panelChar == null ? "" : panelChar.characterMoniker;
    NamePanel.GetComponent<TextMeshProUGUI>().text = chrName;
  }

  public void SetCharHealth(Character panelChar)
  {
    if (panelChar == null)
    {
      HealthPanel.SetActive(false);
      return;
    }
    HealthPanel.SetActive(true);
    HealthPanel.GetComponent<Image>().enabled = true;
    foreach (Transform t in HealthPanel.transform)
    {
      if (t.name.Equals("HealthFillBar"))
      {
        t.GetComponent<Image>().fillAmount = (float)panelChar.GetCurrHealth() / (float)panelChar.mxHlth;
      } else if (t.name.Equals("HealthText"))
      {
        t.GetComponent<TextMeshProUGUI>().text = panelChar.GetCurrHealth().ToString() + " / " + panelChar.mxHlth.ToString();
      }
    }
  }
}
