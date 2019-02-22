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
        PlayerMeta player = BaseSaver.GetPlayer();
        panels = new GameObject[panelGroup.transform.childCount];
        for (int i = 0; i < panelGroup.transform.childCount; i++)
        {
            panels[i] = panelGroup.transform.GetChild(i).gameObject;
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
        if (selected == null || selectableUnits[picked] != selected)
        {
            selected = selectableUnits[picked];
        }
        else
        {
            selected = null;
        }
        UpdateSelectedUI();
        SelectCharSound(picked);       
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
                    if (trns.name.Equals("Header") && trns.GetComponent<TextMeshProUGUI>().text.Equals(selected.GetUnitType().ToString()))
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
