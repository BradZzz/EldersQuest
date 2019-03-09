using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractivityManagerEditor : MonoBehaviour
{
    public static InteractivityManagerEditor instance;
    public InteractDefaultEdit defaultMode;
    public InteractUnitSelectedEdit unitSelectedMode;
    private InteractModeEdit _currentMode;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        _currentMode = defaultMode;
    }

    public void OnClear(TileEditorProxy tile)
    {
        _currentMode.OnClear(tile);
    }
  
    public void OnUnitSelected(UnitProxyEditor unit)
    {
        _currentMode.OnUnitSelected(unit);
    }

    public void OnTileSelected(TileEditorProxy tile)
    {
        _currentMode.OnTileSelected(tile);
    }

    public void OnTileHovered(TileEditorProxy tile)
    {
        _currentMode.OnTileHovered(tile);
    }
    public void OnTileUnHovered(TileEditorProxy tile)
    {
        _currentMode.OnTileUnHovered(tile);
    }

    private void ChangeMode(InteractModeEdit mode)
    {
        _currentMode.enabled = (false);
        _currentMode = mode;
        _currentMode.enabled = true;
    }

    public void EnterUnitSelectedMode()
    {
        ChangeMode(unitSelectedMode);
    }

    public void EnterDefaultMode()
    {
        ChangeMode(defaultMode);
    }
}
