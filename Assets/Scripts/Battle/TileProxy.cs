﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileProxy : MonoBehaviour, IHasNeighbours<TileProxy>, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static float NO_ATK_WAIT = .5f;
    public static float ATK_WAIT = 1.6f;

    public static Color MOVE = Color.grey;
    public Color ATK_INACTIVE = new Color(.86f,.079f,.24f);
    public static Color ATK_ACTIVE = Color.magenta;

    [SerializeField]
    private Tile tile;

    [SerializeField]
    private Transform anchor;

    private UnitProxy unitThatSetTileOnFire;
    private int fireTrns;
    private Sprite def, fireAlt;
    private float timeLeft = 0;
    private float FIRE_DELAY_TIME = .5f;

    private List<GridObjectProxy> objectProxies = new List<GridObjectProxy>();
    //private GameObject instanceDummy;

    //void Awake(){
    //    instanceDummy = new GameObject();
    //}

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
        GetComponent<Renderer>().material.color = MOVE;
    }

    public void HighlightSelectedAdv(UnitProxy inRangeUnit, List<TileProxy> visitable, List<TileProxy> attackable)
    {
        bool canAtk = inRangeUnit.GetData().GetTurnActions().CanAttack();
        bool canMv = inRangeUnit.GetData().GetTurnActions().CanMove();

        if (BoardProxy.instance.GetTileAtPosition(inRangeUnit.GetPosition()) == this)
        {
            if (!canAtk && !canMv)
            {
                GetComponent<Renderer>().material.color = MOVE;
            }
            return;
        }
    
        if (canAtk)
        {
            if (attackable.Contains(this))
            {
                if (HasUnit() && inRangeUnit.GetData().GetTeam() != GetUnit().GetData().GetTeam())
                {
                    GetComponent<Renderer>().material.color = ATK_ACTIVE;
                }
                else if (!HasObstacle())
                {
                    GetComponent<Renderer>().material.color = ATK_INACTIVE;
                }
            }
        }
        if (canMv)
        {
            if (visitable.Contains(this))
            {
                if (!(HasUnit() && canAtk))
                {
                    GetComponent<Renderer>().material.color = MOVE;
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
        return (UnitProxy) objectProxies.ToList().Where(op => op is UnitProxy).First();
    }

    public bool HasObstacle()
    {
        return objectProxies.ToList().Where(op => op is ObstacleProxy).Any();
    }

    public bool HasUnit()
    {
        return objectProxies.ToList().Where(op => op is UnitProxy).Any();
    }

    public bool HasObstruction()
    {
        return HasUnit() || HasObstacle();
    }

    public bool UnitOnTeam(int team)
    {
        return objectProxies.ToList().Where(op => op is UnitProxy && ((UnitProxy)op).GetData().GetTeam() == team).Any();
    }

    public void SetTurnsOnFire(int trns, UnitProxy unit){
        unitThatSetTileOnFire = unit;
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
                if (unitThatSetTileOnFire != null) {
                    unitThatSetTileOnFire.GetData().SetLvl(unitThatSetTileOnFire.GetData().GetLvl() + 1);
                }
                ConditionTracker.instance.EvalDeath(GetUnit());
            }
        }

        fireTrns = fireTrns - 1 > 0 ? fireTrns - 1 : 0;
        if (fireTrns == 0) {
            GetComponent<SpriteRenderer>().sprite = def;
            unitThatSetTileOnFire = null;
        }
    }

    // Update is called once per frame
    void Update () {
       if (OnFire()) {
           timeLeft -= Time.deltaTime;
           if ( timeLeft <= 0 )
           {
              timeLeft = FIRE_DELAY_TIME;
              FloatUp(Skill.Actions.None, "fire", Color.red, "Tile on fire");
           }
       }
    }

    public void FloatUp(Skill.Actions interaction, string msg, Color color, string desc){
        AnimationInteractionController.InteractionAnimation(interaction, this, msg, color, desc);
    }

    public void CreateSmoke(){
        StartCoroutine(CreateSmokeAnim());
    }

    IEnumerator CreateSmokeAnim(){
        Vector3 instPos = transform.position;
        instPos.y += .8f;
        GameObject smoke = Instantiate(BoardProxy.instance.glossary.GetComponent<Glossary>().Smoke, instPos, Quaternion.identity);
        //smoke.transform.position = Vector3.zero;
        //smoke.GetComponent<RectTransform>().ForceUpdateRectTransforms();
        yield return new WaitForSeconds(1f);
        Destroy(smoke);
    }

    #region events
    public void OnPointerDown(PointerEventData eventData)
    {
        if (TurnController.instance.PlayersTurn()){
            InteractivityManager.instance.OnTileSelected(this);
            foreach (var obj in objectProxies.ToList())
            {
                obj.OnSelected();
            }
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        InteractivityManager.instance.OnTileUnHovered(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (TurnController.instance.PlayersTurn()){
            InteractivityManager.instance.OnTileHovered(this);
        }
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
