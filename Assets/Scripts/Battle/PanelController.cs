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
  public GameObject TeamPanel;
  public GameObject HealthPanel;
  public GameObject TurnTxt;
  public GameObject TurnTeamPnl;

  private void Awake()
  {
    instance = this;
  }

  private void Start()
  {
    NamePanel.GetComponent<TextMeshProUGUI>().text = "";
    ImagePanel.GetComponent<Image>().enabled = false;
    TeamPanel.SetActive(false);
    HealthPanel.SetActive(false);
    TurnTxt.GetComponent<TextMeshProUGUI>().text = "";
  }

  public static void SwitchChar(UnitProxy unit)
  {
    instance.SwitchCharImages(unit);
    instance.SwitchCharName(unit);
    instance.SetTeam(unit);
    instance.SetCharHealth(unit);
    instance.SetTurnText(unit);
  }

  public void SetTurnPanel(string msg)
  { 
      TurnTeamPnl.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = msg;
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
    string txt = unit == null ? "" : unit.GetData().characterMoniker;
    NamePanel.GetComponent<TextMeshProUGUI>().text = txt;
  }

  void SetTeam(UnitProxy unit)
  {
    if (unit == null)
    {
      TeamPanel.SetActive(false);
    }
    else
    {
      TeamPanel.SetActive(true);
      // TODO: Change the outline banner here based on the faction
      TeamPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = unit.GetData().GetTeam().ToString();
    }
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

  void SetTurnText(UnitProxy unit)
  {
    string txt = unit == null ? "" : unit.GetData().GetTurnActions().ToString();
    TurnTxt.GetComponent<TextMeshProUGUI>().text = txt;
  }
}