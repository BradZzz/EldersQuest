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
    public UIGlossary uiGlossy;

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
        classBtn.GetComponent<Outline>().effectColor = Color.red;
        GameMeta game = BaseSaver.GetGame();
        string pnlString = "";

        classPnl.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = pnlString;
        classPnl.transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<Animator>().runtimeAnimatorController = null;
        classPnl.transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<ImageAnimation>().Flush();
        classPnl.transform.GetChild(1).GetChild(2).GetComponent<TextMeshProUGUI>().text = pnlString;
        classPnl.transform.GetChild(1).GetChild(3).GetComponent<TextMeshProUGUI>().text = pnlString;

        if (game.classesSeen.Length > 0) {
          game.classesSeen = game.classesSeen.OrderBy(nm=>nm).ToArray();

          classPnl.transform.GetChild(0).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2 (800, 200 * game.classesSeen.Length);
          
          foreach (Transform child in classPnl.transform.GetChild(0).GetChild(0)) {
               Destroy(child.gameObject);
           }

          string[] clssSeen = game.classesSeen.Where(clss => !(clss.Contains("BaseMage") || clss.Contains("BaseScout") || clss.Contains("BaseSoldier"))).ToArray();

          for(int i =0; i < clssSeen.Length; i++){
          //foreach(string clss in game.classesSeen.Where(clss => !(clss.Contains("BaseMage") || clss.Contains("BaseScout") || clss.Contains("BaseSoldier"))).ToArray()){
              string clss = clssSeen[i];
              GameObject clssCpy = Instantiate(clssRw, classPnl.transform.GetChild(0).GetChild(0));
              clssCpy.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = ClassNode.FormatClass(clss);
              ClassNode nde = StaticClassRef.GetClass((StaticClassRef.AvailableClasses)Enum.Parse(typeof(StaticClassRef.AvailableClasses), clss));
              UnitProxy baseUnit = ClassNode.ComputeClassBaseUnit(nde, glossy);
              clssCpy.transform.GetChild(1).GetComponent<Image>().sprite = baseUnit.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
              clssCpy.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = " " + ClassNode.GetFactionFromClass(clss);
              clssCpy.GetComponent<Button>().onClick.AddListener(() => { 
                instance.SetClassInfoText(StaticClassRef.GetFullClassDescription(clss) + "\n", StaticClassRef.GetClassByReference(clss).ClassName(), 
                  ClassNode.GetClassHeirarchyString(nde)); 
                instance.SetClassSpriteAnimator(baseUnit.transform.GetChild(0).GetComponent<Animator>()); 
              });
              if (i == 0) {
                  clssCpy.GetComponent<Button>().onClick.Invoke();
              }
          }
          classPnl.transform.GetChild(0).GetComponent<ScrollRect>().normalizedPosition = new Vector2(0, 1);
        } else {
          SetClassInfoText("", "", "No classes found yet... Explore a little bit more and maybe something will be here!");
        }
    }

    public void SetClassInfoText(string msg, string header, string desc){
        Debug.Log("SetSkillInfoText: " + msg);
        classPnl.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = msg;
        classPnl.transform.GetChild(1).GetChild(3).GetComponent<TextMeshProUGUI>().text = "<u>" + header + "</u>";
        classPnl.transform.GetChild(1).GetChild(2).GetComponent<TextMeshProUGUI>().text = desc;
    }

    public void SetClassSpriteAnimator(Animator anim){
        Debug.Log("SetSpriteAnimator");
        classPnl.transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<Animator>().runtimeAnimatorController = anim.runtimeAnimatorController;
        Debug.Log("Clicking: " + anim.transform.parent.gameObject.name);
        List<Sprite> unitSprites = new List<Sprite>(uiGlossy.GetSprites((UIGlossary.uiFX)Enum.Parse(typeof(UIGlossary.uiFX), anim.transform.parent.gameObject.name)));
        classPnl.transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<ImageAnimation>().Reset(unitSprites);
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
                case GameMeta.World.mountain:
                  camp1.transform.GetChild(1).gameObject.SetActive(true);
                  camp1.transform.GetChild(0).gameObject.SetActive(false);
                  camp1.GetComponent<Image>().enabled = false;
                  break;
                case GameMeta.World.pyramid:
                  camp2.transform.GetChild(1).gameObject.SetActive(true);
                  camp2.transform.GetChild(0).gameObject.SetActive(false);
                  camp2.GetComponent<Image>().enabled = false;
                  break;
                case GameMeta.World.candy:
                  camp3.transform.GetChild(1).gameObject.SetActive(true);
                  camp3.transform.GetChild(0).gameObject.SetActive(false);
                  camp3.GetComponent<Image>().enabled = false;
                  break;
                case GameMeta.World.final:
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
        skillBtn.GetComponent<Outline>().effectColor = Color.red;
        GameMeta game = BaseSaver.GetGame();
        string pnlString = "";

        skillPnl.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = pnlString;
        skillPnl.transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<Animator>().runtimeAnimatorController = null;
        skillPnl.transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<ImageAnimation>().Flush();
        skillPnl.transform.GetChild(1).GetChild(2).GetComponent<TextMeshProUGUI>().text = pnlString;
        skillPnl.transform.GetChild(1).GetChild(3).GetComponent<TextMeshProUGUI>().text = pnlString;

        if (game.skillsSeen.Length > 0) {
          game.skillsSeen = game.skillsSeen.OrderBy(nm=>nm).ToArray();
          skillPnl.transform.GetChild(0).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2 (800, 200 * game.skillsSeen.Length);

          foreach (Transform child in skillPnl.transform.GetChild(0).GetChild(0)) {
               Destroy(child.gameObject);
           }

          for(int i = 0; i < game.skillsSeen.Length; i++){
          //foreach(string skll in game.skillsSeen){
              string skll = game.skillsSeen[i];
              GameObject skllCpy = Instantiate(skillRw, skillPnl.transform.GetChild(0).GetChild(0));
              skllCpy.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = skll;
              Skill tSkill = Skill.ReturnSkillByString((Skill.SkillClasses)Enum.Parse(typeof(Skill.SkillClasses), skll));
              skllCpy.GetComponent<Button>().onClick.AddListener(() => { 
                instance.SetSkillInfoText(tSkill.PrintDetails() + " " + tSkill.PrintStackDetails() + "\n", skll, "");
                instance.SetSkillSpriteAnimator(tSkill.GetSkillGen());
              });
              if (i == 0) {
                  skllCpy.GetComponent<Button>().onClick.Invoke();
              }
          }
          skillPnl.transform.GetChild(0).GetComponent<ScrollRect>().normalizedPosition = new Vector2(0, 1);
        } else {
          SetSkillInfoText("", "", "No skills found yet... Explore a little bit more and maybe something will be here!");
        }
    }

    public void SetSkillInfoText(string msg, string header, string desc){
        Debug.Log("SetSkillInfoText: " + msg);
        skillPnl.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = msg;
        skillPnl.transform.GetChild(1).GetChild(3).GetComponent<TextMeshProUGUI>().text = "<u>" + header + "</u>";
        skillPnl.transform.GetChild(1).GetChild(2).GetComponent<TextMeshProUGUI>().text = desc;
    }

    public void SetSkillSpriteAnimator(Skill.SkillGen skill){
        Debug.Log("SetSpriteAnimator");
        Animator anm = Skill.ReturnSkillAnimation(skill, glossy);
        Sprite sprt = Skill.ReturnSkillSprite(skill, glossy);
        if (anm != null) {
          skillPnl.transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<Animator>().runtimeAnimatorController = anm.runtimeAnimatorController;
          List<Sprite> fxSprites = new List<Sprite>(uiGlossy.GetSprites((UIGlossary.uiFX)Enum.Parse(typeof(UIGlossary.uiFX), "fx" + skill.ToString())));
          skillPnl.transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<ImageAnimation>().Reset(fxSprites);
        }  else {
          skillPnl.transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<ImageAnimation>().Flush();
          Color wht = Color.white;
          wht.a = 1;
          skillPnl.transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<Image>().color = wht;
          skillPnl.transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<Image>().sprite = sprt;
        }
    }
}
