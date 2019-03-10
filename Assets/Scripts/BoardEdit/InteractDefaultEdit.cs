using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractDefaultEdit : InteractModeEdit
{

    public override void OnTileSelected(TileEditorProxy tile)
    {
        // in default mode, look to select a character if possible. Then an item, etc.
        Debug.Log("OnTileSelected Default");
        //tile.HighlightSelected();
        if (BoardEditorUI.instance.GetPaintTile() != BoardEditorUI.TileEditTypes.player 
            && BoardEditorUI.instance.GetPaintTile() != BoardEditorUI.TileEditTypes.enemy) {
            tile.gameObject.GetComponent<SpriteRenderer>().sprite = BoardEditorUI.instance.GetPaintTileSprite();
            tile.ClearAllEffects();
            switch(BoardEditorUI.instance.GetPaintTile()){
              case BoardEditorUI.TileEditTypes.divine:tile.SetLifeDivine(true);break;
              case BoardEditorUI.TileEditTypes.fire:tile.SetLifeFire(true);break;
              case BoardEditorUI.TileEditTypes.snow:tile.SetLifeSnow(true);break;
              case BoardEditorUI.TileEditTypes.wall:tile.SetLifeWall(true);break;
            }
        } else if (!tile.HasObstacle()) {
            if (tile.HasUnit()) {
                tile.FlushGridObjectProxies();
            } else {
                if (BoardEditorUI.instance.GetPaintTile() == BoardEditorUI.TileEditTypes.player) {
                    tile.CreateUnitOnTile(BoardEditorUI.instance.glossary.GetComponent<Glossary>().playerTile, 0);
                } else {
                    tile.CreateUnitOnTile(BoardEditorUI.instance.glossary.GetComponent<Glossary>().enemyTile, 1);
                }
            }
        }
    }



    public override void OnUnitSelected(UnitProxyEditor unit)
    {
        //if in default mode, if a unit is selected, enter unit mode
        //InteractivityManagerEditor.instance.EnterUnitSelectedMode();
        //InteractivityManagerEditor.instance.OnUnitSelected(unit);//forward the event to the new mode
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
