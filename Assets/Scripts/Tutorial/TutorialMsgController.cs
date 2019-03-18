using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialMsgController : MonoBehaviour
{
    //public GameObject tutorialDialog;
    public int sceneTyp;

    private sceneType scene;
    private string[] buffer;
    private TextMeshProUGUI txtMsg;
    private enum sceneType{
      map, tech, clss, none
    }
    private int idx;
    
    void Awake(){
        PlayerMeta player = BaseSaver.GetPlayer();
        if (player.world == GameMeta.World.tutorial) {
          buffer = new string[]{ };
          scene = (sceneType) sceneTyp;
          switch(scene){
            case sceneType.map: buffer = StoryStatic.GetMapTutorialString(); break;
            case sceneType.tech: buffer = StoryStatic.GetTechTutorialString(); break;
            case sceneType.clss: buffer = StoryStatic.GetClassSelectTutorialString(); break;
          }
          if (buffer.Length == 0) {
             gameObject.SetActive(false);
          }
          txtMsg = transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
          idx = 0;
          txtMsg.text = buffer[idx];
        } else {
          gameObject.SetActive(false);
        }
    }

    public void Click(){
        idx++;
        if (buffer.Length > idx) {
            txtMsg.text = buffer[idx];
        } else {
            gameObject.SetActive(false);
        }
    }
}
