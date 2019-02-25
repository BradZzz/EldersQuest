using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsNav : MonoBehaviour
{
    public GameObject hScorePnl, classPnl, skillPnl;

    public GameObject hScoreBtn, classBtn, skillBtn;

    public enum Location{
      highscores, classes, skills, none
    }

    private Location thisLoc;

    void Awake(){
        thisLoc = Location.highscores;
        Refresh();
    }

    public void Click(int loc){
        thisLoc = (Location)loc;

        Refresh();
    }

    void Refresh(){
        hScorePnl.SetActive(false);
        classPnl.SetActive(false);
        skillPnl.SetActive(false);

        hScoreBtn.GetComponent<Outline>().effectColor = Color.black;
        classBtn.GetComponent<Outline>().effectColor = Color.black;
        skillBtn.GetComponent<Outline>().effectColor = Color.black;

        switch(thisLoc){
            case Location.classes: PopulateClassesPanel(); break;
            case Location.highscores: PopulateHScoresPanel(); break;
            case Location.skills: PopulateSkillsPanel(); break;
        }
    }

    void PopulateClassesPanel(){
        classPnl.SetActive(true); 
        classBtn.GetComponent<Outline>().effectColor = Color.red;
        GameMeta game = BaseSaver.GetGame();
        if (game.classesSeen.Length > 0) {
          string pnlString = "";
          foreach(string clss in game.classesSeen){
              pnlString += clss + "\t";
          }
          classPnl.GetComponent<TextMeshProUGUI>().text = pnlString;
        }
    }

    void PopulateHScoresPanel(){
        hScorePnl.SetActive(true); 
        hScoreBtn.GetComponent<Outline>().effectColor = Color.red;
        GameMeta game = BaseSaver.GetGame();  
        if (game.scores.Length > 0) {
          string pnlString = "";
          foreach(HighScoreMeta scr in game.scores){
              pnlString += scr.ToString() + "\t";
          }
          hScorePnl.GetComponent<TextMeshProUGUI>().text = pnlString;
        }
    }

    void PopulateSkillsPanel(){
        skillPnl.SetActive(true); 
        skillBtn.GetComponent<Outline>().effectColor = Color.red;
        GameMeta game = BaseSaver.GetGame();
        if (game.skillsSeen.Length > 0) {
          string pnlString = "";
          foreach(string skll in game.skillsSeen){
              pnlString += skll + "\t";
          }
          skillPnl.GetComponent<TextMeshProUGUI>().text = pnlString;
        }
    }
}
