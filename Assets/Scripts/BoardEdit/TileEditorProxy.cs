using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileEditorProxy : MonoBehaviour, IHasNeighbours<TileEditorProxy>, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
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

    private UnitProxyEditor unitThatSetTileOnFire;
    private int fireTrns, wallTrns, divineTrns, snowTrns;
    private Sprite def, fireAlt, wallAlt, divineAlt, snowAlt;
    private float timeLeft = 0;
    private float FIRE_DELAY_TIME = .5f;

    private bool lifetimeWall;
    private bool lifetimeDivine;
    private bool lifetimeSnow;
    private bool lifetimeFire;

    private List<GridObjectProxyEdit> objectProxies = new List<GridObjectProxyEdit>();
    //private GameObject instanceDummy;

    //void Awake(){
    //    instanceDummy = new GameObject();
    //}

    public Vector3Int GetPosition()
    {
        return tile.position;
    }

    public IEnumerable<TileEditorProxy> Neighbours
    {
        get
        {
            return BoardEditProxy.instance.GetNeighborTiles(tile.position);
        }
    }

    void SnapToPosition()
    {
        transform.position = BoardEditProxy.instance.grid.CellToLocal(tile.position);
    }

    public void Init(Tile t, Sprite fireAlt, Sprite wallAlt, Sprite divineAlt, Sprite snowAlt)
    {
        def = GetComponent<SpriteRenderer>().sprite;
        this.fireAlt = fireAlt;
        this.wallAlt = wallAlt;
        this.divineAlt = divineAlt;
        this.snowAlt = snowAlt;
        tile = t;
        name = t.position.ToString();//for convenience in the heirarchy
        SnapToPosition();
    }

    public void HighlightSelected()
    {
        GetComponent<Renderer>().material.color = MOVE;
    }

    public void HighlightSelectedAdv(UnitProxyEditor inRangeUnit, List<TileEditorProxy> visitable, List<TileEditorProxy> attackable)
    {
        bool canAtk = inRangeUnit.GetData().GetTurnActions().CanAttack();
        bool canMv = inRangeUnit.GetData().GetTurnActions().CanMove();

        if (BoardEditProxy.instance.GetTileAtPosition(inRangeUnit.GetPosition()) == this)
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

    public void ClearAllEffects(){
        SetLifeWall(false);
        SetLifeDivine(false);
        SetLifeSnow(false);
        SetLifeFire(false);
        FlushGridObjectProxies();
    }

    public void SetLifeWall(bool lifetimeWall){
        this.lifetimeWall = lifetimeWall;
        wallTrns = 0;
        if (lifetimeWall) {
          wallTrns = 1;
          ObstacleProxyEdit obs = Instantiate(BoardEditProxy.instance.glossary.GetComponent<Glossary>().obstacleEditTile, BoardEditProxy.instance.transform);
          obs.Init();
          ReceiveGridObjectProxy(obs);
          obs.SnapToCurrentPosition();
        }
    }

    public void SetLifeDivine(bool lifetimeDivine){
        this.lifetimeDivine = lifetimeDivine;
        divineTrns = 0;
        if (lifetimeDivine) {
          divineTrns = 1;
        }
    }

    public void SetLifeSnow(bool lifetimeSnow){
        this.lifetimeSnow = lifetimeSnow;
        snowTrns = 0;
        if (lifetimeSnow) {
          snowTrns = 1;
        }
    }

    public void SetLifeFire(bool lifetimeFire){
        this.lifetimeFire = lifetimeFire;
        fireTrns = 0;
        if (lifetimeFire) {
          fireTrns = 1;
        }
    }

    public void ForceHighlight()
    {
        if(this != null){
            GetComponent<Renderer>().material.color = Color.green;
        }
    }

    public void UnHighlight()
    {
        if(this != null){
            GetComponent<Renderer>().material.color = Color.white;
        }
    }

    public void ReceiveGridObjectProxy(GridObjectProxyEdit proxy)
    {
        if (!objectProxies.Contains(proxy) && proxy != null)
        {
            objectProxies.Add(proxy);
            proxy.SetPosition(tile.position);
        }
    }

    public void RemoveGridObjectProxy(GridObjectProxyEdit proxy)
    {
        Debug.Log("Grid Object Removed: " + proxy.name);
        if (objectProxies.Contains(proxy))
        {
            objectProxies.Remove(proxy);
        }
    }

    public void FlushGridObjectProxies(){
        foreach (GridObjectProxyEdit proxy in objectProxies)
        {
            Destroy(proxy.gameObject);
        }
        objectProxies.Clear();
    }

    public List<GridObjectProxyEdit> GetContents()
    {
        return objectProxies;
    }

    public bool CanReceive(GridObjectProxyEdit obj)
    {
        if (obj is UnitProxyEditor)
        {
            if (HasUnit())//TODO: rework to a better system with layers
                return false;
        }
        return true;//for now
    }

    public UnitProxyEditor GetUnit()
    {
        return (UnitProxyEditor) objectProxies.ToList().Where(op => op is UnitProxyEditor).First();
    }

    //public ObstacleProxy GetObstacle()
    //{
    //    return (ObstacleProxy) objectProxies.ToList().Where(op => op is ObstacleProxy).First();
    //}

    public bool HasObstacle()
    {
        return objectProxies.ToList().Where(op => op is ObstacleProxyEdit).Any();
    }

    public bool HasUnit()
    {
        return objectProxies.ToList().Where(op => op is UnitProxyEditor).Any();
    }

    public bool HasObstruction()
    {
        return HasUnit();
    }

    public bool UnitOnTeam(int team)
    {
        return objectProxies.ToList().Where(op => op is UnitProxyEditor && ((UnitProxyEditor)op).GetData().GetTeam() == team).Any();
    }

    //void ResetTile(){
    //    fireTrns = 0;
    //    wallTrns = 0;
    //    divineTrns = 0;
    //    snowTrns = 0;
    //}

    /*
      Fire
    */

    //public void SetTurnsOnFire(int trns, UnitProxyEditor unit){
    //    unitThatSetTileOnFire = unit;
    //    if (fireTrns == 0) {
    //        ResetTile();
    //    }
    //    fireTrns += trns;
    //    if (fireTrns > 0) {
    //        GetComponent<SpriteRenderer>().sprite = fireAlt;
    //    }
    //}

    public bool OnFire(){
        return fireTrns > 0;
    }

    /*
      Wall
    */

    //public void SetTurnsWall(int trns, UnitProxyEditor unit){
    //    unitThatSetTileOnFire = unit;
    //    if (wallTrns == 0) {
    //        ResetTile();
    //    }
    //    wallTrns += trns;
    //    if (wallTrns > 0) {
    //        GetComponent<SpriteRenderer>().sprite = wallAlt;
    //        ObstacleProxyEdit obs = Instantiate(BoardEditProxy.instance.GetComponent<Glossary>().obstacleEditTile, transform);
    //        obs.Init();
    //        ReceiveGridObjectProxy(obs);
    //        obs.SnapToCurrentPosition();
    //    }
    //}

    public bool IsWall(){
        return wallTrns > 0;
    }

    /*
      Divine
    */

    //public void SetTurnsDivine(int trns, UnitProxyEditor unit){
    //    unitThatSetTileOnFire = unit;
    //    if (divineTrns == 0) {
    //        ResetTile();
    //    }
    //    divineTrns += trns;
    //    if (divineTrns > 0) {
    //        GetComponent<SpriteRenderer>().sprite = divineAlt;
    //    }
    //}

    public bool IsDivine(){
        return divineTrns > 0;
    }

    public void CreateUnitOnTile(UnitProxyEditor unt){
        UnitProxyEditor newEditable = Instantiate(unt, BoardEditProxy.instance.transform);
        UnitEdit cMeta = new UnitEdit();
        newEditable.PutData(cMeta);
        newEditable.Init();
        newEditable.SetPosition(GetPosition());
        ReceiveGridObjectProxy(newEditable);
        newEditable.SnapToCurrentPosition();
    }

    /*
      Snow
    */

    //public void SetTurnsFrozen(int trns, UnitProxyEditor unit){
    //    unitThatSetTileOnFire = unit;
    //    if (snowTrns == 0) {
    //        ResetTile();
    //    }
    //    snowTrns += trns;
    //    if (snowTrns > 0) {
    //        GetComponent<SpriteRenderer>().sprite = snowAlt;
    //    }
    //}

    public bool Frozen(){
        return snowTrns > 0;
    }

    //public void DecrementTileEffects(){
    //    if (fireTrns > 0 && HasUnit()) {
    //        //Only injure unit from fire if it's that unit's team's turn
    //        if (GetUnit().GetData().GetTeam() == TurnController.instance.currentTeam && GetUnit().IsAttackedEnvironment(1)){
    //            if (unitThatSetTileOnFire != null) {
    //                unitThatSetTileOnFire.GetData().SetLvl(unitThatSetTileOnFire.GetData().GetLvl() + 1);
    //            }
    //            ConditionTracker.instance.EvalDeath(GetUnit());
    //        }
    //    }

    //    if (divineTrns > 0 && HasUnit()) {
    //        //Divine tiles heal
    //        FloatUp(Skill.Actions.None, "+1", Color.green, "Healed from tile");
    //        GetUnit().GetData().SetCurrHealth(GetUnit().GetData().GetCurrHealth() + 1);
    //    }

    //    if (snowTrns > 0 && HasUnit()) {
    //        //Snow tiles apply enfeeble and rooted at the end of the turn
    //        if (GetUnit().GetData().GetTeam() == TurnController.instance.currentTeam){
    //            FloatUp(Skill.Actions.None, "enfeebled", Color.red, "Enfeebled from tile");
    //            FloatUp(Skill.Actions.None, "rooted", Color.red, "Rooted from tile");
    //            GetUnit().GetData().GetTurnActions().EnfeebledForTurns(1);
    //            GetUnit().GetData().GetTurnActions().RootForTurns(1);
    //        }
    //    }

    //    if (wallTrns <= 0 && HasObstacle()) {
    //        RemoveGridObjectProxy(GetObstacle());
    //    }

    //    if (!lifetimeWall) {
    //      wallTrns = wallTrns - 1 > 0 ? wallTrns - 1 : 0;
    //    }
    //    if (!lifetimeDivine) {
    //      divineTrns = divineTrns - 1 > 0 ? divineTrns - 1 : 0;
    //    }
    //    if (!lifetimeSnow) {
    //      snowTrns = snowTrns - 1 > 0 ? snowTrns - 1 : 0;
    //    }
    //    if (!lifetimeFire) {
    //      fireTrns = fireTrns - 1 > 0 ? fireTrns - 1 : 0;
    //    }

    //    //fireTrns = fireTrns - 1 > 0 ? fireTrns - 1 : 0;
    //    //wallTrns = wallTrns - 1 > 0 ? wallTrns - 1 : 0;
    //    //divineTrns = divineTrns - 1 > 0 ? divineTrns - 1 : 0;
    //    //snowTrns = snowTrns - 1 > 0 ? snowTrns - 1 : 0;

    //    if (!OnFire() && !IsWall() && !Frozen() && !IsDivine()) {
    //        GetComponent<SpriteRenderer>().sprite = def;
    //        unitThatSetTileOnFire = null;
    //    }
    //}

    //Update is called once per frame
    void Update () {
       //if (IsWall()) {
       //    timeLeft -= Time.deltaTime;
       //    if ( timeLeft <= 0 )
       //    {
       //       timeLeft = FIRE_DELAY_TIME;
       //    }
       //}
       //if (HasObstacle()) {
       //  transform.GetComponent<SpriteRenderer>().color = Color.red;
       //}
    }

    //public void FloatUp(Skill.Actions interaction, string msg, Color color, string desc){
    //    AnimationInteractionController.InteractionAnimation(interaction, this, msg, color, desc);
    //}

    public void CreateSmoke(){
        StartCoroutine(CreateSmokeAnim());
    }

    IEnumerator CreateSmokeAnim(){
        Vector3 instPos = transform.position;
        instPos.y += .8f;
        GameObject smoke = Instantiate(BoardEditProxy.instance.glossary.GetComponent<Glossary>().Smoke, instPos, Quaternion.identity);
        yield return new WaitForSeconds(1f);
        Destroy(smoke);
    }

    #region events
    public void OnPointerDown(PointerEventData eventData)
    {
        InteractivityManagerEditor.instance.OnTileSelected(this);
        foreach (var obj in objectProxies.ToList())
        {
            obj.OnSelected();
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        InteractivityManagerEditor.instance.OnTileUnHovered(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (TurnController.instance.PlayersTurn()){
            InteractivityManagerEditor.instance.OnTileHovered(this);
        }
    }

    Vector3 _startPosition;
    Vector3 _offsetToMouse;
    float _zDistanceToCamera;

    public void OnBeginDrag (PointerEventData eventData)
    {
       Debug.Log("OnBeginDrag");
       _startPosition = BoardEditProxy.instance.grid.transform.position;
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
       BoardEditProxy.instance.grid.transform.position = Camera.main.ScreenToWorldPoint (
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
