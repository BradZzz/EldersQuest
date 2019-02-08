using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractUnitSelected : InteractMode
{
    private List<TileProxy> allTiles = new List<TileProxy>();
    private List<TileProxy> visitableTiles = new List<TileProxy>();

    private UnitProxy currentUnit;
    public override void OnTileSelected(TileProxy tile)
    {
        if (currentUnit != null && visitableTiles.Contains(tile))
        {
            TileProxy startTile = BoardProxy.instance.GetTileAtPosition(currentUnit.GetPosition());
            if (startTile != tile) {
                UnitProxy unit = startTile.GetUnit();
                PanelController.SwitchChar(unit);
                if (unit.GetData().GetTurnActions().CanMove())
                {
                    unit.GetData().GetTurnActions().Move();
                    StartCoroutine(currentUnit.CreatePathToTileAndLerpToPosition(tile,
                    () =>
                    {
                        tile.ReceiveGridObjectProxy(currentUnit);
                        startTile.RemoveGridObjectProxy(currentUnit);
                        UnHighlightTiles();
                        InteractivityManager.instance.EnterDefaultMode();
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
    }

  IEnumerator ResetTiles()
  {
      UnHighlightTiles();
      InteractivityManager.instance.EnterDefaultMode();
      yield return null;
  }

  //public void ResetAtThis()
  //{
  //    StartCoroutine(ResetTiles());
  //}

  public override void OnUnitSelected(UnitProxy obj)
    {
        if (currentUnit == null)
        {
            UnHighlightTiles();
            currentUnit = obj;
            allTiles = BoardProxy.instance.GetAllVisitableNodes(obj, true);
            visitableTiles = BoardProxy.instance.GetAllVisitableNodes(obj);
            HighlightTiles(obj);
            PanelController.SwitchChar(obj);
        }
        else
        {
            if (obj.GetData().GetTeam() != currentUnit.GetData().GetTeam() 
              && allTiles.Contains(BoardProxy.instance.GetTileAtPosition(obj.GetPosition()))
              && currentUnit.GetData().GetTurnActions().CanAttack())
            {
                if (obj.IsAttacked(currentUnit))
                {
                    //If the unit has died, remove it from the board and destroy the gameobject
                    BoardProxy.instance.GetTileAtPosition(obj.GetPosition()).RemoveGridObjectProxy(obj);
                    Destroy(obj.gameObject);
                    ConditionTracker.instance.EvaluateGame();
                    //Turn off the tiles
                    StartCoroutine(ResetTiles());
                }
                OnDisable();
            }
        }
    }

    private void HighlightTiles(UnitProxy obj)
    {
        foreach (var tile in allTiles)
        {
            tile.HighlightSelected(obj);
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

    public override void OnTileHovered(TileProxy tile)
    {

    }

    public override void OnTileUnHovered(TileProxy tile)
    {

    }



    public void Update()
    {
        if (Input.GetMouseButton(1))//right click to exit
        {
            InteractivityManager.instance.EnterDefaultMode();
        }
    }
}
