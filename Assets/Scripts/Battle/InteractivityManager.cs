using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractivityManager : MonoBehaviour
{
    public static InteractivityManager instance;
    public InteractDefault defaultMode;
    public InteractUnitSelected unitSelectedMode;
    private InteractMode _currentMode;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        _currentMode = defaultMode;
    }

    public void OnClear(TileProxy tile)
    {
        _currentMode.OnClear(tile);
    }
  
    public void OnUnitSelected(UnitProxy unit)
    {
        _currentMode.OnUnitSelected(unit);
    }

    public void OnTileSelected(TileProxy tile)
    {
        _currentMode.OnTileSelected(tile);
    }

    public void OnTileHovered(TileProxy tile)
    {
        _currentMode.OnTileHovered(tile);
    }
    public void OnTileUnHovered(TileProxy tile)
    {
        _currentMode.OnTileUnHovered(tile);
    }

    private void ChangeMode(InteractMode mode)
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
