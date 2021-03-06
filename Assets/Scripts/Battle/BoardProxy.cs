﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class BoardProxy : MonoBehaviour
{
    public static BoardProxy instance;
    public GameObject glossary;
    public Tilemap tileMap;

    public TileProxy prefab;

    public GameObject gameOverPanel;

    public static int PLAYER_TEAM = 0;
    public static int ENEMY_TEAM = 1;

    public static bool HUMAN_PLAYER = false;

    public GameObject background1;
    public GameObject background2;
    public GameObject background3;
    public GameObject background4;

    private TileProxy[,] tiles;
    private BoardMeta boardMeta;
    private int width;
    private int height;
    private bool won;

    public Grid grid;

    private void Awake()
    {
        gameOverPanel.SetActive(false);
        PlayerMeta player = BaseSaver.GetPlayer();
        background1.SetActive(false);
        background2.SetActive(false);
        background3.SetActive(false);
        background4.SetActive(false);
        switch(player.world){
            case GameMeta.World.nile:background1.SetActive(true);break;
            case GameMeta.World.mountain:background2.SetActive(true);break;
            case GameMeta.World.pyramid:background3.SetActive(true);break;
            case GameMeta.World.candy:background4.SetActive(true);break;
        }

        boardMeta = BaseSaver.GetBoard();
        width = boardMeta.width;
        height = boardMeta.height;

        tileMap = GetComponentInChildren<Tilemap>();
        instance = this;
        //tiles = new TileProxy[width, height];
        grid = GetComponent<Grid>();
    }

    // Build the board here
    void Start()
    {
        BuildBoardFromFile();
    }

    public void SummonAtPosition(Vector3Int pos, int team, int val){
        UnitProxy unit = glossary.GetComponent<Glossary>().cthulhuWisp;
        StartCoroutine(PopulateSkeleton(pos, unit, team, val));
    }

    IEnumerator PopulateSkeleton(Vector3Int pos, UnitProxy unit, int team, int val){
        yield return new WaitForSeconds(2.2f);
        TileProxy tl = tiles[pos.x, pos.y];
        //if (!tl.HasUnit()) {
        UnitProxy newUnit = Instantiate(unit, transform);
        //newUnit.Init();
        newUnit.PutData(Unit.BuildInitial(Unit.FactionType.Cthulhu, Unit.UnitType.Soldier, team, new CthulhuBaseWisp(), val));
        newUnit.Init();
        newUnit.GetData().SetSummoned(true);
        tl.ReceiveGridObjectProxy(newUnit);
        newUnit.SnapToCurrentPosition();
        //}
    }

    void BuildBoardFromFile(){
        PlayerMeta player = BaseSaver.GetPlayer();
        string world = "0" + (((int)player.world)).ToString();
        int lvl = int.Parse(player.lastDest.Replace("Dest",""));
        string currentMap = world + "_" + (lvl < 10 ? "0" : "") + lvl;
        if (player.world == GameMeta.World.candy) {
            currentMap += "_0" + (((int)player.faction) + 1).ToString();
        }
        Debug.Log("BuildBoardFromFile: " + currentMap);
        BoardEditProxy.GetItemInfo(currentMap, PopulateRetrievedInfo);
    }

    public void PopulateRetrievedInfo(BoardEditMeta bMeta){
        Debug.Log("PopulateRetrievedInfo: " + bMeta);
        if (bMeta != null) {
            Debug.Log("Building Stored Board");
            height = bMeta.height;
            width = bMeta.width;
            BuildTestBoard();

            PopulatePlayer(bMeta.players.Select(pos => instance.GetTileAtPosition(pos)).ToArray());
            PopulateEnemies(bMeta.enemies.Select(pos => instance.GetTileAtPosition(pos)).ToArray());

            foreach(Vector3Int pt in bMeta.fireTiles){
                tiles[pt.x, pt.y].SetLifeFire(true);
            }
            foreach(Vector3Int pt in bMeta.snowTiles){
                tiles[pt.x, pt.y].SetLifeSnow(true);
            }
            foreach(Vector3Int pt in bMeta.wallTiles){
                tiles[pt.x, pt.y].SetLifeWall(true);
            }
            foreach(Vector3Int pt in bMeta.divineTiles){
                tiles[pt.x, pt.y].SetLifeDivine(true);
            }
        } else {
            Debug.Log("Building sample board");
            BuildTestBoard();
            PopulatePlayer(GetSideTiles(PLAYER_TEAM));
            PopulateEnemies(GetSideTiles(ENEMY_TEAM));
            PlaceObstaclesAlt();
        }
        foreach(UnitProxy unit in GetUnits())
        {
            unit.AcceptAction(Skill.Actions.BeginGame,null);
        }
        PanelControllerNew.instance.LoadInitUnits(GetUnits());
        TurnController.instance.StartTurn(true);
    }

    void PopulatePlayer(TileProxy[] sideTiles)
    {
        PlayerMeta player = BaseSaver.GetPlayer();
        //Queue<TileProxy> validTls = new Queue<TileProxy>(GetSideTiles(BoardProxy.PLAYER_TEAM));
        Queue<TileProxy> validTls = new Queue<TileProxy>(sideTiles);
        List<UnitProxy> units = new List<UnitProxy>();
        List<Unit> roster = new List<Unit>(player.characters);
        roster.Reverse();
        //Debug.Log("PopulatePlayer: " + validTls.Count.ToString());
        List<Unit> inactiveUnits = new List<Unit>();
        for (int i = 3; i < roster.Count; i++)
        {
            inactiveUnits.Add(roster[i]);
        }

        for (int i = 0; i < roster.Count && i < 3; i++)
        {
            Unit cMeta = new Unit(roster[i]);
            //UnitProxy goodGuy = Instantiate(glossary.GetComponent<Glossary>().units[PLAYER_TEAM], transform);
            UnitProxy goodGuy = Instantiate(ClassNode.ComputeClassBaseUnit(cMeta.GetFactionType(), 
              cMeta.GetUnitType(), glossary.GetComponent<Glossary>()), transform);
            units.Add(goodGuy);
            cMeta = ClassNode.ApplyClassBonusesBattle(cMeta, inactiveUnits.ToArray());
            goodGuy.PutData(cMeta);
            goodGuy.Init();
            TileProxy popTile = validTls.Dequeue();
            popTile.ReceiveGridObjectProxy(goodGuy);
            goodGuy.SnapToCurrentPosition();
            //Debug.Log("goodGuy placed at: " + popTile.GetPosition().ToString());
        }
    }
  
    void PopulateEnemies(TileProxy[] sideTiles)
    {
        //Queue<TileProxy> validTls = new Queue<TileProxy>(GetSideTiles(BoardProxy.ENEMY_TEAM));
        Queue<TileProxy> validTls = new Queue<TileProxy>(sideTiles);
        List<UnitProxy> units = new List<UnitProxy>();
        //Debug.Log("PopulateEnemies: " + validTls.Count.ToString());
        TileProxy popTile;
        for (int i = 0; i < boardMeta.enemies.Length && i < height; i++)
        {
            //Unit cMeta = new Unit(boardMeta.enemies[i].name + i.ToString(),1);
            //glossary.GetComponent<Glossary>().units[ENEMY_TEAM]
            Unit cMeta = new Unit(boardMeta.enemies[i]);
            UnitProxy badGuy = Instantiate(ClassNode.ComputeClassBaseUnit(cMeta.GetFactionType(), 
              cMeta.GetUnitType(), glossary.GetComponent<Glossary>()), transform);
            units.Add(badGuy);
            badGuy.PutData(cMeta);
            badGuy.Init();
            popTile = validTls.Dequeue();
            popTile.ReceiveGridObjectProxy(badGuy);
            badGuy.SnapToCurrentPosition();
            //Debug.Log("badGuy placed at: " + popTile.GetPosition().ToString());
        }
    }

    //void PlaceObstacles()
    //{
    //    Queue<TileProxy> validTls = new Queue<TileProxy>(GetSideTiles(-1));
    //    Debug.Log("PopulateObs: " + validTls.Count.ToString());
    //    for (int i = 0; i < boardMeta.enemies.Length * 2; i++)
    //    {
    //        //Unit cMeta = new Unit(boardMeta.enemies[i].name + i.ToString(),1);
    //        ObstacleProxy obs = Instantiate(glossary.GetComponent<Glossary>().obstacles[0], transform);
    //        obs.Init();
    //        TileProxy popTile = validTls.Dequeue();
    //        popTile.ReceiveGridObjectProxy(obs);
    //        obs.SnapToCurrentPosition();
    //        Debug.Log("Obstacle placed at: " + popTile.GetPosition().ToString());
    //    }
    //}

    void PlaceObstaclesAlt()
    {
        int obsRand = UnityEngine.Random.Range(0,3);
        for (int y =  0; y < height; y++) {
            for (int x = width/2 - 1; x < width/2 + 2; x++) {
                if (obsRand == 0 ? HasObs1(x,y) : (obsRand == 1 ? HasObs2(x,y) : HasObs3(x,y))) {
                    if (!tiles[x,y].HasUnit()) {
                        //Debug.Log("Obstacle placed at: " + x.ToString() + ":" + y.ToString());
                        int obsIdx = UnityEngine.Random.Range(1,glossary.GetComponent<Glossary>().obstacles.Length);
                        ObstacleProxy obs = Instantiate(glossary.GetComponent<Glossary>().obstacles[obsIdx], transform);
                        obs.Init();
                        tiles[x,y].ReceiveGridObjectProxy(obs);
                        //Make sure the obstacles dont go away
                        tiles[x,y].SetLifeWall(true);
                        obs.SnapToCurrentPosition();
                    }
                }
            }
        }
    }

    //Since we have a set amount of exp, we need to always give it to the player when an enemy dies.
    //This function is called with the environment kills an enemy. Give xp to weakest ally unit
    public void GiveLowestCharLvl(UnitProxy diedUnit){
        foreach(UnitProxy unt in GetUnits()){
            Debug.Log("Looking at: " + unt.GetData().characterMoniker);
            if (unt.GetData().GetTeam() == PLAYER_TEAM) {
                Debug.Log("GiveLowestCharLvl: " + unt.GetData().characterMoniker);
                unt.AddLevel();
                break;
            }
        }
        //UnitProxy unit = GetUnits().Where(unt => unt.GetData().GetTeam() == PLAYER_TEAM).OrderByDescending(unt=>unt.GetData().GetLvl()).First();
        //Debug.Log("GiveLowestCharLvl: " + unit.GetData().characterMoniker);
        //unit.AddLevel();
    } 

    bool HasObs1(int x, int y){
      return y > height * .70 || y < height * .30;
    }

    bool HasObs2(int x, int y){
      return y < height * .70 && y > height * .40;
    }

    bool HasObs3(int x, int y){
      return (y < height * .80 && y > height * .60) || (y < height * .40 && y > height * .20);
    }
  
    TileProxy[] GetSideTiles(int team)
    {
        List<TileProxy> tls = new List<TileProxy>();
        if (team == BoardProxy.PLAYER_TEAM || team == -1)
        {
            for (int y = team == -1 ? 1 : 0; y < height - (team == -1 ? 1 : 0); y++)
            {
                for (int x = team == -1 ? 1 : 0; x < (width / 2) - 1; x++)
                {
                    if (!tiles[x, y].HasObstruction())
                    {
                        tls.Add(tiles[x, y]);
                    }
                }
            }
        }
        else if (team == BoardProxy.ENEMY_TEAM || team == -1)
        {
            for (int y = team == -1 ? 1 : 0; y < height - (team == -1 ? 1 : 0); y++)
            {
                for (int x = (width/2) + 1; x < width - (team == -1 ? 1 : 0); x++)
                {
                    if (!tiles[x, y].HasObstruction())
                    {
                        tls.Add(tiles[x, y]);
                    }
                }
            }
        }
        TileProxy[] retTls = tls.ToArray();
        HelperScripts.Shuffle(retTls);
        Debug.Log("GetSideTiles: " + retTls.Length.ToString());
        return retTls;
    }
  
    public void EndTurn()
    {
        if (HUMAN_PLAYER || (!HUMAN_PLAYER && TurnController.instance.currentTeam == PLAYER_TEAM)) {
            TurnController.instance.EndTurn();
        }
    }

    public void RestartBattle(){
        SceneManager.LoadScene("BattleScene");
    }

    public void Surrender(){
        ConditionTracker.instance.EndGame(false);
    }
  
    public List<UnitProxy> GetUnits()
    {
        List<UnitProxy> units = new List<UnitProxy>();

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (tiles[x, y].HasUnit())
                {
                    units.Add(tiles[x, y].GetUnit());
                }
            }
        }
        return units;
    }

    public List<TileProxy> GetOpenTiles()
    {
        List<TileProxy> open = new List<TileProxy>();

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                bool neighborFlag = tiles[x, y].Neighbours.Where(tl => tl.HasUnit()).Any();
                //If the tile doesn't have anything on it, and it's neightbors don't have units
                if (!tiles[x, y].HasObstruction() && !neighborFlag)
                {
                    open.Add(tiles[x, y]);
                }
            }
        }
        return open;
    }

    public List<TileProxy> GetDivineTiles()
    {
        List<TileProxy> divine = new List<TileProxy>();

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (tiles[x,y].IsDivine())
                {
                    divine.Add(tiles[x, y]);
                }
            }
        }
        return divine;
    }
  
    public Dictionary<int,int> CountTeams()
    {
        Dictionary<int,int> countDict = new Dictionary<int, int>();
        countDict[0] = 0;
        countDict[1] = 0;

        foreach (UnitProxy unit in GetUnits().ToList())
        {
            if (!unit.GetData().IsDead()) {
                countDict[unit.GetData().GetTeam()] += 1;
            }
        }

        Debug.Log("CountTeams: " + countDict[0].ToString() + "-" + countDict[1].ToString());

        return countDict;
    }

    private void BuildTestBoard()
    {
        tiles = new TileProxy[width, height];
        Board test = new Board(width, height);
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Tile tile = new Tile(x, y);
                test.tiles[x, y] = tile;
            }
        }

        BuildBoard(test);
    }

    public void BuildBoard(Board board)
    {
        //int width = board.tiles.GetLength(0);
        //int height = board.tiles.GetLength(1);
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                tiles[x, y] = CreateTile(board.tiles[x, y]);
            } 
        }
        //for (int x = 0; x < width; x++)
        //{
        //    for (int y = 0; y < height; y++)
        //    {
        //        tiles[x, y] = CreateTile(board.tiles[x, y]);
        //    }
        //}
    }


    TileProxy CreateTile(Tile tile)
    {
        Vector3 position = grid.CellToLocal(new Vector3Int(tile.position.x, tile.position.y, 0));
        TileProxy nTile = Instantiate(prefab, position, Quaternion.identity, tileMap.transform);
        Glossary glossy = glossary.GetComponent<Glossary>();
        nTile.Init(tile, glossy.GetGrassTile(BaseSaver.GetPlayer().world), glossy.fireTile, glossy.wallTile, glossy.divineTile, glossy.snowTile);
        return nTile;
    }

    public static Vector3 GetWorldPosition(Vector3Int boardCoords)
    {
        return instance.grid.CellToWorld(boardCoords);
    }

    public static Vector3 GetLocalPosition(Vector3Int boardCoords)
    {
        return instance.grid.CellToLocal(boardCoords);
    }

    /// <summary>
    /// gets the neigbours of a tile, not including diagonals
    /// </summary>
    /// <param name="boardCoords"></param>
    /// <returns></returns>
    public List<TileProxy> GetNeighborTiles(Vector3Int boardCoords)
    {
        List<TileProxy> ret = new List<TileProxy>();
        int countX = tiles.GetLength(0);
        int countY = tiles.GetLength(1);
        int x = boardCoords.x;
        int y = boardCoords.y;
        if (x - 1 >= 0)
            ret.Add(tiles[x - 1, y]);
        if (y - 1 >= 0)
            ret.Add(tiles[x, y - 1]);
        if (x + 1 < countX)
            ret.Add(tiles[x + 1, y]);
        if (y + 1 < countY)
            ret.Add(tiles[x, y + 1]);
        return ret;
    }

    Func<TileProxy, TileProxy, double> GetDistanceFunction(UnitProxy thingToMove, bool allTiles = false)
    {
        return (t1, t2) =>
          {
              if (t2.CanReceive(thingToMove))
              {
                  if (t2.Frozen() && !allTiles) {
                      return 2;
                  }
                  return 1;
              }
              return allTiles ? 1 : int.MaxValue;

          };
    }

    Func<TileProxy, TileProxy, double> GetDistanceFunctionAI(UnitProxy thingToMove, bool allTiles = false)
    {
        return (t1, t2) =>
          {
              if (t2.CanReceive(thingToMove))
              {
                  if (t2.Frozen() && !allTiles) {
                      return 2;
                  }
                  if (t2.OnFire() && !allTiles) {
                      return 3;
                  }
                  return 1;
              }
              return allTiles ? 1 : int.MaxValue;

          };
    }

    Func<TileProxy, double> GetEstimationFunction(TileProxy destination, UnitProxy thingToMove)
    {
        return (t) =>
            {
                Vector3Int t1Pos = t.GetPosition();
                Vector3Int t2Pos = destination.GetPosition();
                int xdiff = Mathf.Abs(t1Pos.x - t2Pos.x);
                int ydiff = Mathf.Abs(t1Pos.y - t2Pos.y);
                return xdiff + ydiff;
            };
    }

    public Path<TileProxy> GetPath(TileProxy from, TileProxy to, UnitProxy thingToMove, bool allTiles = false)
    {
        return PathGenerator.FindPath(from,
            to,
            GetDistanceFunction(thingToMove, allTiles),
            GetEstimationFunction(to, thingToMove));//might not want to use a list
    }

    public Path<TileProxy> GetPathAIConsideration(TileProxy from, TileProxy to, UnitProxy thingToMove, bool allTiles = false)
    {
        return PathGenerator.FindPath(from,
            to,
            GetDistanceFunctionAI(thingToMove, allTiles),
            GetEstimationFunction(to, thingToMove));//might not want to use a list
    }

    public List<TileProxy> GetAllVisitableNodes(UnitProxy thingToMove, int rng, bool allTiles = false)
    {
        var startNode = GetTileAtPosition(thingToMove.GetPosition());

        return PathGenerator.FindAllVisitableNodes(startNode,
            rng,
            GetDistanceFunction(thingToMove, allTiles))
            .ToList();
    }

    /*
        These path functions are only for the AI to avoid fire and snow tiles
    */

    //Func<TileProxy, TileProxy, double> GetAIDistanceFunction(UnitProxy thingToMove)
    //{
    //    return (t1, t2) =>
    //      {
    //          if (t2.CanReceive(thingToMove))
    //          {
    //              return 1;
    //          }
    //          return allTiles ? 1 : int.MaxValue;

    //      };
    //}

    //public Path<TileProxy> GetAIEstimationPath(TileProxy from, TileProxy to, UnitProxy thingToMove)
    //{
    //    return PathGenerator.FindPath(from,
    //        to,
    //        GetDistanceFunction(thingToMove, allTiles),
    //        GetEstimationFunction(to, thingToMove));//might not want to use a list
    //}

    public void FlushTiles()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                InteractivityManager.instance.OnClear(tiles[x, y]);
                tiles[x, y].DecrementTileEffects();
            }
        }
    }
  
    public TileProxy GetTileAtPosition(Vector3Int pos)
    {
        if (pos.x >= 0 && pos.x < width && pos.y >= 0 && pos.y < height){
            return tiles[pos.x, pos.y];
        }
        return null;
    }

    public Vector2Int GetDimensions(){
        return new Vector2Int(width, height);
    }
}
