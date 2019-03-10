using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BoardEditProxy : MonoBehaviour
{
    public static BoardEditProxy instance;
    public GameObject glossary;
    public Tilemap tileMap;

    public TileEditorProxy prefab;

    public static int PLAYER_TEAM = 0;
    public static int ENEMY_TEAM = 1;

    public static bool HUMAN_PLAYER = false;

    private TileEditorProxy[,] tiles;
    private BoardMeta boardMeta;
    private int width;
    private int height;
    private bool won;

    public Grid grid;

    private void Awake()
    {
        boardMeta = BaseSaver.GetBoard();
        width = boardMeta.width;
        height = boardMeta.height;

        tileMap = GetComponentInChildren<Tilemap>();
        instance = this;
        //tiles = new TileEditorProxy[width, height];
        grid = GetComponent<Grid>();
    }

    // Build the board here
    void Start()
    {
        BuildTestBoard();
        //PopulatePlayer();
        //PopulateEnemies();
        //PlaceObstaclesAlt();
        //foreach(UnitProxy unit in GetUnits())
        //{
        //    unit.AcceptAction(Skill.Actions.BeginGame,null);
        //}
        //PanelControllerNew.instance.LoadInitUnits(GetUnits());
        //TurnController.instance.StartTurn(true);
    }

    public void Resize(int width, int height){
        DestroyTiles();
        this.width = width;
        this.height = height;
        BuildTestBoard();
    }

    //public void SummonAtPosition(Vector3Int pos, int team, int val){
    //    UnitProxyEditor unit = glossary.GetComponent<Glossary>().summonedSkeleton;
    //    PopulateAtPos(pos, unit, team, val);
    //}

    public void PopulateAtPos(Vector3Int pos, UnitProxyEditor unit, int team, int val){
        TileEditorProxy tl = tiles[pos.x, pos.y];
        //if (!tl.HasUnit()) {
        UnitProxyEditor newUnit = Instantiate(unit, transform);
        newUnit.Init();
        newUnit.PutData(newUnit.GetData().SummonedData(team, val));
        newUnit.GetData().SetSummoned(true);
        tl.ReceiveGridObjectProxy(newUnit);
        newUnit.SnapToCurrentPosition();
        //}
    }

    //void PopulatePlayer()
    //{
    //    PlayerMeta player = BaseSaver.GetPlayer();
    //    Queue<TileEditorProxy> validTls = new Queue<TileEditorProxy>(GetSideTiles(BoardProxy.PLAYER_TEAM));
    //    List<UnitProxy> units = new List<UnitProxy>();
    //    List<Unit> roster = new List<Unit>(player.characters);
    //    roster.Reverse();
    //    //Debug.Log("PopulatePlayer: " + validTls.Count.ToString());
    //    for (int i = 0; i < roster.Count && i < height && i < 3; i++)
    //    {
    //        Unit cMeta = new Unit(roster[i]);
    //        UnitProxy goodGuy = Instantiate(glossary.GetComponent<Glossary>().units[PLAYER_TEAM], transform);
    //        units.Add(goodGuy);
    //        goodGuy.PutData(cMeta);
    //        goodGuy.Init();
    //        TileProxy popTile = validTls.Dequeue();
    //        popTile.ReceiveGridObjectProxy(goodGuy);
    //        goodGuy.SnapToCurrentPosition();
    //        //Debug.Log("goodGuy placed at: " + popTile.GetPosition().ToString());
    //    }
    //}
  
    //void PopulateEnemies()
    //{
    //    Queue<TileProxy> validTls = new Queue<TileProxy>(GetSideTiles(BoardProxy.ENEMY_TEAM));
    //    List<UnitProxy> units = new List<UnitProxy>();
    //    //Debug.Log("PopulateEnemies: " + validTls.Count.ToString());
    //    TileProxy popTile;
    //    for (int i = 0; i < boardMeta.enemies.Length && i < height; i++)
    //    {
    //        //Unit cMeta = new Unit(boardMeta.enemies[i].name + i.ToString(),1);
    //        UnitProxy badGuy = Instantiate(glossary.GetComponent<Glossary>().units[ENEMY_TEAM], transform);
    //        units.Add(badGuy);
    //        badGuy.PutData(boardMeta.enemies[i]);
    //        badGuy.Init();
    //        popTile = validTls.Dequeue();
    //        popTile.ReceiveGridObjectProxy(badGuy);
    //        badGuy.SnapToCurrentPosition();
    //        //Debug.Log("badGuy placed at: " + popTile.GetPosition().ToString());
    //    }
    //}

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

    //void PlaceObstaclesAlt()
    //{
    //    int obsRand = UnityEngine.Random.Range(0,3);
    //    for (int y =  0; y < height; y++) {
    //        for (int x = width/2 - 1; x < width/2 + 2; x++) {
    //            if (obsRand == 0 ? HasObs1(x,y) : (obsRand == 1 ? HasObs2(x,y) : HasObs3(x,y))) {
    //                if (!tiles[x,y].HasUnit()) {
    //                    //Debug.Log("Obstacle placed at: " + x.ToString() + ":" + y.ToString());
    //                    int obsIdx = UnityEngine.Random.Range(1,glossary.GetComponent<Glossary>().obstacles.Length);
    //                    ObstacleProxy obs = Instantiate(glossary.GetComponent<Glossary>().obstacles[obsIdx], transform);
    //                    obs.Init();
    //                    tiles[x,y].ReceiveGridObjectProxy(obs);
    //                    //Make sure the obstacles dont go away
    //                    tiles[x,y].SetLifeWall(true);
    //                    obs.SnapToCurrentPosition();
    //                }
    //            }
    //        }
    //    }
    //}

    //Since we have a set amount of exp, we need to always give it to the player when an enemy dies.
    //This function is called with the environment kills an enemy. Give xp to weakest ally unit
    //public void GiveLowestCharLvl(UnitProxyEditor diedUnit){
    //    foreach(UnitProxyEditor unt in GetUnits()){
    //        Debug.Log("Looking at: " + unt.GetData().characterMoniker);
    //        if (unt.GetData().GetTeam() == PLAYER_TEAM) {
    //            Debug.Log("GiveLowestCharLvl: " + unt.GetData().characterMoniker);
    //            unt.AddLevel();
    //            break;
    //        }
    //    }
    //    //UnitProxy unit = GetUnits().Where(unt => unt.GetData().GetTeam() == PLAYER_TEAM).OrderByDescending(unt=>unt.GetData().GetLvl()).First();
    //    //Debug.Log("GiveLowestCharLvl: " + unit.GetData().characterMoniker);
    //    //unit.AddLevel();
    //} 

    bool HasObs1(int x, int y){
      return y > height * .70 || y < height * .30;
    }

    bool HasObs2(int x, int y){
      return y < height * .70 && y > height * .40;
    }

    bool HasObs3(int x, int y){
      return (y < height * .80 && y > height * .60) || (y < height * .40 && y > height * .20);
    }
  
    TileEditorProxy[] GetSideTiles(int team)
    {
        List<TileEditorProxy> tls = new List<TileEditorProxy>();
        if (team == BoardEditProxy.PLAYER_TEAM || team == -1)
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
        else if (team == BoardEditProxy.ENEMY_TEAM || team == -1)
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
        TileEditorProxy[] retTls = tls.ToArray();
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
  
    public List<UnitProxyEditor> GetUnits()
    {
        List<UnitProxyEditor> units = new List<UnitProxyEditor>();

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

    public List<TileEditorProxy> GetOpenTiles()
    {
        List<TileEditorProxy> open = new List<TileEditorProxy>();

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

    public List<TileEditorProxy> GetSpecialTiles(BoardEditorUI.TileEditTypes tileType)
    {
        List<TileEditorProxy> units = new List<TileEditorProxy>();

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                switch(tileType){
                  case BoardEditorUI.TileEditTypes.fire: 
                    if (tiles[x, y].OnFire()){
                        units.Add(tiles[x, y]);
                    }
                    break;
                  case BoardEditorUI.TileEditTypes.snow: 
                    if (tiles[x, y].Frozen()){
                        units.Add(tiles[x, y]);
                    }
                    break;
                  case BoardEditorUI.TileEditTypes.divine: 
                    if (tiles[x, y].IsDivine()){
                        units.Add(tiles[x, y]);
                    }
                    break;
                  case BoardEditorUI.TileEditTypes.wall: 
                    if (tiles[x, y].IsWall()){
                        units.Add(tiles[x, y]);
                    }
                    break;
                  case BoardEditorUI.TileEditTypes.grass: 
                    if (!tiles[x, y].OnFire() && !tiles[x, y].Frozen() && !tiles[x, y].IsDivine() && !tiles[x, y].IsWall()){
                        units.Add(tiles[x, y]);
                    }
                    break;
                }
            }
        }
        return units;
    }
  
    //public Dictionary<int,int> CountTeams()
    //{
    //    Dictionary<int,int> countDict = new Dictionary<int, int>();
    //    countDict[0] = 0;
    //    countDict[1] = 0;

    //    foreach (UnitProxy unit in GetUnits().ToList())
    //    {
    //        if (unit.GetData().GetCurrHealth() > 0) {
    //            countDict[unit.GetData().GetTeam()] += 1;
    //        }
    //    }

    //    return countDict;
    //}

    private void BuildTestBoard()
    {
        Debug.Log("BuildTestBoard");
        //DestroyTiles();
        tiles = new TileEditorProxy[width, height];
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
        int width = board.tiles.GetLength(0);
        int height = board.tiles.GetLength(1);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                tiles[x, y] = CreateTile(board.tiles[x, y]);
            }
        }
    }


    TileEditorProxy CreateTile(Tile tile)
    {
        Vector3 position = grid.CellToLocal(new Vector3Int(tile.position.x, tile.position.y, 0));
        TileEditorProxy nTile = Instantiate(prefab, position, Quaternion.identity, tileMap.transform);
        Glossary glossy = glossary.GetComponent<Glossary>();
        nTile.Init(tile, glossy.fireTile, glossy.wallTile, glossy.divineTile, glossy.snowTile);
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
    public List<TileEditorProxy> GetNeighborTiles(Vector3Int boardCoords)
    {
        List<TileEditorProxy> ret = new List<TileEditorProxy>();
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

    Func<TileEditorProxy, TileEditorProxy, double> GetDistanceFunction(UnitProxyEditor thingToMove, bool allTiles = false)
    {
        return (t1, t2) =>
          {
              if (t2.CanReceive(thingToMove))
              {
                  return 1;
              }
              //else if (!t2.HasObstacle())
              //{
              //    return allTiles ? 1 : int.MaxValue;
              //}
              //return int.MaxValue;
              return allTiles ? 1 : int.MaxValue;

          };
    }

    Func<TileEditorProxy, double> GetEstimationFunction(TileEditorProxy destination, UnitProxyEditor thingToMove)
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
    public Path<TileEditorProxy> GetPath(TileEditorProxy from, TileEditorProxy to, UnitProxyEditor thingToMove, bool allTiles = false)
    {
        return PathGenerator.FindPath(from,
            to,
            GetDistanceFunction(thingToMove, allTiles),
            GetEstimationFunction(to, thingToMove));//might not want to use a list
    }

    public List<TileEditorProxy> GetAllVisitableNodes(UnitProxyEditor thingToMove, int rng, bool allTiles = false)
    {
        var startNode = GetTileAtPosition(thingToMove.GetPosition());

        return PathGenerator.FindAllVisitableNodes(startNode,
            rng,
            GetDistanceFunction(thingToMove, allTiles))
            .ToList();
    }

    //public void FlushTiles()
    //{
    //    for (int x = 0; x < width; x++)
    //    {
    //        for (int y = 0; y < height; y++)
    //        {
    //            InteractivityManager.instance.OnClear(tiles[x, y]);
    //            tiles[x, y].DecrementTileEffects();
    //        }
    //    }
    //}

    public void DestroyTiles()
    {
        if (tiles == null) {
            return;
        }
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (tiles[x, y] != null) {
                    Destroy(tiles[x, y].gameObject);
                }
            }
        }
    }
  
    public TileEditorProxy GetTileAtPosition(Vector3Int pos)
    {
        if (pos.x >= 0 && pos.x < width && pos.y >= 0 && pos.y < height){
            return tiles[pos.x, pos.y];
        }
        return null;
    }

    public static void SaveItemInfo(string fileName, string saveStr){
       string path = null;
       path = "Assets/Maps/" + fileName + ".json";
       //string str = saveStr;
       using (FileStream fs = new FileStream(path, FileMode.Create)){
           using (StreamWriter writer = new StreamWriter(fs)){
               writer.Write(saveStr);
           }
       }
       UnityEditor.AssetDatabase.Refresh ();
    }
}
