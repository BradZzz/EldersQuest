using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
        TileProxy tle = BoardProxy.instance.GetTileAtPosition(unit.GetPosition());
        tle.CreateAnimation(Glossary.fx.bloodExplosions);
        //tle.FloatUp(Skill.Actions.None,"Death", Color.red,"Character died");
        AnimationInteractionController.InteractionAnimationGameobject(
          BoardProxy.instance.glossary.GetComponent<Glossary>().skull, tle.gameObject, AnimationInteractionController.NO_WAIT, true);

        tle.RemoveGridObjectProxy(unit);
        Destroy(unit.gameObject);
        EvaluateGame();
    }

    public void EndGame(bool won)
    {
        Debug.Log("EndGame: " + won.ToString());
        StartCoroutine(TimeEndGame(won));
    }

    IEnumerator TimeEndGame(bool won){
        if (!AnimationInteractionController.AllAnimationsFinished()) { 
            yield return new WaitForSeconds(AnimationInteractionController.AFTER_KILL);
        }

        BoardProxy.instance.gameOverPanel.SetActive(true);
        BoardProxy.instance.gameOverPanel.transform.GetChild(2).gameObject.SetActive(false);
        BoardProxy.instance.gameOverPanel.transform.GetChild(3).gameObject.SetActive(false);

        /*
          Replace shit here
        */

        PlayerMeta player = BaseSaver.GetPlayer();
        BoardMeta brd = BaseSaver.GetBoard();
        Glossary glossy = BoardProxy.instance.glossary.GetComponent<Glossary>();

        Unit.FactionType fact = won ? player.faction : brd.enemies[0].GetFactionType();
        switch(fact){
          case Unit.FactionType.Cthulhu:
            BoardProxy.instance.gameOverPanel.transform.GetChild(0).GetComponent<Image>().sprite = glossy.endBattleOverlayCthulhuLeft;
            BoardProxy.instance.gameOverPanel.transform.GetChild(1).GetComponent<Image>().sprite = glossy.endBattleOverlayCthulhuRight;
            break;
          case Unit.FactionType.Egypt:
            BoardProxy.instance.gameOverPanel.transform.GetChild(0).GetComponent<Image>().sprite = glossy.endBattleOverlayEgyptLeft;
            BoardProxy.instance.gameOverPanel.transform.GetChild(1).GetComponent<Image>().sprite = glossy.endBattleOverlayEgyptRight;
            break;
          case Unit.FactionType.Human:
            BoardProxy.instance.gameOverPanel.transform.GetChild(0).GetComponent<Image>().sprite = glossy.endBattleOverlayHumanLeft;
            BoardProxy.instance.gameOverPanel.transform.GetChild(1).GetComponent<Image>().sprite = glossy.endBattleOverlayHumanRight;
            break;
          default:
            BoardProxy.instance.gameOverPanel.transform.GetChild(0).GetComponent<Image>().sprite = glossy.endBattleOverlaySusieLeft;
            BoardProxy.instance.gameOverPanel.transform.GetChild(1).GetComponent<Image>().sprite = glossy.endBattleOverlaySusieRight;
            break;
        }

        GameObject screenChild = BoardProxy.instance.gameOverPanel.transform.GetChild(0).gameObject;

        Vector3 moveToPos = screenChild.transform.position;
        float screenHeight = screenChild.GetComponent<RectTransform>().rect.height;

        Vector3 startPosHigh = new Vector3(0,moveToPos.y + screenHeight,moveToPos.z);
        Vector3 startPosLow = new Vector3(0,moveToPos.y - screenHeight,moveToPos.z);

        BoardProxy.instance.gameOverPanel.transform.GetChild(0).localPosition = startPosHigh;
        BoardProxy.instance.gameOverPanel.transform.GetChild(1).localPosition = startPosLow;

        if (!won) {
            BoardProxy.instance.gameOverPanel.transform.GetChild(2).gameObject.SetActive(true);
            BoardProxy.instance.gameOverPanel.transform.GetChild(2).localPosition = startPosHigh;
            iTween.MoveTo(BoardProxy.instance.gameOverPanel.transform.GetChild(2).gameObject, moveToPos, 3f);
        } else {
            BoardProxy.instance.gameOverPanel.transform.GetChild(3).gameObject.SetActive(true);
            switch(fact){
              case Unit.FactionType.Cthulhu:
                BoardProxy.instance.gameOverPanel.transform.GetChild(3).GetChild(0).GetComponent<Image>().sprite = glossy.endBattleWinOverlayCthulhu1;
                BoardProxy.instance.gameOverPanel.transform.GetChild(3).GetChild(1).GetComponent<Image>().sprite = glossy.endBattleWinOverlayCthulhu2;
                break;
              case Unit.FactionType.Egypt:
                BoardProxy.instance.gameOverPanel.transform.GetChild(3).GetChild(0).GetComponent<Image>().sprite = glossy.endBattleWinOverlayEgypt1;
                BoardProxy.instance.gameOverPanel.transform.GetChild(3).GetChild(1).GetComponent<Image>().sprite = glossy.endBattleWinOverlayEgypt2;
                break;
              case Unit.FactionType.Human:
                BoardProxy.instance.gameOverPanel.transform.GetChild(3).GetChild(0).GetComponent<Image>().sprite = glossy.endBattleWinOverlayHuman1;
                BoardProxy.instance.gameOverPanel.transform.GetChild(3).GetChild(1).GetComponent<Image>().sprite = glossy.endBattleWinOverlayHuman2;
                break;
              default:
                BoardProxy.instance.gameOverPanel.transform.GetChild(3).GetChild(0).GetComponent<Image>().sprite = glossy.endBattleWinOverlayCthulhu1;
                BoardProxy.instance.gameOverPanel.transform.GetChild(3).GetChild(1).GetComponent<Image>().sprite = glossy.endBattleWinOverlayCthulhu2;
                break;
            }
            BoardProxy.instance.gameOverPanel.transform.GetChild(3).localPosition = startPosHigh;
            iTween.MoveTo(BoardProxy.instance.gameOverPanel.transform.GetChild(3).gameObject, moveToPos, 3f);
        }

        iTween.MoveTo(BoardProxy.instance.gameOverPanel.transform.GetChild(0).gameObject, moveToPos, 1f);
        iTween.MoveTo(BoardProxy.instance.gameOverPanel.transform.GetChild(1).gameObject, moveToPos, 1f);
        

        MusicTransitionToMap();
        this.won = won;
        string txt = "Defeat";
        if (won){
            List<UnitProxy> units = BoardProxy.instance.GetUnits().Where(unit => unit.GetData().GetTeam() == BoardProxy.PLAYER_TEAM 
              && unit.GetData().GetCurrHealth() > 0 && !unit.GetData().GetSummoned()).ToList();
            //PlayerMeta player = BaseSaver.GetPlayer();
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
        BoardProxy.instance.gameOverPanel.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = txt;
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
