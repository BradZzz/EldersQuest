using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractMode : MonoBehaviour
{
    public abstract void OnUnitSelected(UnitProxy unit);

    public abstract void OnTileSelected(TileProxy tile);
    public abstract void OnTileHovered(TileProxy tile);
    public abstract void OnTileUnHovered(TileProxy tile);

    public abstract void OnClear(TileProxy tile);

}

