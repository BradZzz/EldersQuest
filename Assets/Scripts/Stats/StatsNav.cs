using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsNav : MonoBehaviour
{
    public static StatsNav instance;

    public GameObject hScorePnl, classPnl, skillPnl;
    public GameObject skillRw;
    public GameObject hScoreBtn, classBtn, skillBtn;

    public enum Location{
      highscores, classes, skills, none
    }

    private Location thisLoc;

    void Awake(){
        instance = this;
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
        string pnlString = "\n";
        if (game.classesSeen.Length > 0) {
          foreach(string clss in game.classesSeen){
              pnlString += clss + ": " + StaticClassRef.GetFullClassDescription(clss) + "\n";
          }
        }
        classPnl.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = pnlString;
    }

    void PopulateHScoresPanel(){
        hScorePnl.SetActive(true); 
        hScoreBtn.GetComponent<Outline>().effectColor = Color.red;
        GameMeta game = BaseSaver.GetGame();
        string pnlString = "\n";  
        if (game.scores.Length > 0) {
          List<HighScoreMeta> scores = new List<HighScoreMeta>(game.scores);
          scores.OrderBy(scr => scr.score);
          foreach(HighScoreMeta scr in scores.Take(10)){
              pnlString += scr.ToString() + "\n";
          }
        }
        hScorePnl.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = pnlString;
    }

    void PopulateSkillsPanel(){
        skillPnl.SetActive(true); 
        skillBtn.GetComponent<Outline>().effectColor = Color.red;
        GameMeta game = BaseSaver.GetGame();
        string pnlString = "";
        if (game.skillsSeen.Length > 0) {
          game.skillsSeen = game.skillsSeen.OrderBy(nm=>nm).ToArray();
          foreach(string skll in game.skillsSeen){
              GameObject skllCpy = Instantiate(skillRw, skillPnl.transform.GetChild(0).GetChild(0));
              skllCpy.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = skll;
              Skill tSkill = Skill.ReturnSkillByString((Skill.SkillClasses)Enum.Parse(typeof(Skill.SkillClasses), skll));
              //pnlString = skll + ": " + tSkill.PrintDetails() + "\n";
              Debug.Log("pnlString: " + skll + ": " + tSkill.PrintDetails() + "\n");
              //UnityEngine.Events.UnityAction action1 = () => { instance.SetSkillInfoText(pnlString); };
              skllCpy.GetComponent<Button>().onClick.AddListener(() => { instance.SetSkillInfoText(skll + ": " + tSkill.PrintDetails() + "\n"); });
              

              //Skill tSkill = Skill.ReturnSkillByString((Skill.SkillClasses)Enum.Parse(typeof(Skill.SkillClasses), skll));
              //pnlString += skll + ": " + tSkill.PrintDetails() + "\n";
          }
        }
        skillPnl.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = pnlString;


        //GameObject nwRow = Instantiate(chrRW, chrSelect.transform);
        //nwRow.GetComponent<TechTreeUnitWrap>().unit = unt;
        //nwRow.GetComponent<TechTreeUnitWrap>().Refresh();
        //RefreshMainPanel(nwRow, unt, idx);

        //UnityEngine.Events.UnityAction action1 = () => { instance.CharClicked(unt); };
        //nwRow.GetComponent<Button>().onClick.AddListener(action1);
    }

    public void SetSkillInfoText(string msg){
        Debug.Log("SetSkillInfoText: " + msg);
        skillPnl.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = msg;
    }
}
