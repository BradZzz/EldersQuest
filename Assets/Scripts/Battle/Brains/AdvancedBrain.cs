using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AdvancedBrain : MonoBehaviour
{
    public static AdvancedBrain instance;

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
                List<TileProxy> opposingTeamTiles = new List<TileProxy>(attackableTiles.Where(tile => tile.HasUnit() 
                    && opposingUnits.Contains(tile.GetUnit())));
                //Look at all the tiles in range
                List<TileProxy> validTiles = BoardProxy.instance.GetAllVisitableNodes(unit, unit.GetMoveSpeed());
                bool coward = unit.GetData().LowHP() && HasHealthyUnits();
                if (!coward) {
                    //If the ai can still move, but has used their attacks, move them away from the enemy team.
                    //These are usually actions a scout would take, so we are trying to protect them here
                    coward = !unit.GetData().GetTurnActions().CanAttack() && unit.GetData().GetTurnActions().CanMove();
                }
                if (opposingTeamTiles.Count > 0 && unit.GetData().GetTurnActions().CanAttack())
                {
                    //Unit in range. Attack!
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
                //If you need to move towards the enemies or run away, logic through here
                else if ((opposingTeamTiles.Count == 0 || coward) && unit.GetData().GetTurnActions().CanMove())
                {
                    Debug.Log("Unit: " + unit.GetData().characterMoniker + " - Coward: " + coward.ToString());
                    TileProxy start = BoardProxy.instance.GetTileAtPosition(unit.GetPosition());
                    //Find the closest opposing unit
                    UnitProxy nearestUnit = GetClosestUnit(unit, opposingUnits);
                    //Calculate a path from the unit to the closest opposing unit
                    Path<TileProxy> path = BoardProxy.instance.GetPath(start, BoardProxy.instance.GetTileAtPosition(nearestUnit.GetPosition()), unit);
                    if (coward) {
                        Debug.Log("Unit: " + unit.GetData().characterMoniker + " is trying to escape!");
                        TileProxy escapeTile = GetRetreatDest(unit, opposingUnits);
                        if (escapeTile != null) {
                            Debug.Log("Escape tile: " + escapeTile.GetPosition().ToString());
                            //Calculate a path from the unit to the closest opposing unit
                            path = BoardProxy.instance.GetPath(start, escapeTile, unit);
                        }
                    }
                    if (path.Count() > 0 && path.Where(tile => validTiles.Contains(tile) && !tile.HasUnit()).Any()) {
                        //See how many of those tiles are in the tiles we are allowed to move
                        TileProxy dest = path.Where(tile => validTiles.Contains(tile) && !tile.HasUnit()).First();
                        if (unit.GetData().ModerateHP()) {
                            //Avoid bad tiles if we don't have too much hp
                            dest = path.Where(tile => validTiles.Contains(tile) && !tile.HasUnit() && !tile.OnFire() && !tile.Frozen()).First();
                        }

                        //If the unit is not trying to run away from battle
                        if (!coward){
                          /*
                            If the ai is allowed to move to a tile and it has ranged, 
                            you want to land it a bit further away from it's target.
                            If the dest and the original target are the same we need to
                            subtract moves from the path over 2 moves
                          */
                          int atkDiff = unit.GetData().GetAtkRange() - 1;
                          //The distance between the ai's destination and the enemy the ai is planning to attack in the future
                          TileProxy[] rngPth = BoardProxy.instance.GetPath(dest, BoardProxy.instance.GetTileAtPosition(nearestUnit.GetPosition()), unit).ToArray();
                          if (atkDiff > 0 && rngPth.Count() <= atkDiff) {
                              List<TileProxy> listPth = path.ToList();
                              int dstIdx = listPth.IndexOf(dest);
                              if (dstIdx + atkDiff - 1 <= listPth.Count() - 1) {
                                  dest = listPth[dstIdx + atkDiff - 1];
                              }
                          }
                        }
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

    public UnitProxy GetClosestUnit(UnitProxy unit, List<UnitProxy> enemies){
        //TileProxy
        //Path<TileProxy> path = BoardProxy.instance.GetPath(start, BoardProxy.instance.GetTileAtPosition(nearestUnit.GetPosition()), unit);
        TileProxy start = BoardProxy.instance.GetTileAtPosition(unit.GetPosition());
        int min = int.MaxValue;
        UnitProxy closest = null;
        foreach(UnitProxy enemy in enemies){
            TileProxy end = BoardProxy.instance.GetTileAtPosition(enemy.GetPosition());
            Path<TileProxy> path = BoardProxy.instance.GetPath(start, end, unit);
            if (path.Count() < min) {
                min = path.Count();
                closest = enemy;
            }
        }
        return closest;
    }

    /*
        We are trying to turn the computer into the biggest dick possible here. 
        If the computer can kill a unit, they will kill the unit with the highest lvl
        If they cannot, they will attack the unit with the lowest level;
    */
    public UnitProxy GetWeakestUnit(UnitProxy unit, List<UnitProxy> enemies){
        TileProxy start = BoardProxy.instance.GetTileAtPosition(unit.GetPosition());
        int maxLvl = int.MaxValue;

        List<UnitProxy> killableUnits = new List<UnitProxy>();
        UnitProxy weakestUnit = null;
        int atk = unit.GetData().GetAttack();
        foreach(UnitProxy enemy in enemies){
            int currHP = enemy.GetData().GetCurrHealth();
            if (atk >=currHP) {
                killableUnits.Add(enemy);
            }
            if (enemy.GetData().GetLvl() < maxLvl) {
                maxLvl = enemy.GetData().GetLvl();
                weakestUnit = enemy;
            }
        }
        if (killableUnits.Count() > 0) {
          maxLvl = int.MinValue;
          foreach(UnitProxy enemy in killableUnits){
              if (enemy.GetData().GetLvl() > maxLvl) {
                  maxLvl = enemy.GetData().GetLvl();
                  weakestUnit = enemy;
              }
          }
        }
        return weakestUnit;
    }

    /*
        This returns the corner of the board furthest from the most enemies
    */
    public TileProxy GetRetreatDest(UnitProxy unit, List<UnitProxy> enemies){
        TileProxy start = BoardProxy.instance.GetTileAtPosition(unit.GetPosition());
        Vector2Int dims = BoardProxy.instance.GetDimensions();

        Debug.Log("Dims: " + dims);

        List<TileProxy> divines = BoardProxy.instance.GetDivineTiles();

        int maxPths = int.MaxValue;
        TileProxy bestDivine = null;
        foreach(TileProxy divine in divines){
          if (divine != null && !divine.HasObstruction()) {
            int pthScore = 0;
            Path<TileProxy> path = BoardProxy.instance.GetPath(start, divine, unit);
            pthScore += path.Count();

            //rageatk doesnt work
            //attack doesn't happen after a move
            Debug.Log("Pos: " + divine.GetPosition() + " score: " + pthScore.ToString());
            if (pthScore < maxPths) {
                maxPths = pthScore;
                bestDivine = divine;
            }
          }
        }

        if (bestDivine != null) {
            return bestDivine;
        }

        List<TileProxy> corners = new List<TileProxy>(new TileProxy[]{ 
            BoardProxy.instance.GetTileAtPosition(new Vector3Int(0,0,0)),
            BoardProxy.instance.GetTileAtPosition(new Vector3Int(dims[0] - 1,0,0)),
            BoardProxy.instance.GetTileAtPosition(new Vector3Int(0,dims[1] - 1,0)),
            BoardProxy.instance.GetTileAtPosition(new Vector3Int(dims[0] - 1,dims[1] - 1,0))
        });

        //int maxPths = -1;
        TileProxy bestCorner = null;
        foreach(TileProxy corner in corners){
          if (corner != null && !corner.HasObstruction()) {
            int pthScore = 0;
            foreach(UnitProxy enemy in enemies){
                TileProxy end = BoardProxy.instance.GetTileAtPosition(enemy.GetPosition());
                Path<TileProxy> path = BoardProxy.instance.GetPath(corner, end, unit);
                pthScore += path.Count();
            }
            Debug.Log("Pos: " + corner.GetPosition() + " score: " + pthScore.ToString());
            if (pthScore > maxPths) {
                maxPths = pthScore;
                bestCorner = corner;
            }
          }
        }

        return bestCorner;
    }

    public bool HasHealthyUnits(){
        return BoardProxy.instance.GetUnits().Where(unit => unit.GetData().GetTeam() == BoardProxy.ENEMY_TEAM && !unit.GetData().LowHP()).Any();
    }
}
