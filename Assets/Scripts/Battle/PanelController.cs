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

  public static void SwitchChar(UnitProxy unit)
  {
    instance.SwitchCharImages(unit);
    instance.SwitchCharName(unit);
    instance.SetCharHealth(unit);
  }

  void SwitchCharImages(UnitProxy unit)
  {
    if (unit == null)
    {
      ImagePanel.GetComponent<Image>().enabled = false;
    }
    else
    {
      Sprite img = unit.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
      ImagePanel.GetComponent<Image>().enabled = true;
      ImagePanel.GetComponent<Image>().sprite = img;
    }
  }

  void SwitchCharName(UnitProxy unit)
  {
    NamePanel.GetComponent<TextMeshProUGUI>().text = unit.GetData().characterMoniker;
  }

  void SetCharHealth(UnitProxy unit)
  {
    if (unit == null)
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
        t.GetComponent<Image>().fillAmount = (float) unit.GetData().GetCurrHealth() / (float)unit.GetData().mxHlth;
      } else if (t.name.Equals("HealthText"))
      {
        t.GetComponent<TextMeshProUGUI>().text = unit.GetData().GetCurrHealth().ToString() + " / " + unit.GetData().mxHlth.ToString();
      }
    }
  }
}