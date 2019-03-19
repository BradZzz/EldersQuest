using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapNavigator : MonoBehaviour
{
  public Sprite compDest;
  public GameObject[] w0Dests;
  public GameObject[] w1Dests;
  public GameObject[] w2Dests;
  public GameObject[] w3Dests;
  public GameObject[] w4Dests;
  public GameObject descPnl;
  public GameObject map;

  private GameObject[] dests;
  private List<string> destSave;
  private List<GameObject> openDests;
  private string selected;

  private Vector3 mapStartPos;
  private Vector3 mapEndPos;

  void Awake()
  {
    selected = "";
    PlayerMeta player = BaseSaver.GetPlayer();
    BoardMeta[] boards = BaseSaver.GetBoards();
    destSave = new List<string>(player.stats.dests);
    if (boards == null) {
        //SceneManager.LoadScene("ScrollingTextScene");
        Dictionary<string,BoardMeta> bMet = MapStatic.ReturnTestBoardDests(player.world == GameMeta.World.tutorial);
        List<BoardMeta> bList = new List<BoardMeta>();
        foreach(string key in bMet.Keys){
            bList.Add(bMet[key]);
        }
        BaseSaver.PutBoards(bList.ToArray());
    }
    if (GameMeta.GameEnded()) {
        SceneManager.LoadScene("ScrollingTextScene");
    }
    if (GameMeta.RosterNeedsUpgrade()) {
        SceneManager.LoadScene("TechScene");
    }
    ChangeDests(w0Dests,false);
    ChangeDests(w1Dests,false);
    ChangeDests(w2Dests,false);
    ChangeDests(w3Dests,false);
    ChangeDests(w4Dests,false);
    descPnl.SetActive(false);

    float time = player.stats.dests.Length == 1 ? 2 : .8f;
    switch(player.world){
        case GameMeta.World.mountain: 
          StartCoroutine(ZoomScale(new Vector3(850,-175,0),w2Dests,time));
        break;
        case GameMeta.World.pyramid:
          StartCoroutine(ZoomScale(new Vector3(-600,-175,0),w3Dests,time));
        break;
        case GameMeta.World.candy: 
          StartCoroutine(ZoomScale(new Vector3(-600,225,0),w4Dests,time));
        break;
        case GameMeta.World.tutorial: 
          StartCoroutine(ZoomScale(new Vector3(850,225,0),w0Dests,time));
        break;
        default:
          StartCoroutine(ZoomScale(new Vector3(850,225,0),w1Dests,time));
          break;
    }
  }

  IEnumerator ZoomScale(Vector3 newPos, GameObject[] wDests, float time){
    iTween.ScaleTo(map, iTween.Hash(
       "x", 1.8,
       "y", 1.8,
       "time", time,
       "easetype", "easeInOutSine"
    ));
    mapStartPos = map.GetComponent<RectTransform>().localPosition;
    mapEndPos = newPos;

    iTween.ValueTo(gameObject, iTween.Hash(
         "from", 0f,
         "to", 1f,
         "time", time,
         "onupdate", "MoveMap"));
    yield return new WaitForSeconds(time + .2f);
    ChangeDests(wDests,true);
    dests = wDests;
    Init();
  }

  IEnumerator ZoomOffset(Vector3 newPos, GameObject[] wDests, float time){
    /*
        Offset the position given by the difference between the first dest and the current dest
    */


    //iTween.ScaleTo(map, iTween.Hash(
    //   "x", 1.8,
    //   "y", 1.8,
    //   "time", time,
    //   "easetype", "easeInOutSine"
    //));
    mapStartPos = map.GetComponent<RectTransform>().localPosition;
    mapEndPos = newPos;

    iTween.ValueTo(gameObject, iTween.Hash(
         "from", 0f,
         "to", 1f,
         "time", time,
         "onupdate", "MoveMap"));
    yield return new WaitForSeconds(time + .2f);
    ChangeDests(wDests,true);
    dests = wDests;
    Init();
  }

  public void MoveMap(float perc){
      map.GetComponent<RectTransform>().localPosition = ((mapEndPos - mapStartPos) * perc) + mapStartPos;
  }

  void ChangeDests(GameObject[] iDests, bool valid){
      List<string> compDests = CompDests();
      foreach(GameObject iDe in iDests){
          iDe.SetActive(valid);
          if (valid && compDests.Contains(iDe.name)) {
              iDe.GetComponent<Image>().sprite = compDest;
          }
      }
  }

  public void GoBack(){
    StopMusic();
    SceneManager.LoadScene("MainScene");
  }

  void Init()
  {
    Debug.Log("Start");
    PlayerMeta player = BaseSaver.GetPlayer();

    StopMusic();
    //AudioManager.instance.SetParameterInt(AudioManager.instance.music, FMODPaths.TransitionParameter, 0);

    Color c = Color.white;
    openDests = new List<GameObject>();
    foreach (GameObject dest in dests)
    {
      if (destSave.Contains(dest.name))
      {
        dest.SetActive(true);
        dest.GetComponent<LineRenderer>().enabled = false;
        dest.transform.GetChild(0).gameObject.SetActive(false);
        openDests.Add(dest);
      }
      else
      {
        dest.SetActive(false);
      }
    }
    openDests[openDests.Count - 1].GetComponent<LineRenderer>().enabled = false;
    StartCoroutine(DrawLinesInOrder(openDests));
    //if (player.stats.dests.Length > 1) {
    //    Vector3 diff = dests[dests.Length - 1].transform.position - dests[0].transform.position;
    //    Vector3 currentPos = map.GetComponent<RectTransform>().localPosition;

    //    Debug.Log("currentPos: " + currentPos.ToString());
    //    Debug.Log("diff: " + diff.ToString());

    //    StartCoroutine(ZoomOffset(currentPos + diff,w1Dests,1));
    //}
    //DrawBetweenDests(openDests);
  }

  IEnumerator DrawLinesInOrder(List<GameObject> oDests)
  {

    //lineRenderer = gameObject.GetComponent<LineRenderer>();
    //lineRenderer.positionCount = 2;
    //lineRenderer.SetPosition(0, p0);

    //distance = Vector3.Distance(p0, p1);
    oDests.RemoveAt(oDests.Count-1);
    Debug.Log("Dests: " + oDests.Count.ToString());
    foreach (GameObject dest in oDests)
    {
      Debug.Log("Name: " + dest.name);
      dest.GetComponent<LineRenderer>().enabled = true;

      Vector3 p0 = Vector3.zero;
      Vector3 p1 = dest.GetComponent<LineRenderer>().GetPosition(1);
  
      float counter = 0;
      //float lineDrawSpeed = 2f;
      float distance = Vector3.Distance(Vector3.zero, p1);
      
      while (counter < 1)
      {
          counter += .05f;
          float x = Mathf.Lerp(0, distance, counter);
          Vector3 pointALongLine= x * Vector3.Normalize(p1 - p0);
          dest.GetComponent<LineRenderer>().SetPosition(1, pointALongLine);
          yield return new WaitForSeconds(.1f);
          Debug.Log("pointALongLine: " + pointALongLine.ToString() + "counter: " + counter.ToString() + " distance: " + distance.ToString());
      }
    }
  }

  void DrawBetweenDests(List<GameObject> oDests)
  {
    for (int i = 0; i < oDests.Count; i++)
    {
      LineRenderer line = oDests[i].GetComponent<LineRenderer>();
      if (i != oDests.Count - 1)
      {
        line.enabled = true;
        line.startWidth = 1f;
        line.endWidth = 1f;
        line.positionCount = 2;
        //Vector3 start = new Vector3(oDests[i].transform.position.x, oDests[i].transform.position.y, 0);
  
        Vector3 finish = oDests[i+1].GetComponent<RectTransform>().anchoredPosition3D;
        //Debug.Log("Start: " + start.ToString());
        Debug.Log("Finish: " + finish.ToString());
        //line.SetPosition(0, start);
        line.SetPosition(1, finish);
      }
      else
      {
        line.enabled = false;
      }
    }
  }

  List<string> CompDests(){
    PlayerMeta player = BaseSaver.GetPlayer();
    List<string> compDests = new List<string>(player.stats.dests);
    compDests.RemoveAt(compDests.Count - 1);
    return compDests;
  }

  public void PutSelect(string selected)
  {
    PlayerMeta player = BaseSaver.GetPlayer();
    BoardMeta selectedBoard = BaseSaver.GetBoards()[(int)(StoryStatic.Dests)Enum.Parse(typeof(StoryStatic.Dests), selected)];

    if (this.selected == selected && !CompDests().Contains(selected))
    {
      player.lastDest = selected;
      BaseSaver.PutPlayer(player);
      BaseSaver.PutBoard(selectedBoard);
      MusicTransitionToBattle();
      SceneManager.LoadScene("BattleScene");
      //MusicTransitionToBattle();
    }
    else
    {
      BoardMeta bMeta = BaseSaver.GetBoard();
      if (this.selected.Length > 0)
      {
        ByName(this.selected).transform.GetChild(0).gameObject.SetActive(false);
      }
      this.selected = selected;
      ByName(this.selected).transform.GetChild(0).gameObject.SetActive(true);

//<color=#9BC2C2>Map: Dest2</color>

//<color=#CFE4E4>Pwr: 2</color>

//<color=#669999>Mage: 0
//Scout: 1
//Soldier: 0</color>

      setDesc("<color=#9BC2C2>Map: " + this.selected + "</color>\n\n<color=#CFE4E4>" + selectedBoard.ReturnMapDesc() + 
        "</color>\n<color=#669999>" + CalcArmyString(selectedBoard.enemies) + "</color>");
    }
  }

  public string CalcArmyString(Unit[] chars){
      PlayerMeta player = BaseSaver.GetPlayer();

      List<string> mages = new List<string>(new string[]{ "HumanBaseMage", "EgyptBaseMage", "CthulhuBaseMage" });
      List<string> scouts = new List<string>(new string[]{ "HumanBaseScout", "EgyptBaseScout", "CthulhuBaseScout" });

      int rM = 0;
      int rSc = 0;
      int rSo = 0;

      foreach(Unit unt in chars){
        ClassNode clss = unt.GetCurrentClass();
        while(clss.GetParent() != null){
            clss = clss.GetParent();
        }
        if (mages.Contains(clss.GetType().ToString())) {
          rM++;
        } else if (scouts.Contains(clss.GetType().ToString())) {
          rSc++;
        } else {
          rSo++;
        }
      }

      return "\nMage: " + rM.ToString() + "\nScout: " + rSc.ToString() + "\nSoldier: " + rSo.ToString();
  }

  public GameObject ByName(string objName)
  {
      foreach (GameObject dest in dests)
      {
          if (dest.name.Equals(objName))
          {
             return dest;
          }
      }
      return null;
  }

  public string GetSelect()
  {
    return selected;
  }

  public void setDesc(string msg){
      if (msg.Length == 0) {
          descPnl.SetActive(false);
      } else {
          descPnl.SetActive(true);
          descPnl.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = msg;
      }
  }


    #region Music Transition

    private void MusicTransitionToBattle()
    {
        AudioManager.instance.SetParameterInt(AudioManager.instance.music, FMODPaths.TransitionParameter, 1);
        //StartMusic();
    }

    private void StartMusic()
    {
        //AudioManager.instance.PlayMusic();
    }

    private void StopMusic()
    {
        //AudioManager.instance.StopMusicImmediate();
    }

    #endregion

}