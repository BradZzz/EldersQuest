using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BasicBrain : MonoBehaviour
{
    public static BasicBrain instance;

    void Awake()
    {
        instance = this;
    }
  
    public static void StartThinking()
    {
        instance.StartCoroutine(instance.BeginProcess());
    }
  
    public IEnumerator BeginProcess()
    {
        yield return new WaitForSeconds(.5f); 
        //List<UnitProxy> aiUnits = new List<UnitProxy>(BoardProxy.instance.GetUnits().Where(unit => unit.GetData().GetTeam() == BoardProxy.ENEMY_TEAM));
        List<UnitProxy> opposingUnits = new List<UnitProxy>(BoardProxy.instance.GetUnits().Where(unit => unit.GetData().GetTeam() == BoardProxy.PLAYER_TEAM));

        Debug.Log("Beginning AI Turn");
        //Debug.Log("aiUnits: " + aiUnits.Count.ToString());
        Debug.Log("playerUnits: " + opposingUnits.Count.ToString());

        Queue<UnitProxy> unitQueue = new Queue<UnitProxy>(BoardProxy.instance.GetUnits().Where(unit => unit.GetData().GetTeam() == BoardProxy.ENEMY_TEAM));

        while (unitQueue.Count() > 0)
        {
            UnitProxy unit = unitQueue.Dequeue();
            bool didSomething = true;
            while (didSomething)
            {
                didSomething = false;
                /*
                If an enemy is within range, attack
                If an enemy is not within range, go towards nearest one
                */
                List<TileProxy> visitableTiles = BoardProxy.instance.GetAllVisitableNodes(unit,true);
                List<TileProxy> opposingTeamTiles = new List<TileProxy>(visitableTiles.Where(tile => tile.HasUnit() 
                    && tile.GetUnit().GetData().GetTeam() != BoardProxy.ENEMY_TEAM));
                List<TileProxy> validTiles = BoardProxy.instance.GetAllVisitableNodes(unit);

                Debug.Log("TurnActions: " + unit.GetData().GetTurnActions().mv + ":"+ unit.GetData().GetTurnActions().atk);

                if (opposingTeamTiles.Count == 0 && unit.GetData().GetTurnActions().CanMove())
                {
                    Debug.Log("Trying to Move");
                    TileProxy start = BoardProxy.instance.GetTileAtPosition(unit.GetPosition());
                    Path<TileProxy> path = BoardProxy.instance.GetPath(start, BoardProxy.instance.GetTileAtPosition(opposingUnits[0].GetPosition()), unit);
                    TileProxy dest = path.Where(tile => validTiles.Contains(tile) && !tile.HasUnit()).First();
                    path = BoardProxy.instance.GetPath(start, dest, unit);
                    if (dest != start)
                    {
                        foreach (TileProxy tl in path)
                        {
                            tl.ForceHighlight();
                        }
                        yield return new WaitForSeconds(.25f);
                        foreach (TileProxy tl in path)
                        {
                            tl.UnHighlight();
                        }
                        unit.OnSelected();
                        yield return new WaitForSeconds(.25f);
                        InteractivityManager.instance.OnTileSelected(dest);
                        yield return new WaitForSeconds(1f);
                    }
                    else
                    {
                        if (unitQueue.Count() > 0 && !unit.GetData().GetTurnActions().idle)
                        {
                            //If there are more ai units left to move and this ai hasn't been put in the back of the queue yet
                            //Put unit at the end of the queue, wait for other units to move
                            unit.GetData().GetTurnActions().idle = true;
                            unitQueue.Enqueue(unit);
                            break;
                        }
                        else
                        {
                            //The unit has already failed to move twice. Stop trying. Move On.
                            unit.GetData().GetTurnActions().Move();
                            didSomething = true;
                            yield return new WaitForSeconds(.25f);
                        }
                    }
                }
                else if (opposingTeamTiles.Count > 0 && unit.GetData().GetTurnActions().CanAttack())
                {
                    Debug.Log("Trying to Attack");
                    TileProxy oppTile = BoardProxy.instance.GetTileAtPosition(opposingTeamTiles[0].GetPosition());
                    unit.OnSelected();
                    yield return new WaitForSeconds(.5f);
                    oppTile.ForceHighlight();
                    yield return new WaitForSeconds(.25f);
                    oppTile.UnHighlight();
                    opposingTeamTiles[0].GetUnit().OnSelected();
                    didSomething = true;
                }
                yield return new WaitForSeconds(.25f); 
            }
        }
        //No more actions, end turn 
        TurnController.instance.EndTurn();
        yield return null;
    }
}
