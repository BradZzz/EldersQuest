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

    private int fireTrns;
    private Sprite def, fireAlt;

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

    public void Init(Tile t, Sprite fireAlt)
    {
        def = GetComponent<SpriteRenderer>().sprite;
        this.fireAlt = fireAlt;
        tile = t;
        name = t.position.ToString();//for convenience in the heirarchy
        SnapToPosition();
    }

    public void HighlightSelected()
    {
        GetComponent<Renderer>().material.color = Color.red;
    }

    public void HighlightSelectedAdv(UnitProxy inRangeUnit, List<TileProxy> visitable, List<TileProxy> attackable)
    {
        bool canAtk = inRangeUnit.GetData().GetTurnActions().CanAttack();
        bool canMv = inRangeUnit.GetData().GetTurnActions().CanMove();

        if (BoardProxy.instance.GetTileAtPosition(inRangeUnit.GetPosition()) == this)
        {
            if (!canAtk && !canMv)
            {
                GetComponent<Renderer>().material.color = Color.red;
            }
            return;
        }
    
        if (canAtk)
        {
            if (attackable.Contains(this))
            {
                if (HasUnit() && inRangeUnit.GetData().GetTeam() != GetUnit().GetData().GetTeam())
                {
                    GetComponent<Renderer>().material.color = Color.blue;
                }
                else if (!HasObstacle())
                {
                    GetComponent<Renderer>().material.color = Color.cyan;
                }
            }
        }
        if (canMv)
        {
            if (visitable.Contains(this))
            {
                if (!(HasUnit() && canAtk))
                {
                    GetComponent<Renderer>().material.color = Color.red;
                }
            }  
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
            if (HasObstacle())
                return false;
        }
        return true;//for now
    }

    public UnitProxy GetUnit()
    {
        return (UnitProxy) objectProxies.Where(op => op is UnitProxy).First();
    }

    public bool HasObstacle()
    {
        return objectProxies.Where(op => op is ObstacleProxy).Any();
    }

    public bool HasUnit()
    {
        return objectProxies.Where(op => op is UnitProxy).Any();
    }

    public bool HasObstruction()
    {
        return HasUnit() || HasObstacle();
    }

    public bool UnitOnTeam(int team)
    {
        return objectProxies.Where(op => op is UnitProxy && ((UnitProxy)op).GetData().GetTeam() == team).Any();
    }

    public void SetTurnsOnFire(int trns){
        fireTrns += trns;
        if (fireTrns > 0) {
            GetComponent<SpriteRenderer>().sprite = fireAlt;
        }
    }

    public bool OnFire(){
        return fireTrns > 0;
    }

    public void DecrementTileEffects(){
        if (fireTrns > 0 && HasUnit()) {
            //Only injure unit from fire if it's that unit's team's turn
            if (GetUnit().GetData().GetTeam() == TurnController.instance.currentTeam && GetUnit().IsAttackedEnvironment(1)){
                ConditionTracker.instance.EvalDeath(GetUnit());
            }
        }

        fireTrns = fireTrns - 1 > 0 ? fireTrns - 1 : 0;
        if (fireTrns == 0) {
            GetComponent<SpriteRenderer>().sprite = def;
        }
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
    }
  
    #endregion
}
