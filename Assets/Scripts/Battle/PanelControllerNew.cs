using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelControllerNew : MonoBehaviour
{
    public static PanelControllerNew instance;

    public GameObject playerMain;
    public GameObject playerSub1;
    public GameObject playerSub2;

    public GameObject enemyMain;
    public GameObject enemySub1;
    public GameObject enemySub2;

    private List<UnitProxy> players;
    private List<UnitProxy> enemies;

    private void Awake()
    {
        instance = this;
        ClearPanels();
    }

    public void LoadInitUnits(List<UnitProxy> units){
        players = new List<UnitProxy>();
        enemies = new List<UnitProxy>();
        foreach(UnitProxy unit in units){
            if (unit.GetData().GetTeam() == BoardProxy.ENEMY_TEAM) {
                enemies.Add(unit);
            }
            if (unit.GetData().GetTeam() == BoardProxy.PLAYER_TEAM) {
                players.Add(unit);
            }
        }
    }

    public static void ClearPanels(){
        Debug.Log("Clear Panels");        

        instance.playerMain.SetActive(false);
        instance.playerSub1.SetActive(false);
        instance.playerSub2.SetActive(false);
        
        instance.enemyMain.SetActive(false);
        instance.enemySub1.SetActive(false);
        instance.enemySub2.SetActive(false);
    }

    public static void SwitchChar(UnitProxy unit)
    {
        ClearPanels();
        if (unit != null) {
            Debug.Log("SwitchChar: " + unit.GetData().characterMoniker);
            if (unit.GetData().GetTeam() == BoardProxy.PLAYER_TEAM) {
                Debug.Log("Player Panel");
                LoadPlayerPanel(unit);
            } else {
                Debug.Log("Enemy Panel");
                LoadEnemyPanel(unit);
            }
        }
    }

    static void LoadPlayerPanel(UnitProxy unit){
        List<UnitProxy> remainingPlayers = new List<UnitProxy>(instance.players);
        remainingPlayers.Remove(unit);
        LoadPanelSuite(instance.playerMain, instance.playerSub1, instance.playerSub2, unit, remainingPlayers);
    }

    static void LoadEnemyPanel(UnitProxy unit){
        List<UnitProxy> remainingEnemies = new List<UnitProxy>(instance.enemies);
        remainingEnemies.Remove(unit);
        LoadPanelSuite(instance.enemyMain, instance.enemySub1, instance.enemySub2, unit, remainingEnemies);
    }

    static void LoadPanelSuite(GameObject main, GameObject sub1, GameObject sub2, UnitProxy unit, List<UnitProxy> remainingUnits){
        remainingUnits.Remove(unit);
        RefreshMainPanel(main, unit);
        if (remainingUnits.Count > 0) {
            RefreshSubPanel(sub1, remainingUnits[0]);
        }
        if (remainingUnits.Count > 1) {
            RefreshSubPanel(sub2, remainingUnits[1]);
        }
    }

    static void RefreshMainPanel(GameObject panel, UnitProxy unit){
        panel.SetActive(true);
        foreach(Transform child in panel.transform.GetChild(0)){
            if (child.name.Equals("CharImg")) {
                child.GetComponent<Image>().sprite = unit.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
            }
            if (child.name.Equals("CharName")) {
                child.GetComponent<TextMeshProUGUI>().text = unit.GetData().characterMoniker;
            }
            if (child.name.Equals("CharType")) {
                child.GetComponent<TextMeshProUGUI>().text = unit.GetData().uType.ToString();
            }
            if (child.name.Equals("HealthOutline")) {
                foreach (Transform t in child.transform)
                {
                    if (t.name.Equals("HealthFillBar"))
                    {
                      t.GetComponent<Image>().fillAmount = (float) unit.GetData().GetCurrHealth() / (float)unit.GetData().mxHlth;
                    } else if (t.name.Equals("HealthText"))
                    {
                      t.GetComponent<TextMeshProUGUI>().text = unit.GetData().GetCurrHealth().ToString() + " / " + unit.GetData().mxHlth.ToString();
                    }
                }
            }
            if (child.name.Equals("Stats")) {
                foreach (Transform t in child.transform)
                {
                    if (t.name.Equals("Move"))
                    {
                        RefreshSkillPnl(t, unit.GetData().moveSpeed.ToString());
                    } 
                    if (t.name.Equals("AtkPwr"))
                    {
                        RefreshSkillPnl(t, unit.GetData().atk.ToString());
                    }
                    if (t.name.Equals("AtkRng"))
                    {
                        RefreshSkillPnl(t, unit.GetData().atkRange.ToString());
                    }
                }
            }
            if (child.name.Equals("Turn")) {
                foreach (Transform t in child.transform)
                {
                    if (t.name.Equals("MvTrn"))
                    {
                        RefreshSkillPnl(t, unit.GetData().GetTurnActions().mv.ToString());
                    } 
                    if (t.name.Equals("AtkTrn"))
                    {
                        RefreshSkillPnl(t, unit.GetData().GetTurnActions().atk.ToString());
                    }
                }
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

    static void RefreshSubPanel(GameObject panel, UnitProxy unit){
        panel.SetActive(true);
        foreach(Transform child in panel.transform){
            if (child.name.Equals("type")) {
                child.GetComponent<TextMeshProUGUI>().text = unit.GetData().uType.ToString();
            }
            if (child.name.Equals("mv")) {
                child.GetChild(0).GetComponent<TextMeshProUGUI>().text = unit.GetData().GetTurnActions().mv.ToString();
            }
            if (child.name.Equals("atk")) {
                child.GetChild(0).GetComponent<TextMeshProUGUI>().text = unit.GetData().GetTurnActions().atk.ToString();
            }
            if (child.name.Equals("HealthOutline")) {
                child.GetChild(0).GetComponent<Image>().fillAmount = (float) unit.GetData().GetCurrHealth() / (float)unit.GetData().mxHlth;
            }
        }
    }
}
