using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TechTreeNav : MonoBehaviour
{
    public static TechTreeNav instance;

    public GameObject chrSelect;
    public GameObject chrRW;

    public GameObject techSelect;
    public GameObject techNext;
    public GameObject techRW;

    private Unit clickedUnit;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        RefreshSelect();
    }

    void RefreshSelect(){
        foreach (Transform child in chrSelect.transform)
        {
            Destroy(child.gameObject);
        }
        PlayerMeta player = BaseSaver.GetPlayer();
        foreach(Unit unt in player.characters){
            PopulateRw(unt);
        }
    }

    void RefreshTech(){
         foreach (Transform child in techSelect.transform)
         {
             Destroy(child.gameObject);
         }

         foreach (Transform child in techNext.transform)
         {
             Destroy(child.gameObject);
         }
    }

    void PopulateRw(Unit unt){
        GameObject nwRow = Instantiate(chrRW, chrSelect.transform);
        nwRow.GetComponent<TechTreeUnitWrap>().unit = unt;
        nwRow.GetComponent<TechTreeUnitWrap>().Refresh();
        RefreshMainPanel(nwRow, unt);

        UnityEngine.Events.UnityAction action1 = () => { instance.CharClicked(unt); };
        nwRow.GetComponent<Button>().onClick.AddListener(action1);
    }

    static void RefreshMainPanel(GameObject panel, Unit unit){
        Debug.Log("RefreshMainPanel: " + unit.characterMoniker);
        panel.SetActive(true);
        foreach(Transform child in panel.transform){
            if (child.name.Equals("CharName")) {
                child.GetComponent<TextMeshProUGUI>().text = unit.characterMoniker;
            }
            if (child.name.Equals("CharType")) {
                child.GetComponent<TextMeshProUGUI>().text = unit.GetCurrentClass().ClassName();
            }
            if (child.name.Equals("HPImg")) {
                child.GetChild(0).GetComponent<TextMeshProUGUI>().text = unit.GetMaxHP().ToString();
            }
            if (child.name.Equals("ExpImg")) {
                child.GetChild(0).GetComponent<TextMeshProUGUI>().text = unit.GetLvl().ToString();
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
    }

    void IterateThroughTreeUp(ClassNode parent){
        if (parent != null) {
            GameObject nwRow = Instantiate(techRW, techSelect.transform);
            RefreshTechPanel(nwRow, parent);
            IterateThroughTreeUp(parent.GetParent());
        }
    } 

    void IterateThroughTreeDown(int lvl, ClassNode parent){
        if (parent != null && lvl >= parent.GetWhenToUpgrade()) {
            foreach(ClassNode nd in parent.GetChildren()){
                GameObject nwRow = Instantiate(techRW, techNext.transform);
                RefreshTechPanel(nwRow, nd);

                UnityEngine.Events.UnityAction action = () => { instance.UpgradeSelected(nd); };
                nwRow.GetComponent<Button>().onClick.AddListener(action);
            }
        }
    } 

    void RefreshTechPanel(GameObject panel, ClassNode clss){
        Debug.Log("RefreshTechPanel: " + clss.ClassName());
        foreach(Transform child in panel.transform){
            if (child.name.Equals("Class"))
            {
                child.GetComponent<TextMeshProUGUI>().text = clss.ClassName();
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
}
