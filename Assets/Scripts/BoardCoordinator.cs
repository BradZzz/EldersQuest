using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardCoordinator : MonoBehaviour
{
    /*
     * Turn character based on click
     * 
     * 1) Hold positions in boardcoordinator. When a space is clicked, update the board coordinator and send back the relative position to the last pos
     * 2) Add a* algorithm to game
     * 3) Move character from one place to the other when a space is clicked
     */

  public int width;
  public int height;
  public GameObject tile;
  public Transform[] currentPos;
  public static BoardCoordinator instance;
  public GameObject glossary;

  private TileSelect[,] board;

  private void Awake()
  {
    instance = this;
    currentPos = new Transform[] { null, null };
    board = new TileSelect[width,height];
    width /= 2;
  }

  // Build the board here
  void Start()
  {
    Vector3 start = new Vector3(0.535f, -6.6f, 0);
    Debug.Log("Start");
    Transform parent = GameObject.Find("Tilemap").transform;
    for (int y = 0; y < height; y++)
    {
       for (int x = 0; x < width * 2; x+=2)
       {
        GameObject tl = CreateTile(parent, start, x, y);
        board[x / 2,y] = tl.GetComponent<TileSelect>();
        tl.GetComponent<TileSelect>().Deactivate();
      }
    }
    currentPos[0] = board[0, 0].transform;
    board[0, 0].GetComponent<TileSelect>().Activate();
  }

  public TileSelect GetCurrentTile()
  {
    if (currentPos[0] == null)
    {
      return null;
    }
    Vector2 currentTile = currentPos[0].GetComponent<TileSelect>().pos;
    Debug.Log("currentTile: " + currentTile.ToString());
    return board[(int)currentTile.x, (int)currentTile.y];
  }

  public TileSelect GetLastTile()
  {
    if (currentPos[1] == null)
    {
      return null;
    }
    Vector2 lastTile = currentPos[1].GetComponent<TileSelect>().pos;
    Debug.Log("lastTile: " + lastTile.ToString());
    return board[(int)lastTile.x, (int)lastTile.y];
  }

  GameObject CreateTile(Transform parent, Vector3 pos, int col, int row)
  {
    SpriteRenderer box = tile.GetComponent<SpriteRenderer>();

    float sizeX = box.bounds.size.x;
    float sizeY = box.bounds.size.y;

    sizeX /= 2;
    sizeY *= .26f;


    float xOffset = col * sizeX + sizeX * (row % 2 == 0 ? .5f : -.5f);
    float yOffset = row * sizeY + sizeY * (col % 2 == 0 ? -.5f : .5f);

    GameObject nTile = Instantiate(tile, new Vector3(pos.x + xOffset, pos.y + yOffset, pos.z), Quaternion.identity, parent);
    nTile.GetComponent<TileSelect>().SetPos(new Vector2(col/2, row));
    return nTile;
  }

   public Character.dirs SetCurrPos(Transform pos, bool save)
   {
      Debug.Log("SetCurrPos");
      Debug.Log(pos.GetComponent<TileSelect>().GetResting());
      Debug.Log(currentPos[0].GetComponent<TileSelect>().GetResting());
      Transform prev, curr;

      if (!Mathf.Approximately(pos.GetComponent<TileSelect>().GetResting().x, currentPos[0].GetComponent<TileSelect>().GetResting().x) 
      || !Mathf.Approximately(pos.GetComponent<TileSelect>().GetResting().y, currentPos[0].GetComponent<TileSelect>().GetResting().y))
      {
          prev = currentPos[0];
          curr = pos;

          if (save)
          {
            currentPos[1] = prev;
            currentPos[0] = curr;
            Debug.Log("Changed");
          }
      }
      else
      {
        return Character.dirs.None;
      }

      /*
       * If nothing in the queue
       */
      //Vector2 lastPos = new Vector2(currentPos.x, currentPos.y);
      //currentPos = pos;

      Vector2 diff = new Vector2(curr.gameObject.GetComponent<TileSelect>().GetResting().x - prev.gameObject.GetComponent<TileSelect>().GetResting().x,
        curr.gameObject.GetComponent<TileSelect>().GetResting().y - prev.gameObject.GetComponent<TileSelect>().GetResting().y);
      Character.dirs charDirection = Character.dirs.None;

      //Debug.Log("Clicks After: " + currentPos[0].ToString() + " : " + currentPos[1].ToString() + " : " + pos.ToString());

      Debug.Log("Click Diff: " + diff.ToString());

      if (diff.x < 0)
      {
         /* West */
         if (diff.y < 0)
         {
            /* North */
            charDirection = Character.dirs.SW;
         }
         else if (diff.y > 0)
         {
            /* South */
            charDirection = Character.dirs.NW;
         }
         else
         {
            /* None */
            charDirection = Character.dirs.W;
         }
      }
      else if (diff.x > 0)
      {
         /* East */
         if (diff.y < 0)
         {
            /* North */
            charDirection = Character.dirs.SE;
         }
         else if (diff.y > 0)
         {
            /* South */
            charDirection = Character.dirs.NE;
         }
         else
         {
            /* None */
            charDirection = Character.dirs.E;
         }
      }
      else
      {
         /* None */
         if (diff.y < 0)
         {
            /* South */
            charDirection = Character.dirs.S;
         }
         else if (diff.y > 0)
         {
            /* North */
            charDirection = Character.dirs.N;
         }
      }
      return charDirection;
   }

  public TileSelect[] GetSurroundingTiles(TileSelect selectedTile)
  {
    Vector2 pos = selectedTile.pos;
    List<TileSelect> retTiles = new List<TileSelect>();
    if ((int)pos.y % 2 == 1) {
      if (pos.x > 0 && pos.y > 0)
      {
        retTiles.Add(board[(int)pos.x - 1, (int)pos.y - 1]);
      }
      if (pos.y > 0)
      {
        retTiles.Add(board[(int)pos.x, (int)pos.y - 1]);
      }
      if (pos.x > 0 && pos.y < height)
      {
        retTiles.Add(board[(int)pos.x - 1, (int)pos.y + 1]);
      }
      if (pos.y < height)
      {
        retTiles.Add(board[(int)pos.x, (int)pos.y + 1]);
      }
    }
    else
    {
      if (pos.x < width && pos.y > 0)
      {
        retTiles.Add(board[(int)pos.x + 1, (int)pos.y - 1]);
      }
      if (pos.y > 0)
      {
        retTiles.Add(board[(int)pos.x, (int)pos.y - 1]);
      }
      if (pos.x < width && pos.y < height)
      {
        retTiles.Add(board[(int)pos.x + 1, (int)pos.y + 1]);
      }
      if (pos.y < height)
      {
        retTiles.Add(board[(int)pos.x, (int)pos.y + 1]);
      }
    }

    Debug.Log("Selected pos: " + pos.ToString());
    Debug.Log("Selected dirs: " + retTiles.ToArray());

    return retTiles.ToArray();
  }

}
