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
  public GameObject[] candles;
  public GameObject[] lights;

  private string[] textHolder;
  private int idx;
  private int txtIdx = 0;
  private int candleIdx;
  private int lightIdx;
  private bool waiting;

  private IEnumerator waiter;
  private IEnumerator lightNUM;
  private IEnumerator candleNUM;
  private bool gameEnded;

    private FMOD.Studio.EventInstance cutsceneSnapshot;
  //private float percentsPerSecond = 0.2f;
  //private float sceneProgress = 0;

  void Awake () {
    GameMeta game = BaseSaver.GetGame();
    PlayerMeta player = BaseSaver.GetPlayer();
    waiting = false;
    candleIdx = 0;
    lightIdx = 0;
        //lightNUM = SwitchLights();
        //candleNUM = SwitchCandles();
        cutsceneSnapshot = FMODUnity.RuntimeManager.CreateInstance(FMODPaths.CutsceneSnapshot);
    StartCoroutine(SwitchLights());
    StartCoroutine(SwitchCandles());
    textHolder = new string[]{ };
    gameEnded = GameMeta.GameEnded();
    if (!gameEnded) {
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
      if (player.world == GameMeta.World.tutorial) {
        textHolder = new string[]{ "Congratulations! Tutorial completed. Good luck with the main story!" };
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
    }
    clickToContinue.SetActive(false);
  }

  IEnumerator SwitchCandles(){
      candles[candleIdx].SetActive(false);
      //candles[candleIdx].
      candleIdx = candleIdx == 0 ? 1 : 0;
      candles[candleIdx].SetActive(true);
      yield return new WaitForSeconds(.3f);
      StartCoroutine(SwitchCandles());
  }

  IEnumerator SwitchLights(){
      lights[lightIdx].SetActive(false);
      lightIdx = lightIdx == 0 ? 1 : 0;
      lights[lightIdx].SetActive(true);
      yield return new WaitForSeconds(1f);
      StartCoroutine(SwitchLights());
  }

  public void Start()
  {
    textBox.text = "";
    StartCoroutine(AnimateText());
    cutsceneSnapshot.start();
  }

  void FixedUpdate()
  {
    if (Input.anyKeyDown)
    {
      if (!waiting) {
        if (waiter == null) {
            waiter = WaitForTouch();
        }
        StartCoroutine(waiter);
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
      } else {
        Debug.Log("Waiting");
      }
    }
  }

  IEnumerator WaitForTouch(){
    waiting = true;
    yield return new WaitForSeconds(.5f);
    waiting = false;
  }

  public void SkipToNextText()
  {
    StopAllCoroutines();
    StartCoroutine(SwitchLights());
    StartCoroutine(SwitchCandles());
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
            FMODUnity.RuntimeManager.PlayOneShot(FMODPaths.TypeSound);
            yield return new WaitForSeconds(.08f);
    }
    clickToContinue.SetActive(true);
  }

  public void MoveToScene(){
    if (gameEnded) {
      SceneManager.LoadScene("MainScene");
    } else {
      SceneManager.LoadScene("CharSelectScreen");
    }
        cutsceneSnapshot.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        cutsceneSnapshot.release();
  }
}
