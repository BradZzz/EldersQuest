using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Awake()
    {
       instance = this;
       board = BaseSaver.GetBoard();
       if (board == null) {
          board = new BoardMeta();
       }
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
                                BoardProxy.instance.EndGame(key == BoardProxy.ENEMY_TEAM);
                            }
                        }
                    }
                    break;
                default: break;
            }
        }
    }
}
