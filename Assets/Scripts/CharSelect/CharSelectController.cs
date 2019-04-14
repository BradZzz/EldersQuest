using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharSelectController : MonoBehaviour
{
    public GameObject panelGroup;
    public GameObject glossary;
    public GameObject armyPnl;
    public Button contBtn;
    public TextMeshProUGUI rosterTxt;
    
    private Unit selected;
    private GameObject[] panels;
    private Unit[] selectableUnits;

    void Awake()
    {
        PlayerMeta player = BaseSaver.GetPlayer();
        List<string> mages = new List<string>(new string[]{ "HumanBaseMage", "EgyptBaseMage", "CthulhuBaseMage" });
        List<string> scouts = new List<string>(new string[]{ "HumanBaseScout", "EgyptBaseScout", "CthulhuBaseScout" });

        int rM = 0;
        int rSc = 0;
        int rSo = 0;

        foreach(Unit unt in player.characters){
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

        //rosterTxt.text = "Current Army: \nMage: " + rM.ToString() + "\nScout: " + rSc.ToString() + "\nSoldier: " + rSo.ToString();
        armyPnl.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = rM.ToString();
        armyPnl.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = rSc.ToString();
        armyPnl.transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = rSo.ToString();

        switch(player.faction){
          case Unit.FactionType.Cthulhu:
            armyPnl.transform.GetChild(0).GetChild(2).GetComponent<Image>().sprite = glossary.GetComponent<Glossary>().cthulhuMage.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
            armyPnl.transform.GetChild(1).GetChild(2).GetComponent<Image>().sprite = glossary.GetComponent<Glossary>().cthulhuScout.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
            armyPnl.transform.GetChild(2).GetChild(2).GetComponent<Image>().sprite = glossary.GetComponent<Glossary>().cthulhuSoldier.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
            break;
          case Unit.FactionType.Human:
            armyPnl.transform.GetChild(0).GetChild(2).GetComponent<Image>().sprite = glossary.GetComponent<Glossary>().humanMage.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
            armyPnl.transform.GetChild(1).GetChild(2).GetComponent<Image>().sprite = glossary.GetComponent<Glossary>().humanScout.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
            armyPnl.transform.GetChild(2).GetChild(2).GetComponent<Image>().sprite = glossary.GetComponent<Glossary>().humanSoldier.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
            break;
          case Unit.FactionType.Egypt:
            armyPnl.transform.GetChild(0).GetChild(2).GetComponent<Image>().sprite = glossary.GetComponent<Glossary>().egyptMage.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
            armyPnl.transform.GetChild(1).GetChild(2).GetComponent<Image>().sprite = glossary.GetComponent<Glossary>().egyptScout.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
            armyPnl.transform.GetChild(2).GetChild(2).GetComponent<Image>().sprite = glossary.GetComponent<Glossary>().egyptSoldier.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
            break;
        }


        panels = new GameObject[panelGroup.transform.childCount];
        for (int i = 0; i < panelGroup.transform.childCount; i++)
        {
            panels[i] = panelGroup.transform.GetChild(i).gameObject;
            UnitProxy unt = ClassNode.ComputeClassBaseUnit(player.faction, (Unit.UnitType) i, glossary.GetComponent<Glossary>());
            panels[i].transform.GetChild(1).GetComponent<Image>().sprite = unt.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
        }
        selectableUnits = new Unit[]{
            Unit.BuildInitial(player.faction, Unit.UnitType.Mage, BoardProxy.PLAYER_TEAM),
            Unit.BuildInitial(player.faction, Unit.UnitType.Scout, BoardProxy.PLAYER_TEAM),
            Unit.BuildInitial(player.faction,  Unit.UnitType.Soldier, BoardProxy.PLAYER_TEAM),
        };
        for (int i = 0; i < selectableUnits.Length && i < panels.Length; i++)
        {
            RefreshPanel(panels[i], selectableUnits[i]);
        }
        selected = null;
        contBtn.gameObject.SetActive(false);
        ResetParticles();

        GetComponent<Image>().color = HelperScripts.GetColorByFaction(player.faction);
    }

    void RefreshPanel(GameObject pnl, Unit unt)
    {
        Debug.Log("Unit: " + unt.GetUnitType().ToString());
        foreach (Transform trns in pnl.transform)
        {
            if (trns.name.Equals("Header"))
            {
                trns.GetComponent<TextMeshProUGUI>().text = unt.GetCurrentClass().ClassName();
            }
            if (trns.name.Equals("Desc"))
            {
                trns.GetComponent<TextMeshProUGUI>().text = unt.GetCurrentClass().ClassDesc();
            }
        }
    }
  
    // Start is called before the first frame update
    public void Selected(int picked)
    {
        ResetParticles();
        if (selected == null || selectableUnits[picked] != selected)
        {
            selected = selectableUnits[picked];
            //PopulateParticles(panels[picked].transform);
            panels[picked].GetComponent<Outline>().enabled = true;
        }
        else
        {
            selected = null;
        }
        UpdateSelectedUI();
        SelectCharSound(picked);       
    }

    public void ResetParticles(){
      foreach(GameObject panel in panels){
          panel.GetComponent<Outline>().enabled = false;
      }
    }

    public void PopulateParticles(Transform parent){
        PlayerMeta player = BaseSaver.GetPlayer();
        foreach(Transform child in parent){
            if (child.name.Equals("PrtSys")) {
                Color hColor = player.faction == Unit.FactionType.Human ? Color.blue : (player.faction == Unit.FactionType.Egypt ? Color.yellow : new Color(.4f,.2f,.6f));
                SetParticles(child, true, hColor);
            }
        }
    }

    public void SetParticles(Transform parent, bool active, Color color){
        foreach(Transform child in parent){
            child.gameObject.SetActive(active);
            if (active) {
                var main = child.gameObject.GetComponent<ParticleSystem>().main;
                main.startColor = color;
            }
        }
    }

    void UpdateSelectedUI()
    {
        foreach (GameObject pnl in panels){
            if (selected == null)
            {
                //pnl.GetComponent<Outline>().effectColor = Color.black;
                contBtn.gameObject.SetActive(false);
            }
            else
            {
                contBtn.gameObject.SetActive(true);
                //foreach (Transform trns in pnl.transform)
                //{
                //    if (trns.name.Equals("Header") && trns.GetComponent<TextMeshProUGUI>().text.Equals(selected.GetUnitType().ToString()))
                //    {
                //        pnl.GetComponent<Outline>().effectColor = Color.red;
                //        break;
                //    }
                //    else
                //    {
                //        pnl.GetComponent<Outline>().effectColor = Color.black;
                //    }
                //}
            }
        }
    }

    public void ContinueToScene(){
        PlayerMeta player = BaseSaver.GetPlayer();
        List<Unit> units = new List<Unit>(player.characters);
        units.Insert(0,selected);
        //units.Add(selected);
        player.characters = units.ToArray();

        Debug.Log("Current Player Chars: " + player.characters.Length.ToString());

        BaseSaver.PutPlayer(player);
        CharAcceptSound();
        SceneManager.LoadScene("MapScene");
    }

    #region SFX

    private void SelectCharSound(int picked)
    {
        switch (picked)
        {
            case 0:
                FMODUnity.RuntimeManager.PlayOneShot(FMODPaths.SelectCharMageSound);
                break;
            case 1:
                FMODUnity.RuntimeManager.PlayOneShot(FMODPaths.SelectCharMonsterSound);
                break;
            case 2:
                FMODUnity.RuntimeManager.PlayOneShot(FMODPaths.SelectCharCreatureSound);
                break;
        }
    }

    private void CharAcceptSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(FMODPaths.SelectCharAcceptSound);
    }

    #endregion
}
