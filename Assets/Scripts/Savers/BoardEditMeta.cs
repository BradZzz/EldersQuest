using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class BoardEditMeta
{
    public int height;
    public int width;

    public Vector3Int[] players, enemies;

    public Vector3Int[] fireTiles, snowTiles, wallTiles, divineTiles;

    public string[] unlocks;

    public BoardEditMeta() { 
        height = 10;
        width = 10;
        players = new Vector3Int[]{ };
        enemies = new Vector3Int[]{ };
        fireTiles = new Vector3Int[]{ };
        snowTiles = new Vector3Int[]{ };
        wallTiles = new Vector3Int[]{ };
        divineTiles = new Vector3Int[]{ };
    }

    public BoardEditMeta(int height, int width, UnitProxyEditor[] players, UnitProxyEditor[] enemies, TileEditorProxy[] fireTiles, 
      TileEditorProxy[] snowTiles, TileEditorProxy[] wallTiles, TileEditorProxy[] divineTiles)
    {
        this.height = height;
        this.width = width;
        this.players = players.Select(chr => chr.GetPosition()).ToArray();
        this.enemies = enemies.Select(chr => chr.GetPosition()).ToArray();
        this.fireTiles = fireTiles.Select(chr => chr.GetPosition()).ToArray();
        this.snowTiles = snowTiles.Select(chr => chr.GetPosition()).ToArray();
        this.wallTiles = wallTiles.Select(chr => chr.GetPosition()).ToArray();
        this.divineTiles = divineTiles.Select(chr => chr.GetPosition()).ToArray();
    }
}
