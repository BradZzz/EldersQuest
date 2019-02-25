using System.Collections;
using System.Collections.Generic;
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
            case Location.classes: classPnl.SetActive(true); classBtn.GetComponent<Outline>().effectColor = Color.red; break;
            case Location.highscores: hScorePnl.SetActive(true); hScoreBtn.GetComponent<Outline>().effectColor = Color.red; break;
            case Location.skills: skillPnl.SetActive(true); skillBtn.GetComponent<Outline>().effectColor = Color.red; break;
        }
    }
}
