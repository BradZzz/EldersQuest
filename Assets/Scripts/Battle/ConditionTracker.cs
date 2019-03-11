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
                                StartCoroutine(WaitEndGame(key));
                            }
                        }
                    }
                    break;
                default: break;
            }
        }
    }

    IEnumerator WaitEndGame(int key){
        yield return new WaitForSeconds(1f);
        EndGame(key == BoardProxy.ENEMY_TEAM);
    }

    public void EvalDeath(UnitProxy unit){
        BoardProxy.instance.GetTileAtPosition(unit.GetPosition()).RemoveGridObjectProxy(unit);
        Destroy(unit.gameObject);
        ConditionTracker.instance.EvaluateGame();
    }

    void EndGame(bool won)
    {
        Debug.Log("EndGame: " + won.ToString());
        //while (!AnimationInteractionController.AllAnimationsFinished()) {}
        //BoardProxy.instance.gameOverPanel.SetActive(true);
        //this.won = won;
        //string txt = "Defeat";
        //if (won){
        //    List<UnitProxy> units = BoardProxy.instance.GetUnits().Where(unit => unit.GetData().GetTeam() == BoardProxy.PLAYER_TEAM 
        //      && unit.GetData().GetCurrHealth() > 0 && !unit.GetData().GetSummoned()).ToList();
        //    PlayerMeta player = BaseSaver.GetPlayer();
        //    List<Unit> pChars = units.Select(unit => new Unit(unit.GetData())).ToList();
        //    List<string> dests = new List<string>(player.stats.dests);
        //    foreach (string unlock in BaseSaver.GetBoard().unlocks)
        //    {
        //        if (!dests.Contains(unlock))
        //        {
        //            dests.Add(unlock);
        //            unlkChar = true;
        //        }
        //    }
        //    player.stats.dests = dests.ToArray();
        //    player.characters = pChars.ToArray();
        //    BaseSaver.PutPlayer(player);
        //    txt = "Victory";
        //}
        //BoardProxy.instance.gameOverPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = txt;
        StartCoroutine(TimeEndGame(won));
    }

    IEnumerator TimeEndGame(bool won){
        if (!AnimationInteractionController.AllAnimationsFinished()) { 
            yield return new WaitForSeconds(AnimationInteractionController.AFTER_KILL);
        }
        BoardProxy.instance.gameOverPanel.SetActive(true);
        MusicTransitionToMap();
        this.won = won;
        string txt = "Defeat";
        if (won){
            List<UnitProxy> units = BoardProxy.instance.GetUnits().Where(unit => unit.GetData().GetTeam() == BoardProxy.PLAYER_TEAM 
              && unit.GetData().GetCurrHealth() > 0 && !unit.GetData().GetSummoned()).ToList();
            PlayerMeta player = BaseSaver.GetPlayer();
            List<Unit> pChars = new List<Unit>(player.characters);
            pChars.Reverse();
            //Remove the top three chars from the roster
            pChars.RemoveAt(0);
            if (pChars.Count > 0) {
                pChars.RemoveAt(0);
            }
            if (pChars.Count > 0) {
                pChars.RemoveAt(0);
            }
            Debug.Log("pChars Before");
            foreach(Unit unt in pChars) {
                Debug.Log(unt.ToString());
            }
            //Fill them back in if they are still on the board
            List<Unit> pCharsBoard = new List<Unit>(new HashSet<Unit>(units.Select(unit => new Unit(unit.GetData()))));
            foreach(Unit unt in pCharsBoard) {
                Debug.Log(unt.ToString());
                if (!pChars.Where(pCh => pCh.characterMoniker.Equals(unt.characterMoniker)).Any()) {
                    pChars.Add(unt);
                }
            }
            Debug.Log("pChars After");
            foreach(Unit unt in pChars) {
                Debug.Log(unt.ToString());
            }
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
            BaseSaver.PutPlayer(player);
            txt = "Victory";
        }
        BoardProxy.instance.gameOverPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = txt;
    }

    public void GameOverNavController()
    {
        if (won)
        {
            string nxtScene = "MapScene";
            PlayerMeta player = BaseSaver.GetPlayer();
            if (unlkChar && player.characters.Length < GameMeta.MAX_ROSTER)
            {
                nxtScene = "CharSelectScreen";
            }
            SceneManager.LoadScene(nxtScene);
            MusicTransitionToMap();
        }
        else
        {
            SceneManager.LoadScene("DeathScene");
            MusicTransitionToMap();
        }
    }

    #region Music Transition

    private void MusicTransitionToMap()
    {
        if (AudioManager.instance != null) {
            AudioManager.instance.SetParameterInt(AudioManager.instance.music, FMODPaths.TransitionParameter, 0);
        }
    }

    #endregion


}
