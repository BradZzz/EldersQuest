using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnController : MonoBehaviour
{
    /*
      TODO:
      Start at team zero
        (In unit settings, decrement movement/attack on use)
      When turn is clicked switch sides
      If the ai flag is active, start ai movement
      at end of ai movement, end turn
    */

    [HideInInspector]
    public int currentTeam;

    public static TurnController instance;

    private void Awake()
    {
        instance = this;
        currentTeam = 0;
    }

    int GetTeam()
    {
        return currentTeam;
    }

    void SwitchTeams()
    {
        currentTeam = GetTeam() == 1 ? 0 : 1;
    }

    public void StartTurn()
    {
        //Turn off all the units not on the team.
        //Turn on all the units with the current team.
        foreach(UnitProxy unit in BoardProxy.instance.GetUnits())
        {
            if (unit.GetData().GetTeam() == currentTeam)
            {
                unit.GetData().GetTurnActions().BeginTurn();
            }
            else
            {
                unit.GetData().GetTurnActions().EndTurn();
            }
        }
        //Set the turn panel to the current turn
        PanelController.instance.SetTurnPanel(currentTeam.ToString());
    }
  
    public void EndTurn()
    {
        Debug.Log("EndTurn");
        //Play the turn cutscene

        //Switch controller teams
        SwitchTeams();

        StartTurn();

        //Run AI (if applicable)
    }
}
