using System;
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
    public Sprite fireTile;

    public GameObject gameOverPanel;

    public static int PLAYER_TEAM = 0;
    public static int ENEMY_TEAM = 1;

    public static bool HUMAN_PLAYER = false;

    private TileProxy[,] tiles;
    private BoardMeta boardMeta;
    private int width;
    private int height;
    private bool won;

    public Grid grid;

    private void Awake()
    {
        gameOverPanel.SetActive(false);
        boardMeta = BaseSaver.GetBoard();
        width = boardMeta.width;
        height = boardMeta.height;

        tileMap = GetComponentInChildren<Tilemap>();
        instance = this;
        tiles = new TileProxy[width, height];
        grid = GetComponent<Grid>();
    }

    // Build the board here
    void Start()
    {
        BuildTestBoard();
        PopulatePlayer();
        PopulateEnemies();
        PlaceObstacles();
        PanelControllerNew.instance.LoadInitUnits(GetUnits());
        TurnController.instance.StartTurn();
    }

    void PopulatePlayer()
    {
        PlayerMeta player = BaseSaver.GetPlayer();
        Queue<TileProxy> validTls = new Queue<TileProxy>(GetSideTiles(BoardProxy.PLAYER_TEAM));
        List<UnitProxy> units = new List<UnitProxy>();
        Debug.Log("PopulatePlayer: " + validTls.Count.ToString());
        for (int i = 0; i < player.characters.Length && i < height; i++)
        {
            Unit cMeta = new Unit(player.characters[i]);
            UnitProxy goodGuy = Instantiate(glossary.GetComponent<Glossary>().units[PLAYER_TEAM], transform);
            units.Add(goodGuy);
            goodGuy.PutData(cMeta);
            goodGuy.Init();
            TileProxy popTile = validTls.Dequeue();
            popTile.ReceiveGridObjectProxy(goodGuy);
            goodGuy.SnapToCurrentPosition();
            Debug.Log("goodGuy placed at: " + popTile.GetPosition().ToString());
        }
    }
  
    void PopulateEnemies()
    {
        Queue<TileProxy> validTls = new Queue<TileProxy>(GetSideTiles(BoardProxy.ENEMY_TEAM));
        List<UnitProxy> units = new List<UnitProxy>();
        Debug.Log("PopulateEnemies: " + validTls.Count.ToString());
        TileProxy popTile;
        for (int i = 0; i < boardMeta.enemies.Length && i < height; i++)
        {
            //Unit cMeta = new Unit(boardMeta.enemies[i].name + i.ToString(),1);
            UnitProxy badGuy = Instantiate(glossary.GetComponent<Glossary>().units[ENEMY_TEAM], transform);
            units.Add(badGuy);
            badGuy.PutData(new Unit("e" + i.ToString(), "Snoopy Bot" + i.ToString(), 1, ENEMY_TEAM, 3, 1, 3, 3, 1, 1, new string[0]{ }));
            badGuy.Init();
            popTile = validTls.Dequeue();
            popTile.ReceiveGridObjectProxy(badGuy);
            badGuy.SnapToCurrentPosition();
            Debug.Log("badGuy placed at: " + popTile.GetPosition().ToString());
        }
    }

    void PlaceObstacles()
    {
        Queue<TileProxy> validTls = new Queue<TileProxy>(GetSideTiles(-1));
        Debug.Log("PopulateObs: " + validTls.Count.ToString());
        for (int i = 0; i < boardMeta.enemies.Length * 2; i++)
        {
            //Unit cMeta = new Unit(boardMeta.enemies[i].name + i.ToString(),1);
            ObstacleProxy obs = Instantiate(glossary.GetComponent<Glossary>().obstacles[0], transform);
            obs.Init();
            TileProxy popTile = validTls.Dequeue();
            popTile.ReceiveGridObjectProxy(obs);
            obs.SnapToCurrentPosition();
            Debug.Log("Obstacle placed at: " + popTile.GetPosition().ToString());
        }
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
        TurnController.instance.EndTurn();
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
  
    public Dictionary<int,int> CountTeams()
    {
        Dictionary<int,int> countDict = new Dictionary<int, int>();
        countDict[0] = 0;
        countDict[1] = 0;

        foreach (UnitProxy unit in GetUnits().ToList())
        {
            if (unit.GetData().GetCurrHealth() > 0) {
                countDict[unit.GetData().GetTeam()] += 1;
            }
        }

        return countDict;
    }

    private void BuildTestBoard()
    {
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


    TileProxy CreateTile(Tile tile)
    {
        Vector3 position = grid.CellToLocal(new Vector3Int(tile.position.x, tile.position.y, 0));
        TileProxy nTile = Instantiate(prefab, position, Quaternion.identity, tileMap.transform);
        nTile.Init(tile, fireTile);
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
                  return 1;
              }
              //else if (!t2sHasObstacle())
              //{
              //    return allTiles ? 1 : int.MaxValue;
              //}
              //return int.MaxValue;
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

    public List<TileProxy> GetAllVisitableNodes(UnitProxy thingToMove, int rng, bool allTiles = false)
    {
        var startNode = GetTileAtPosition(thingToMove.GetPosition());

        return PathGenerator.FindAllVisitableNodes(startNode,
            rng,
            GetDistanceFunction(thingToMove, allTiles))
            .ToList();
    }

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
        return tiles[pos.x, pos.y];
    }
}
