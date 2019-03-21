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

    public Glossary glossy;
    public GameObject camp1, camp2, camp3, camp4;
    public GameObject hScorePnl, classPnl, skillPnl;
    public GameObject skillRw, clssRw;
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
        //classPnl.GetComponent<Outline>().effectColor = Color.red;
        GameMeta game = BaseSaver.GetGame();
        string pnlString = "";
        if (game.classesSeen.Length > 0) {
          game.classesSeen = game.classesSeen.OrderBy(nm=>nm).ToArray();

          classPnl.transform.GetChild(0).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2 (800, 200 * game.classesSeen.Length);
          
          foreach (Transform child in classPnl.transform.GetChild(0).GetChild(0)) {
               Destroy(child.gameObject);
           }

          foreach(string clss in game.classesSeen){
              GameObject clssCpy = Instantiate(clssRw, classPnl.transform.GetChild(0).GetChild(0));
              clssCpy.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = ClassNode.FormatClass(clss);
              ClassNode nde = StaticClassRef.GetClass((StaticClassRef.AvailableClasses)Enum.Parse(typeof(StaticClassRef.AvailableClasses), clss));
              clssCpy.transform.GetChild(1).GetComponent<Image>().sprite = ClassNode.ComputeClassBaseUnit(nde, glossy).transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
              clssCpy.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = " " + ClassNode.GetFactionFromClass(clss);
              clssCpy.GetComponent<Button>().onClick.AddListener(() => { instance.SetClassInfoText(StaticClassRef.GetClassByReference(clss).ClassName() + ": " + StaticClassRef.GetFullClassDescription(clss) + "\n"); });
          }
        }
        classPnl.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = pnlString;
    }

    public void SetClassInfoText(string msg){
        Debug.Log("SetSkillInfoText: " + msg);
        classPnl.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = msg;
    }

    void PopulateHScoresPanel(){
        hScorePnl.SetActive(true); 
        hScoreBtn.GetComponent<Outline>().effectColor = Color.red;

        camp1.transform.GetChild(1).gameObject.SetActive(false);
        camp2.transform.GetChild(1).gameObject.SetActive(false);
        camp3.transform.GetChild(1).gameObject.SetActive(false);
        camp4.transform.GetChild(1).gameObject.SetActive(false);

        GameMeta game = BaseSaver.GetGame();
        foreach(GameMeta.World world in game.unlockedWorlds){
            switch(world){
                case GameMeta.World.nile:
                  camp1.transform.GetChild(1).gameObject.SetActive(true);
                  camp1.transform.GetChild(0).gameObject.SetActive(false);
                  camp1.GetComponent<Image>().enabled = false;
                  break;
                case GameMeta.World.mountain:
                  camp2.transform.GetChild(1).gameObject.SetActive(true);
                  camp2.transform.GetChild(0).gameObject.SetActive(false);
                  camp2.GetComponent<Image>().enabled = false;
                  break;
                case GameMeta.World.pyramid:
                  camp3.transform.GetChild(1).gameObject.SetActive(true);
                  camp3.transform.GetChild(0).gameObject.SetActive(false);
                  camp3.GetComponent<Image>().enabled = false;
                  break;
                case GameMeta.World.candy:
                  camp4.transform.GetChild(1).gameObject.SetActive(true);
                  camp4.transform.GetChild(0).gameObject.SetActive(false);
                  camp4.GetComponent<Image>().enabled = false;
                  break;
            }
        }
        string pnlString = "High Scores:\n\n";  
        //if (game.scores.Length > 0) {
        List<HighScoreMeta> scores = new List<HighScoreMeta>(game.scores);
        List<HighScoreMeta> camp1scrs = scores.Where(scr=>scr.world == GameMeta.World.nile).ToList();
        List<HighScoreMeta> camp2scrs = scores.Where(scr=>scr.world == GameMeta.World.mountain).ToList();
        List<HighScoreMeta> camp3scrs = scores.Where(scr=>scr.world == GameMeta.World.pyramid).ToList();
        List<HighScoreMeta> camp4scrs = scores.Where(scr=>scr.world == GameMeta.World.candy).ToList();

        camp1scrs.OrderBy(scr => scr.score);
        camp2scrs.OrderBy(scr => scr.score);
        camp3scrs.OrderBy(scr => scr.score);
        camp4scrs.OrderBy(scr => scr.score);

        pnlString += (" Campaign 1 => " + (camp1scrs.Count > 0 ? camp1scrs[0].score.ToString() : "0") + "\n");
        pnlString += (" Campaign 2 => " + (camp2scrs.Count > 0 ? camp2scrs[0].score.ToString() : "0") + "\n");
        pnlString += (" Campaign 3 => " + (camp3scrs.Count > 0 ? camp3scrs[0].score.ToString() : "0") + "\n");
        pnlString += (" Campaign 4 => " + (camp4scrs.Count > 0 ? camp4scrs[0].score.ToString() : "0") + "\n");

        //foreach(HighScoreMeta scr in scores.Take(10)){
        //    pnlString += scr.ToString() + "\n";
        //}
        //}
        hScorePnl.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = pnlString;
    }

    void PopulateSkillsPanel(){
        skillPnl.SetActive(true); 
        //skillBtn.GetComponent<Outline>().effectColor = Color.red;
        GameMeta game = BaseSaver.GetGame();
        string pnlString = "";
        if (game.skillsSeen.Length > 0) {
          game.skillsSeen = game.skillsSeen.OrderBy(nm=>nm).ToArray();
          skillPnl.transform.GetChild(0).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2 (800, 200 * game.skillsSeen.Length);

          foreach (Transform child in skillPnl.transform.GetChild(0).GetChild(0)) {
               Destroy(child.gameObject);
           }

          foreach(string skll in game.skillsSeen){
              GameObject skllCpy = Instantiate(skillRw, skillPnl.transform.GetChild(0).GetChild(0));
              skllCpy.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = skll;
              Skill tSkill = Skill.ReturnSkillByString((Skill.SkillClasses)Enum.Parse(typeof(Skill.SkillClasses), skll));
              skllCpy.GetComponent<Button>().onClick.AddListener(() => { instance.SetSkillInfoText(skll + ": " + tSkill.PrintDetails() + "\n"); });
          }
        }
        skillPnl.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = pnlString;
    }

    public void SetSkillInfoText(string msg){
        Debug.Log("SetSkillInfoText: " + msg);
        skillPnl.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = msg;
    }
}
