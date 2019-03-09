using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InteractUnitSelectedEdit : InteractModeEdit
{
    private List<TileEditorProxy> allTiles = new List<TileEditorProxy>();
    private List<TileEditorProxy> visitableTiles = new List<TileEditorProxy>();
    private List<TileEditorProxy> attackableTiles = new List<TileEditorProxy>();

    private UnitProxyEditor currentUnit;
    private UnitProxyEditor toAttack;
    private bool UnitMoving;

    public override void OnTileSelected(TileEditorProxy tile)
    {
        if (!UnitMoving) {
            if (currentUnit != null && visitableTiles.Contains(tile))
            {
                toAttack = null;
                TileEditorProxy startTile = BoardEditProxy.instance.GetTileAtPosition(currentUnit.GetPosition());
                if (startTile != tile) {
                    UnitProxyEditor unit = startTile.GetUnit();
                    if (unit.GetData().GetTurnActions().CanMove())
                    {
                        unit.GetData().GetTurnActions().Move();
                        //PanelControllerNew.SwitchChar(unit);
                        UnitMoving = true;
                        StartCoroutine(currentUnit.CreatePathToTileAndLerpToPosition(tile,
                        () =>
                        {
                            tile.ReceiveGridObjectProxy(currentUnit);
                            startTile.RemoveGridObjectProxy(currentUnit);
                            UnHighlightTiles();
                            InteractivityManagerEditor.instance.EnterDefaultMode();
                            UnitMoving = false;
                        }));
                    }
                    else
                    {
                        Debug.Log("Out of actions. Send signal to player they can't move unit.");
                    }
                }
                else
                {
                    StartCoroutine(currentUnit.CreatePathToTileAndLerpToPosition(tile,
                    () =>
                    {
                        StartCoroutine(ResetTiles());
                    }));
                }
            
        }
        //else if (currentUnit != null)
        //{
        //    //Select all the tiles with opp team in all tiles
        //    //List<TileProxy> visitableTiles = allTiles.Where(tl => tl.GetUnit().GetData().GetTeam() != currentUnit.GetData().GetTeam()).ToList<TileProxy>();
        //    if (!allTiles.Where(tl => tl.HasUnit() && (tl.GetUnit().GetData().GetTeam() != currentUnit.GetData().GetTeam())).ToList<TileEditorProxy>().Contains(tile))
        //    {
        //        //BoardProxy.instance.FlushTiles();
        //        //PanelControllerNew.SwitchChar(null);

        //        StartCoroutine(ResetTiles());
        //    }
        //    ////Player clicked out of unit range, reset tiles/UI so player can click somewhere else instead
        //    //BoardProxy.instance.FlushTiles();
        //    //PanelController.SwitchChar(null);
        //}
     }  
  }

  IEnumerator ResetTiles()
  {
      UnHighlightTiles();
      //PanelControllerNew.SwitchChar(null);
      InteractivityManagerEditor.instance.EnterDefaultMode();
      yield return null;
  }

  //public void ResetAtThis()
  //{
  //    StartCoroutine(ResetTiles());
  //}

    public override void OnUnitSelected(UnitProxyEditor obj)
    {
        if (!UnitMoving) {
            if (currentUnit == null)
            {
                toAttack = null;
                UnHighlightTiles();
                currentUnit = obj;
                //The maximum range in which a player has actions
                allTiles = BoardEditProxy.instance.GetAllVisitableNodes(obj, obj.GetMoveSpeed() > obj.GetAttackRange() ? obj.GetMoveSpeed() : obj.GetAttackRange(), true);
                //The attackable tiles
                attackableTiles = BoardEditProxy.instance.GetAllVisitableNodes(obj, obj.GetAttackRange(), true);
                //The visitable tiles
                visitableTiles = BoardEditProxy.instance.GetAllVisitableNodes(obj, obj.GetMoveSpeed());
                HighlightTiles(obj);
                //PanelControllerNew.SwitchChar(obj);
            }
        }
    }

    private void HighlightTiles(UnitProxyEditor obj)
    {
        foreach (var tile in allTiles)
        {
            tile.HighlightSelectedAdv(obj, visitableTiles, attackableTiles);
        }
    }

    private void UnHighlightTiles()
    {
        foreach (var tile in allTiles)
        {
            tile.UnHighlight();
        }
    }

    public void OnEnable()
    {

    }

    public void OnDisable()
    {
        UnHighlightTiles();
        currentUnit = null;
    }

    public override void OnTileHovered(TileEditorProxy tile)
    {

    }

    public override void OnTileUnHovered(TileEditorProxy tile)
    {

    }



    public void Update()
    {
        if (Input.GetMouseButton(1))//right click to exit
        {
            InteractivityManagerEditor.instance.EnterDefaultMode();
        }
    }

    public override void OnClear(TileEditorProxy tile)
    {
        tile.UnHighlight();
        currentUnit = null;
    }
}
