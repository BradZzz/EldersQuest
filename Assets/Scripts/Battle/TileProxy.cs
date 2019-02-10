using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileProxy : MonoBehaviour, IHasNeighbours<TileProxy>, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private Tile tile;

    [SerializeField]
    private Transform anchor;

    private List<GridObjectProxy> objectProxies = new List<GridObjectProxy>();

    public Vector3Int GetPosition()
    {
        return tile.position;
    }

    public IEnumerable<TileProxy> Neighbours
    {
        get
        {
            return BoardProxy.instance.GetNeighborTiles(tile.position);
        }
    }

    void SnapToPosition()
    {
        transform.position = BoardProxy.instance.grid.CellToLocal(tile.position);
    }

    public void Init(Tile t)
    {
        tile = t;
        name = t.position.ToString();//for convenience in the heirarchy
        SnapToPosition();
    }

    public void HighlightSelected(UnitProxy inRangeUnit, bool hovering = false)
    {
        if (inRangeUnit != null) {
            bool unitOnTeam = UnitOnTeam(inRangeUnit.GetData().GetTeam());

            bool oppUnitInRange = HasUnit() && !unitOnTeam && inRangeUnit.GetData().GetTurnActions().CanAttack();
            bool ableToMove = !HasUnit() && inRangeUnit.GetData().GetTurnActions().CanMove();
            bool charSelectWOMoves = HasUnit() && !inRangeUnit.GetData().GetTurnActions().CanMove() && inRangeUnit == GetUnit();

            if (oppUnitInRange)
            {
                this.GetComponent<Renderer>().material.color = Color.blue;
            }
            else if (ableToMove)
            {
                this.GetComponent<Renderer>().material.color = Color.red;
            }
            else if (charSelectWOMoves)
            {
                this.GetComponent<Renderer>().material.color = Color.red;
            }
        }
        else if (hovering || !HasUnit())
        {
            this.GetComponent<Renderer>().material.color = Color.red;
        }
    }

    public void ForceHighlight()
    {
        this.GetComponent<Renderer>().material.color = Color.green;
    }

    public void UnHighlight()
    {
        this.GetComponent<Renderer>().material.color = Color.white;
    }

    public void ReceiveGridObjectProxy(GridObjectProxy proxy)
    {
        if (!objectProxies.Contains(proxy))
        {
            objectProxies.Add(proxy);
            proxy.SetPosition(tile.position);
        }
    }

    public void RemoveGridObjectProxy(GridObjectProxy proxy)
    {
        if (objectProxies.Contains(proxy))
        {
            objectProxies.Remove(proxy);
        }
    }

    public List<GridObjectProxy> GetContents()
    {
        return objectProxies;
    }

    public bool CanReceive(GridObjectProxy obj)
    {
        if (obj is UnitProxy)
        {
            if (HasUnit())//TODO: rework to a better system with layers
                return false;
        }
        return true;//for now
    }

    public UnitProxy GetUnit()
    {
        return (UnitProxy) objectProxies.Where(op => op is UnitProxy).First();
    }

    public bool HasUnit()
    {
        return objectProxies.Where(op => op is UnitProxy).Any();
    }

    public bool UnitOnTeam(int team)
    {
        return objectProxies.Where(op => op is UnitProxy && ((UnitProxy)op).GetData().GetTeam() == team).Any();
    }

    #region events
    public void OnPointerDown(PointerEventData eventData)
    {
        InteractivityManager.instance.OnTileSelected(this);
        foreach (var obj in objectProxies.ToList())
        {
            obj.OnSelected();
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        InteractivityManager.instance.OnTileUnHovered(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        InteractivityManager.instance.OnTileHovered(this);
    }

    //This needs to move the board when the player holds down the click
    //public void OnDrag(PointerEventData eventData)
    //{
    //    iTween.MoveTo(GameObject.Find("Grid"), eventData.pointerPressRaycast.worldPosition, .1f);
        //foreach (UnitProxy unt in BoardProxy.instance.GetUnits())
        //{
        //    unt.SnapToCurrentPosition();
        //}
    //}

    //public static GameObject DraggedInstance = GameObject.Find("Grid");
    
    Vector3 _startPosition;
    Vector3 _offsetToMouse;
    float _zDistanceToCamera;

    public void OnBeginDrag (PointerEventData eventData)
    {
       Debug.Log("OnBeginDrag");
       _startPosition = BoardProxy.instance.grid.transform.position;
       _zDistanceToCamera = Mathf.Abs (_startPosition.z - Camera.main.transform.position.z);
    
       _offsetToMouse = _startPosition - Camera.main.ScreenToWorldPoint (
           new Vector3 (Input.mousePosition.x, Input.mousePosition.y, _zDistanceToCamera)
       );
    }
    
    public void OnDrag (PointerEventData eventData)
    {
       Debug.Log("OnDrag");
       if(Input.touchCount > 1)
           return;
    
       //BoardProxy.instance.grid.
       BoardProxy.instance.grid.transform.position = Camera.main.ScreenToWorldPoint (
           new Vector3 (Input.mousePosition.x, Input.mousePosition.y, _zDistanceToCamera)
           ) + _offsetToMouse;
    }
    
    public void OnEndDrag (PointerEventData eventData)
    {
       Debug.Log("OnEndDrag");
       _offsetToMouse = Vector3.zero;
       //foreach (UnitProxy unt in BoardProxy.instance.GetUnits())
       //{
       //   unt.SnapToCurrentPosition();
       //}
    }
  
    #endregion
}
