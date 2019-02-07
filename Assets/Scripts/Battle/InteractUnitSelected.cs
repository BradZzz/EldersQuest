using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractUnitSelected : InteractMode
{
    private List<TileProxy> visitableTiles = new List<TileProxy>();
    private UnitProxy currentUnit;
    public override void OnTileSelected(TileProxy tile)
    {
        if (currentUnit != null && visitableTiles.Contains(tile))
        {
            TileProxy startTile = BoardProxy.instance.GetTileAtPosition(currentUnit.GetPosition());
            if (startTile != tile) {
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

  public override void OnUnitSelected(UnitProxy obj)
    {
        if (currentUnit == null)
        {
            UnHighlightTiles();
            currentUnit = obj;
            visitableTiles = BoardProxy.instance.GetAllVisitableNodes(obj);
            HighlightTiles();
            PanelController.SwitchChar(obj);
        }
        else
        {
            if (obj.GetData().GetTeam() != currentUnit.GetData().GetTeam())
            {
                //RemoveGridObjectProxy
                bool dead = obj.IsAttacked(currentUnit.GetData().GetAttack());
                if (dead)
                {
                    TileProxy startTile = BoardProxy.instance.GetTileAtPosition(obj.GetPosition());
                    startTile.RemoveGridObjectProxy(obj);
                    Destroy(obj.gameObject);
                    StartCoroutine(ResetTiles());
                }
            }
        }
    }

    private void HighlightTiles()
    {
        foreach (var tile in visitableTiles)
        {
            tile.HighlightSelected();
        }
    }

    private void UnHighlightTiles()
    {
        foreach (var tile in visitableTiles)
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
