using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractDefaultEdit : InteractModeEdit
{

    public override void OnTileSelected(TileEditorProxy tile)
    {
        // in default mode, look to select a character if possible. Then an item, etc.
        //tile.HighlightSelected();
    }



    public override void OnUnitSelected(UnitProxyEditor unit)
    {
        //if in default mode, if a unit is selected, enter unit mode
        InteractivityManagerEditor.instance.EnterUnitSelectedMode();
        InteractivityManagerEditor.instance.OnUnitSelected(unit);//forward the event to the new mode
    }

    public void OnDisable()
    {

    }

    public void OnEnable()
    {

    }

    public override void OnTileHovered(TileEditorProxy tile)
    {
        tile.HighlightSelected();
    }

    public override void OnTileUnHovered(TileEditorProxy tile)
    {
        tile.UnHighlight();
    }

    public override void OnClear(TileEditorProxy tile)
    {
        tile.UnHighlight();
    }
}
