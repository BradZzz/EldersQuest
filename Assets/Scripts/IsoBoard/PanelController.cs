using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelController : MonoBehaviour
{
  public static PanelController instance;
  public GameObject ImagePanel;

  private void Awake()
  {
    instance = this;
  }

  public void SwitchCharImages(Sprite sprite)
  {
    ImagePanel.GetComponent<Image>().sprite = sprite;
  }
}
