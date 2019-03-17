using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SideBtnPnl : MonoBehaviour
{
    public GameObject OpenBtn;
    public GameObject OpenPnl;

    void Awake(){
        Close();
    }

    public void RestartBattle(){
        SceneManager.LoadScene("BattleScene");
    }

    public void Surrender(){
        ConditionTracker.instance.EndGame(false);
    }

    // Start is called before the first frame update
    public void Open()
    {
        OpenBtn.SetActive(false);
        OpenPnl.SetActive(true);
    }

    // Update is called once per frame
    public void Close()
    {
        OpenBtn.SetActive(true);
        OpenPnl.SetActive(false);
    }
}
