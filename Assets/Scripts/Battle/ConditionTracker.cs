using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
  Static to keep track of win conditions
*/
public class ConditionTracker : MonoBehaviour
{
    /*
      Things this class needs to do
      Take in a condition object(s)
      Evaluate Game Over Condition
      Call BoardProxy on GameOver
    */
    public static ConditionTracker instance;    

    BoardMeta board;
    bool won;
    bool unlkChar;

    void Awake()
    {
       instance = this;
       board = BaseSaver.GetBoard();
       if (board == null) {
          board = new BoardMeta();
       }
       won = false;
       unlkChar = false;
    }

    // Start is called before the first frame update
    public void EvaluateGame()
    {
        foreach (CondMeta cond in board.conditions)
        {
            switch (cond.cond)
            {
                case CondMeta.condition.Enemy:
                    if (cond.meta.Equals("All"))
                    {
                        Dictionary<int,int> teams = BoardProxy.instance.CountTeams();
                        foreach(int key in teams.Keys)
                        {
                            if (teams[key] == 0)
                            {
                                EndGame(key == BoardProxy.ENEMY_TEAM);
                            }
                        }
                    }
                    break;
                default: break;
            }
        }
    }

    void EndGame(bool won)
    {
        BoardProxy.instance.gameOverPanel.SetActive(true);
        this.won = won;
        string txt = "Defeat";
        if (won){
            List<UnitProxy> units = BoardProxy.instance.GetUnits().Where(unit => unit.GetData().GetTeam() == BoardProxy.PLAYER_TEAM).ToList();
            PlayerMeta player = BaseSaver.GetPlayer();
            List<Unit> pChars = units.Select(unit => new Unit(unit.GetData())).ToList();
            List<string> dests = new List<string>(player.stats.dests);
            foreach (string unlock in BaseSaver.GetBoard().unlocks)
            {
                if (!dests.Contains(unlock))
                {
                    dests.Add(unlock);
                    unlkChar = true;
                }
            }
            player.stats.dests = dests.ToArray();
            player.characters = pChars.ToArray();
            player.stats.dests = dests.ToArray();
            BaseSaver.PutPlayer(player);
            txt = "Victory";
        }
        BoardProxy.instance.gameOverPanel.transform.Find("GameOverHeader").GetComponent<TextMeshProUGUI>().text = txt;
    }

    public void GameOverNavController()
    {
        if (won)
        {
            string nxtScene = "MapScene";
            if (unlkChar)
            {
                nxtScene = "CharSelectScreen";
            }
            SceneManager.LoadScene(nxtScene);
        }
        else
        {
            SceneManager.LoadScene("DeathScene");
        }
    }
}
