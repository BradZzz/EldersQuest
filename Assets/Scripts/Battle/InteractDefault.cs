using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractDefault : InteractMode
{

    public override void OnTileSelected(TileProxy tile)
    {
        // in default mode, look to select a character if possible. Then an item, etc.
        //tile.HighlightSelected();
    }



    public override void OnUnitSelected(UnitProxy unit)
    {
        //if in default mode, if a unit is selected, enter unit mode
        InteractivityManager.instance.EnterUnitSelectedMode();
        InteractivityManager.instance.OnUnitSelected(unit);//forward the event to the new mode
    }

    public void OnDisable()
    {

    }

    public void OnEnable()
    {

    }

    public override void OnTileHovered(TileProxy tile)
    {
        tile.HighlightSelected(null, true);
    }

    public override void OnTileUnHovered(TileProxy tile)
    {
        tile.UnHighlight();
    }

    public override void OnClear(TileProxy tile)
    {
        tile.UnHighlight();
    }
}
