using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharSelectController : MonoBehaviour
{
    public GameObject panelGroup;
    public Button contBtn;
    
    private Unit selected;
    private GameObject[] panels;
    private Unit[] selectableUnits;

    void Awake()
    {
        panels = new GameObject[panelGroup.transform.childCount];
        for (int i = 0; i < panelGroup.transform.childCount; i++)
        {
            panels[i] = panelGroup.transform.GetChild(i).gameObject;
        }
        selectableUnits = new Unit[]{
            Unit.BuildInitial(Unit.UnitType.Mage, BoardProxy.PLAYER_TEAM),
            Unit.BuildInitial(Unit.UnitType.Scout, BoardProxy.PLAYER_TEAM),
            Unit.BuildInitial(Unit.UnitType.Soldier, BoardProxy.PLAYER_TEAM),
        };
        for (int i = 0; i < selectableUnits.Length && i < panels.Length; i++)
        {
            RefreshPanel(panels[i], selectableUnits[i]);
        }
        selected = null;
        contBtn.gameObject.SetActive(false);
    }

    void RefreshPanel(GameObject pnl, Unit unt)
    {
        Debug.Log("Unit: " + unt.uType.ToString());
        foreach (Transform trns in pnl.transform)
        {
            if (trns.name.Equals("Header"))
            {
                trns.GetComponent<TextMeshProUGUI>().text = unt.uType.ToString();
            }
            if (trns.name.Equals("Desc"))
            {
                trns.GetComponent<TextMeshProUGUI>().text = Unit.GetCharacterDesc(unt.uType);
            }
        }
    }
  
    // Start is called before the first frame update
    public void Selected(int picked)
    {
        if (selected == null || selectableUnits[picked] != selected)
        {
            selected = selectableUnits[picked];
        }
        else
        {
            selected = null;
        }
        UpdateSelectedUI();
    }

    void UpdateSelectedUI()
    {
        foreach (GameObject pnl in panels){
            if (selected == null)
            {
                pnl.GetComponent<Outline>().effectColor = Color.black;
                contBtn.gameObject.SetActive(false);
            }
            else
            {
                contBtn.gameObject.SetActive(true);
                foreach (Transform trns in pnl.transform)
                {
                    if (trns.name.Equals("Header") && trns.GetComponent<TextMeshProUGUI>().text.Equals(selected.uType.ToString()))
                    {
                        pnl.GetComponent<Outline>().effectColor = Color.red;
                        break;
                    }
                    else
                    {
                        pnl.GetComponent<Outline>().effectColor = Color.black;
                    }
                }
            }
        }
    }

    public void ContinueToScene(){
        PlayerMeta player = BaseSaver.GetPlayer();
        List<Unit> units = new List<Unit>(player.characters);
        units.Add(selected);
        player.characters = units.ToArray();
        BaseSaver.PutPlayer(player);
        SceneManager.LoadScene("MapScene");
    }
}
