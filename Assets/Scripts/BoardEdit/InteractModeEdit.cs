using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractModeEdit : MonoBehaviour
{
    public abstract void OnUnitSelected(UnitProxyEditor unit);

    public abstract void OnTileSelected(TileEditorProxy tile);
    public abstract void OnTileHovered(TileEditorProxy tile);
    public abstract void OnTileUnHovered(TileEditorProxy tile);

    public abstract void OnClear(TileEditorProxy tile);

}

