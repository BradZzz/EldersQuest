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
                if (!AnimationInteractionController.AllAnimationsFinished()) { 
                    yield return new WaitForSeconds(AnimationInteractionController.ATK_WAIT);
                }
                Debug.Log("AI Char: " + unit.GetData().characterMoniker + " : " + unit.GetData().GetTurnActions().GetMoves() + 
                  "/" + unit.GetData().GetTurnActions().GetAttacks());
                didSomething = false;
                //Look at all the visitable tiles
                //List<TileProxy> visitableTiles = BoardProxy.instance.GetAllVisitableNodes(unit, unit.GetMoveSpeed(), true);
                //Look at all the attackable tiles
                List<TileProxy> attackableTiles = BoardProxy.instance.GetAllVisitableNodes(unit, unit.GetAttackRange(), true);
                //Look at all the tiles the opposing team is on from the visitable tiles
                List<TileProxy> opposingTeamTiles = new List<TileProxy>(attackableTiles.Where(tile => tile.HasUnit() 
                    && tile.GetUnit().GetData().GetTeam() != BoardProxy.ENEMY_TEAM));
                //Look at all the tiles in range
                List<TileProxy> validTiles = BoardProxy.instance.GetAllVisitableNodes(unit, unit.GetMoveSpeed());
                if (opposingTeamTiles.Count == 0 && unit.GetData().GetTurnActions().CanMove())
                {
                    Debug.Log("Trying to Move");
                    TileProxy start = BoardProxy.instance.GetTileAtPosition(unit.GetPosition());
                    //Calculate a path from the unit to the first opposing unit (should be optimized)
                    Path<TileProxy> path = BoardProxy.instance.GetPath(start, BoardProxy.instance.GetTileAtPosition(opposingUnits[0].GetPosition()), unit);
                    //See how many of those tiles are in the tiles we are allowed to move
                    TileProxy dest = path.Where(tile => validTiles.Contains(tile) && !tile.HasUnit()).First();
                    //Get the path for highlighting
                    path = BoardProxy.instance.GetPath(start, dest, unit);
                    if (dest != start)
                    {
                        didSomething = true;
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
                } else if (opposingTeamTiles.Count > 0 && unit.GetData().GetTurnActions().CanAttack())
                {
                    //Unit in range. Attack!
                    Debug.Log("Trying to Attack");
                    TileProxy oppTile = BoardProxy.instance.GetTileAtPosition(opposingTeamTiles[0].GetPosition());
                    unit.OnSelected();
                    yield return new WaitForSeconds(.1f);
                    oppTile.ForceHighlight();
                    yield return new WaitForSeconds(.3f);
                    oppTile.UnHighlight();
                    opposingTeamTiles[0].GetUnit().OnSelected();
                    yield return new WaitForSeconds(.5f);
                    opposingTeamTiles[0].GetUnit().OnSelected();
                    didSomething = true;
                }
                yield return new WaitForSeconds(.25f); 
            }
        }
        //yield return new WaitForSeconds(AnimationInteractionController.NO_ATK_WAIT); 
        //No more actions, end turn 
        TurnController.instance.EndTurn();
        yield return null;
    }
}
