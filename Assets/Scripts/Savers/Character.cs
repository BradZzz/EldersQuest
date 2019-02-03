using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
  public string characterName;

  /* Sprites */
  public Sprite north;
  public Sprite south;
  public Sprite east;
  public Sprite west;
  public Sprite northEast;
  public Sprite northWest;
  public Sprite southEast;
  public Sprite southWest;

  public Sprite GetDirectionalSprite(dirs dir)
  {
    switch (dir)
    {
      case dirs.N: return north;
      case dirs.S: return south;
      case dirs.E: return east;
      case dirs.W: return west;
      case dirs.NE: return northEast;
      case dirs.NW: return northWest;
      case dirs.SE: return southEast;
      case dirs.SW: return southWest;
    }
    return southWest;
  }

  /* Scale */
  public float scale;
  public enum dirs
  {
    N,S,E,W,NE,NW,SE,SW,None
  };
}
