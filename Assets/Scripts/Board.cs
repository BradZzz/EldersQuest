using System.Collections.Generic;
using UnityEngine;

public class Board
{
    public Board(int width, int height)
    {
        tiles = new Tile[width, height];
    }
    public Tile[,] tiles;
}