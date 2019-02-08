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
    public GameObject gameOverPanel;

    public static int PLAYER_TEAM = 0;
    public static int ENEMY_TEAM = 1;

    public static bool HUMAN_PLAYER = false;

    private TileProxy[,] tiles;
    private BoardMeta boardMeta;
    private int width;
    private int height;

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
        PopulateEnemies();

        //Create Player
        UnitProxy player1 = Instantiate(glossary.GetComponent<Glossary>().units[PLAYER_TEAM], transform);
        player1.PutData(new Unit("p1", "Bob Everyman", PLAYER_TEAM, 3, 1, 4));
        player1.Init();
        tiles[0, 0].ReceiveGridObjectProxy(player1);
        player1.SnapToCurrentPosition();

        TurnController.instance.StartTurn();
    }

    void PopulateEnemies()
    {
        for (int i = 0; i < boardMeta.enemies.Length && i < height; i++)
        {
            UnitProxy badGuy = Instantiate(glossary.GetComponent<Glossary>().units[ENEMY_TEAM], transform);
            badGuy.PutData(new Unit("e" + i.ToString(), boardMeta.enemies[i].name, ENEMY_TEAM, 3, 1, 3));
            badGuy.Init();
            tiles[3, 0 + i].ReceiveGridObjectProxy(badGuy);
            badGuy.SnapToCurrentPosition();
        }
    }

    public void EndTurn()
    {
        TurnController.instance.EndTurn();
    }

    //public void SetTurnPanel(string msg)
    //{ 
    //    TurnTeamPnl.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = msg;
    //}
  
    public void EndGame(bool won)
    {
        gameOverPanel.SetActive(true);
        string txt = "Defeat";
        if (won){
            txt = "Victory";
        }
        gameOverPanel.transform.Find("GameOverHeader").GetComponent<TextMeshProUGUI>().text = txt;
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
            countDict[unit.GetData().GetTeam()] += 1;
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
        nTile.Init(tile);
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
                  return 1;
              else
              {
                  return allTiles ? 1 : int.MaxValue;
              }

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

    public List<TileProxy> GetAllVisitableNodes(UnitProxy thingToMove, bool allTiles = false)
    {
        var startNode = GetTileAtPosition(thingToMove.GetPosition());

        return PathGenerator.FindAllVisitableNodes(startNode,
            thingToMove.GetMoveSpeed(),
            GetDistanceFunction(thingToMove, allTiles))
            .ToList();
    }

    public void FlushTiles()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                tiles[x, y].UnHighlight();
            }
        }
    }
  
    public TileProxy GetTileAtPosition(Vector3Int pos)
    {
        return tiles[pos.x, pos.y];
    }
}
