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
  //private float percentsPerSecond = 0.2f;
  //private float sceneProgress = 0;

  void Awake () {
    GameMeta game = BaseSaver.GetGame();
    PlayerMeta player = BaseSaver.GetPlayer();
    //textHolder = new string[]{ StoryStatic.INTRO_STRING };
    //if (game != null) {
    textHolder = new string[]{ };
    if (!GameMeta.GameEnded()) {
      List<string> txts = new List<string>();
      if (!game.intro) {
        txts = new List<string>(StoryStatic.INTRO_STRING);
        game.intro = true;
        BaseSaver.PutGame(game);
      }
      switch(player.faction) {
        case Unit.FactionType.Cthulhu:txts.Add(StoryStatic.CTHULHU_INTRO);break;
        case Unit.FactionType.Egypt:txts.Add(StoryStatic.EGYPT_INTRO);break;
        case Unit.FactionType.Human:txts.Add(StoryStatic.HUMAN_INTRO);break;
      }
      textHolder = txts.ToArray();
    } else {
      textHolder = new string[]{ game.EndGameDialog() };
      if (player.world == GameMeta.World.candy) {
        switch(player.faction) {
          case Unit.FactionType.Cthulhu:textHolder = new string[]{ StoryStatic.CTHULHU_WIN };break;
          case Unit.FactionType.Egypt:textHolder = new string[]{ StoryStatic.EGYPT_WIN };break;
          case Unit.FactionType.Human:textHolder = new string[]{ StoryStatic.HUMAN_WIN };break;
        }
      }
    }
    //}
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
      } else {
        MoveToScene();
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

  public void MoveToScene(){
    if (GameMeta.GameEnded()) {
      SceneManager.LoadScene("MainScene");
    } else {
      SceneManager.LoadScene("CharSelectScreen");
    }
  }
}
