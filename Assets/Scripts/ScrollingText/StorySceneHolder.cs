using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StorySceneHolder : MonoBehaviour {

  public TextMeshProUGUI textBox;
  public GameObject clickToContinue;

  private string[] textHolder;
  private int idx;
  private int txtIdx = 0;
  private float percentsPerSecond = 0.2f;
  private float sceneProgress = 0;

  void Awake () {
    GameMeta game = BaseSaver.GetGame();
    textHolder = new string[]{ game.EndGameDialog() };
    clickToContinue.SetActive(false);
  }

  public void Start()
  {
    textBox.text = "";
    StartCoroutine(AnimateText());
  }

  void FixedUpdate()
  {
    if (Input.anyKeyDown)
    {
      if (idx < textHolder.Length - 1 || txtIdx < textHolder[idx].Length - 1)
      {
        if (txtIdx < textHolder[idx].Length - 1)
        {
          textBox.text = textHolder[idx];
          txtIdx = textHolder[idx].Length - 1;
          clickToContinue.SetActive(true);
        }
        else
        {
          clickToContinue.SetActive(false);
          SkipToNextText();
        }
      }
    }
  }

  public void SkipToNextText()
  {
    StopAllCoroutines();
    idx++;
    StartCoroutine(AnimateText());
  }

  IEnumerator AnimateText()
  {
    char[] stops = new char[] { '.','?','!'};
    for (txtIdx = 0; txtIdx < textHolder[idx].Length; txtIdx++)
    {
      textBox.text = textHolder[idx].Substring(0, txtIdx);
      if (txtIdx > 0 && Array.IndexOf(stops, textHolder[idx][txtIdx-1]) > -1){
        yield return new WaitForSeconds(1f);
      }
      yield return new WaitForSeconds(.08f);
    }
    clickToContinue.SetActive(true);
  }
}
