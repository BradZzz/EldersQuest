using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TechTreeNav : MonoBehaviour
{
    public static TechTreeNav instance;

    public GameObject chrSelect;
    public GameObject chrRW;

    public GameObject glossary;
    public GameObject techSelect;
    public GameObject techNext;
    public GameObject techRW;

    public Image buttonImg;
    public Sprite battleInactive;
    public GameObject selectTag;
    public GameObject inactive;

    private Unit clickedUnit;
    private PlayerMeta player;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        RefreshSelect();

        Color mainColor = HelperScripts.GetColorByFaction(BaseSaver.GetPlayer().faction);
        mainColor.a = .8f;
        GetComponent<Image>().color = mainColor;

        selectTag.SetActive(false);
        techNext.SetActive(false);
    }

    public void RefreshSelect(){
        if (GameMeta.RosterNeedsUpgrade()) {
            buttonImg.color = new Color(1,.35f,.35f);
        } else {
            buttonImg.color = Color.white;
        }
        foreach (Transform child in chrSelect.transform)
        {
            Destroy(child.gameObject);
        }
        player = BaseSaver.GetPlayer();
        List<Unit> units = new List<Unit>(player.characters.Reverse());
        for(int i = 0; i < units.Count; i++){
            PopulateRw(units[i], i);
        }
        if (units.Count > 3) {
            inactive.SetActive(true);
            List<Unit> inact = new List<Unit>();
            for(int i = 3; i < units.Count; i++){
                inact.Add(units[i]);
            }
            inactive.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = ClassNode.GetClassBonusString(inact.ToArray());
        } else {
            inactive.SetActive(false);
        }
    }

    void RefreshTech(){
         foreach (Transform child in techSelect.transform.GetChild(1))
         {
             Destroy(child.gameObject);
         }

         foreach (Transform child in techNext.transform.GetChild(2))
         {
             Destroy(child.gameObject);
         }
    }

    void PopulateRw(Unit unt, int idx){
        GameObject nwRow = Instantiate(chrRW, chrSelect.transform);
        nwRow.GetComponent<TechTreeUnitWrap>().unit = unt;
        nwRow.GetComponent<TechTreeUnitWrap>().Refresh();
        RefreshMainPanel(nwRow, unt, idx);

        if (idx > 2) {
          //Reserves
          nwRow.GetComponent<Image>().sprite = battleInactive;
        }

        UnityEngine.Events.UnityAction action1 = () => { instance.CharClicked(unt); };
        nwRow.GetComponent<Button>().onClick.AddListener(action1);
    }

    static void RefreshMainPanel(GameObject panel, Unit unit, int unitIdx){
        Debug.Log("RefreshMainPanel: " + unit.characterMoniker);
        panel.SetActive(true);
        foreach(Transform child in panel.transform){
            if (child.name.Equals("CharImg")) {
                UnitProxy unt = ClassNode.ComputeClassBaseUnit(instance.player.faction, unit.GetUnitType(), instance.glossary.GetComponent<Glossary>());
                child.GetComponent<Image>().sprite = unt.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
            }
            if (child.name.Equals("CharName")) {
                child.GetComponent<TextMeshProUGUI>().text = unit.characterMoniker;
            }
            if (child.name.Equals("CharType")) {
                child.GetComponent<TextMeshProUGUI>().text = unit.GetCurrentClass().ClassName();
            }
            if (child.name.Equals("HPImg")) {
                child.GetChild(0).GetComponent<TextMeshProUGUI>().text = unit.GetMaxHP().ToString();
            }
            if (child.name.Equals("Lineup")) {
                if (unitIdx > 2) {
                  //Reserves
                  child.GetComponent<Image>().color = Color.red;
                  child.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Inactive";
                } else {
                  //Roster
                  child.GetComponent<Image>().color = Color.green;
                  child.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Battle Ready";

                }
            }
            if (child.name.Equals("ExpImg")) {
                string expStr = "*";
                if(unit.GetLvl() >= unit.GetCurrentClass().GetWhenToUpgrade()){
                    Debug.Log("Rotate!");
                    child.eulerAngles = new Vector3(0,0,-25);
                    iTween.RotateTo(child.gameObject,iTween.Hash(
                     "z", 25,
                     "time", 5,
                     "easetype", "easeInOutSine",
                     "looptype","pingpong"
                   ));
                    iTween.ScaleTo(child.gameObject,iTween.Hash(
                     "x", 1.3,
                     "y", 1.3,
                     "time", 2,
                     "easetype", "easeInOutSine",
                     "looptype","pingpong"
                   ));
                } else {
                   expStr = unit.GetLvl().ToString();
                }
                child.GetChild(0).GetComponent<TextMeshProUGUI>().text = expStr;
            }
            if (child.name.Equals("Stats")) {
                foreach (Transform t in child.transform)
                {
                    if (t.name.Equals("Move"))
                    {
                        RefreshSkillPnl(t, unit.GetMoveSpeed().ToString());
                    } 
                    if (t.name.Equals("AtkPwr"))
                    {
                        RefreshSkillPnl(t, unit.GetAttack().ToString());
                    }
                    if (t.name.Equals("AtkRng"))
                    {
                        RefreshSkillPnl(t, unit.GetAtkRange().ToString());
                    }
                }
            }
            if (child.name.Equals("Turn")) {
                foreach (Transform t in child.transform)
                {
                    if (t.name.Equals("MvTrn"))
                    {
                        RefreshSkillPnl(t, unit.GetTurnMoves().ToString());
                    } 
                    if (t.name.Equals("AtkTrn"))
                    {
                        RefreshSkillPnl(t, unit.GetTurnAttacks().ToString());
                    }
                }
            }
        }
    }

    public void UpgradeSelected(ClassNode cNode){
        if (clickedUnit != null) {
            clickedUnit = cNode.UpgradeCharacter(clickedUnit);
            clickedUnit.SetCurrentClass(cNode.GetType().ToString());

            PlayerMeta player = BaseSaver.GetPlayer();
            Unit unt = player.characters.Where(chr => chr.characterName.Equals(clickedUnit.characterName)).First();
            List<Unit> nwRoster = new List<Unit>();
            foreach(Unit rUnt in player.characters){
                if (rUnt.characterMoniker.Equals(clickedUnit.characterMoniker)) {
                    nwRoster.Add(clickedUnit);
                } else {
                    nwRoster.Add(rUnt);
                }
            }
            player.characters = nwRoster.ToArray();
            BaseSaver.PutPlayer(player);
            RefreshSelect();
            CharClicked(clickedUnit);
        }
    }

    //void Flush(){
    //     foreach (Transform child in techSelect.transform)
    //     {
    //         Destroy(child.gameObject);
    //     }

    //     foreach (Transform child in techNext.transform)
    //     {
    //         Destroy(child.gameObject);
    //     }
    //}

    public void CharClicked(Unit unt){
        clickedUnit = unt;

        RefreshTech();

        Debug.Log("Clicked: " + unt.characterMoniker);
        PlayerMeta player = BaseSaver.GetPlayer();
        IterateThroughTreeUp(unt.GetCurrentClass());
        IterateThroughTreeDown(unt.GetLvl(), unt.GetCurrentClass());

        //UnitProxy unt = ClassNode.ComputeClassBaseUnit(instance.player.faction, unt.GetUnitType(), instance.glossary.GetComponent<Glossary>());
        selectTag.SetActive(true);
        selectTag.transform.GetChild(0).GetComponent<Image>().sprite = ClassNode.ComputeClassBaseUnit(instance.player.faction, unt.GetUnitType(), instance.glossary.GetComponent<Glossary>())
          .transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
        selectTag.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = unt.characterMoniker;
        selectTag.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = unt.GetCurrentClass().ClassName();
    }

    void IterateThroughTreeUp(ClassNode parent){
        if (parent != null) {
            GameObject nwRow = Instantiate(techRW, techSelect.transform.GetChild(1));
            RefreshTechPanel(nwRow, parent);
            IterateThroughTreeUp(parent.GetParent());
        }
    } 

    void IterateThroughTreeDown(int lvl, ClassNode parent){
        if (parent != null && lvl >= parent.GetWhenToUpgrade()) {
          techNext.SetActive(true);
          foreach(ClassNode nd in parent.GetChildren()){
              GameObject nwRow = Instantiate(techRW, techNext.transform.GetChild(2));
              RefreshTechPanel(nwRow, nd);

              UnityEngine.Events.UnityAction action = () => { instance.UpgradeSelected(nd); };
              nwRow.GetComponent<Button>().onClick.AddListener(action);
          }
        } else {
          techNext.SetActive(false);
        }
    } 

    void RefreshTechPanel(GameObject panel, ClassNode clss){
        Debug.Log("RefreshTechPanel: " + clss.ClassName());
        foreach(Transform child in panel.transform){
            if (child.name.Equals("Class"))
            {
                child.GetComponent<TextMeshProUGUI>().text = clss.ClassName();
            }   
            if (child.name.Equals("Image"))
            {
                child.GetChild(0).GetComponent<TextMeshProUGUI>().text = clss.GetWhenToUpgrade().ToString();
            } 
            if (child.name.Equals("Desc"))
            {
                child.GetComponent<TextMeshProUGUI>().text = clss.ClassDesc();
            } 
        }
    }

    static void RefreshSkillPnl(Transform pnl, string val){
        foreach (Transform t in pnl)
        {
            if (t.name.Equals("Val"))
            {
              t.GetChild(0).GetComponent<TextMeshProUGUI>().text = val;
            }
        }
    }

    public void SaveAndReturn(){
        SavePosition();
        SceneManager.LoadScene("MapScene");
    }

    public void SavePosition(){
        PlayerMeta player = BaseSaver.GetPlayer();
        List<Unit> units = new List<Unit>();
        foreach (Transform child in chrSelect.transform)
        {
            foreach (Transform chld in child)
            {
                foreach(Unit unt in player.characters){
                    if (chld.name.Equals("CharName") && chld.GetComponent<TextMeshProUGUI>().text.Equals(unt.characterMoniker)) {
                        if (!units.Where(un => un.characterMoniker.Equals(unt.characterMoniker)).Any()) {
                            units.Add(unt);
                        }
                    }
                }
            }
        }
        units.Reverse();
        player.characters = units.ToArray();
        //player.characters.Reverse();
        Debug.Log("Player Units: " + player.characters.Length.ToString());
        BaseSaver.PutPlayer(player);
    }
}
