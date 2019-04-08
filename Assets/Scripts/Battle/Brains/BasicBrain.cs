using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BasicBrain : MonoBehaviour
{
    public static BasicBrain instance;

    private Camera cam;

    void Awake()
    {
        instance = this;
        cam = Camera.main;
    }
  
    public static void StartThinking()
    {
        instance.StartCoroutine(instance.BeginProcess());
    }

    //Vector2 ReturnIntersection(Vector3 left, Vector3 right, Vector3 up, Vector3 down){
    //    float A1 = right.y - left.y;
    //    float B1 = left.x - right.x;
    //    float C1 = A1 + B1;

    //    float A2 = right.y - left.y;
    //    float B2 = left.x - right.x;
    //    float C2 = A2 + B2;

    //    float delta = A1 * B2 - A2 * B1;
        
    //    if ((int) delta == 0) 
    //        return Vector2.zero;
        
    //    float x = (B2 * C1 - B1 * C2) / delta;
    //    float y = (A1 * C2 - A2 * C1) / delta;

    //    return new Vector2(x,y);
    //}

    //public void FocusOnUnit(UnitProxy unt){
    //    cam.orthographicSize = 3;
    //    //BoardProxy.instance.transform.position = new Vector3(-1.5f,-1.5f,0);
    //    BoardProxy.instance.transform.position = new Vector3(0,-1.5f,0);

    //    Vector2Int dims = BoardProxy.instance.GetDimensions();
    //    Vector3 bLeft = BoardProxy.instance.GetTileAtPosition(new Vector3Int(0,dims[1]-1,0)).transform.position;
    //    Vector3 bRight = BoardProxy.instance.GetTileAtPosition(new Vector3Int(dims[0]-1,0,0)).transform.position;
    //    Vector3 bUp = BoardProxy.instance.GetTileAtPosition(new Vector3Int(dims[0]-1,dims[1]-1,0)).transform.position;
    //    Vector3 bDown = BoardProxy.instance.GetTileAtPosition(new Vector3Int(0,0,0)).transform.position;
    //    Vector3 bCenter = ReturnIntersection(bLeft, bRight, bUp, bDown);

    //    Vector3 pos = unt.transform.position;
    //    Vector3 diff = pos - bCenter;

    //    Debug.Log("bCenter pos: " + bCenter.ToString());
    //    Debug.Log("pos pos: " + pos.ToString());
    //    Debug.Log("diff pos: " + diff.ToString());

    //    bCenter.x -= diff.x;
    //    bCenter.y -= diff.y;

    //    BoardProxy.instance.transform.position = bCenter;
    //}
  
    public IEnumerator BeginProcess()
    {
        yield return new WaitForSeconds(.5f); 
        //List<UnitProxy> aiUnits = new List<UnitProxy>(BoardProxy.instance.GetUnits().Where(unit => unit.GetData().GetTeam() == BoardProxy.ENEMY_TEAM));
        List<UnitProxy> opposingUnits = new List<UnitProxy>(BoardProxy.instance.GetUnits().Where(unit => unit.GetData().GetTeam() == BoardProxy.PLAYER_TEAM));

        //Debug.Log("Beginning AI Turn");
        ////Debug.Log("aiUnits: " + aiUnits.Count.ToString());
        //Debug.Log("playerUnits: " + opposingUnits.Count.ToString());

        Queue<UnitProxy> unitQueue = new Queue<UnitProxy>(BoardProxy.instance.GetUnits().Where(unit => unit.GetData().GetTeam() == BoardProxy.ENEMY_TEAM));

        while (unitQueue.Count() > 0)
        {
            UnitProxy unit = unitQueue.Dequeue();
            bool didSomething = true;
            while (didSomething && unit.IsAlive())
            {
                if (!AnimationInteractionController.AllAnimationsFinished()) { 
                    yield return new WaitForSeconds(AnimationInteractionController.ATK_WAIT);
                }
                //Debug.Log("AI Char: " + unit.GetData().characterMoniker + " : " + unit.GetData().GetTurnActions().GetMoves() + 
                  //"/" + unit.GetData().GetTurnActions().GetAttacks());
                didSomething = false;
                //Look at all the visitable tiles
                //List<TileProxy> visitableTiles = BoardProxy.instance.GetAllVisitableNodes(unit, unit.GetMoveSpeed(), true);
                //Look at all the attackable tiles
                List<TileProxy> attackableTiles = BoardProxy.instance.GetAllVisitableNodes(unit, unit.GetAttackRange(), true);
                //Look at all the tiles the opposing team is on from the visitable tiles
                List<TileProxy> opposingTeamTiles = new List<TileProxy>(attackableTiles.Where(tile => tile.HasUnit() && !tile.GetUnit().GetData().IsDead()
                    && opposingUnits.Contains(tile.GetUnit())));
                //Look at all the tiles in range
                List<TileProxy> validTiles = BoardProxy.instance.GetAllVisitableNodes(unit, unit.GetMoveSpeed());
                if (opposingTeamTiles.Count > 0 && unit.GetData().GetTurnActions().CanAttack())
                {
                    //unit.FocusOnUnit();
                    //Unit in range. Attack!
                    //Debug.Log("Trying to Attack");
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
                    bool zap = unit.GetData().GetSkills().Where(skll => skll.Contains("Force") || skll.Contains("Void") || skll.Contains("Warp")).Any();
                    if (zap) {
                        yield return new WaitForSeconds(AnimationInteractionController.AFTER_KILL); 
                    } else {
                        yield return new WaitForSeconds(AnimationInteractionController.ATK_WAIT); 
                    }
                } 
                else if (opposingTeamTiles.Count == 0 && unit.GetData().GetTurnActions().CanMove())
                {
                    //FocusOnUnit(unit);
                    //Debug.Log("Trying to Move");
                    TileProxy start = BoardProxy.instance.GetTileAtPosition(unit.GetPosition());
                    //Calculate a path from the unit to the first opposing unit (should be optimized)
                    Path<TileProxy> path = BoardProxy.instance.GetPathAIConsideration(start, BoardProxy.instance.GetTileAtPosition(opposingUnits[0].GetPosition()), unit);
                    if (path.Count() > 0 && path.Where(tile => validTiles.Contains(tile) && !tile.HasUnit()).Any()) {
                        //See how many of those tiles are in the tiles we are allowed to move
                        TileProxy dest = path.Where(tile => validTiles.Contains(tile) && !tile.HasUnit()).First();
                        //Get the path for highlighting
                        path = BoardProxy.instance.GetPath(start, dest, unit);

                        Debug.Log("Dest: " + dest.GetPosition().ToString() + " Start: " + start.GetPosition().ToString());

                        if (dest != start)
                        {
                            //unit.FocusOnUnit();
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
                            Debug.Log("Dest not equal to start");
                            if (unitQueue.Count() > 0 && !unit.GetData().GetTurnActions().idle)
                            {
                                //If there are more ai units left to move and this ai hasn't been put in the back of the queue yet
                                //Put unit at the end of the queue, wait for other units to move
                                Debug.Log("Rotating AI");
                                unit.GetData().GetTurnActions().idle = true;
                                unitQueue.Enqueue(unit);
                                break;
                            }
                            else
                            {
                                Debug.Log("Removing Action");
                                //The unit has already failed to move twice. Stop trying. Move On.
                                unit.GetData().GetTurnActions().Move();
                                didSomething = true;
                                yield return new WaitForSeconds(.25f);
                            }
                        }
                    }
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
