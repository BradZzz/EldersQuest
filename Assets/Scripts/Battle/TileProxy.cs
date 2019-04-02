using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileProxy : MonoBehaviour, IHasNeighbours<TileProxy>, IPointerClickHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static float NO_ATK_WAIT = .5f;
    public static float ATK_WAIT = 1.6f;

    public static Color SHADING_UNDERLAY = Color.grey;
    public static Color MOVE = Color.yellow;
    public Color ATK_INACTIVE = new Color(.86f,.079f,.24f);
    public static Color ATK_ACTIVE = Color.magenta;

    [SerializeField]
    private Tile tile;

    [SerializeField]
    private Transform anchor;

    private UnitProxy unitThatSetTileOnFire;
    private int fireTrns, wallTrns, divineTrns, snowTrns;
    private Sprite def, fireAlt, wallAlt, divineAlt, snowAlt;
    private float timeLeft = 0;
    private float FIRE_DELAY_TIME = .5f;

    private bool lifetimeWall;
    private bool lifetimeDivine;
    private bool lifetimeSnow;
    private bool lifetimeFire;

    private UnitProxy stuckUnit;

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

    public void Init(Tile t, Sprite grassDef, Sprite fireAlt, Sprite wallAlt, Sprite divineAlt, Sprite snowAlt)
    {
        def = GetComponent<SpriteRenderer>().sprite = grassDef;
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
        GetComponent<Renderer>().material.color = SHADING_UNDERLAY;
        //transform.GetChild(0).gameObject.SetActive(true);
        //transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = MOVE;
    }

    public void HighlightSelectedAdv(UnitProxy inRangeUnit, List<TileProxy> visitable, List<TileProxy> attackable)
    {
        bool canAtk = inRangeUnit.GetData().GetTurnActions().CanAttack();
        bool canMv = inRangeUnit.GetData().GetTurnActions().CanMove();

        if (BoardProxy.instance.GetTileAtPosition(inRangeUnit.GetPosition()) == this)
        {
            if (!canAtk && !canMv)
            {
                GetComponent<Renderer>().material.color = SHADING_UNDERLAY;
                transform.GetChild(0).gameObject.SetActive(true);
                transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = MOVE;
            }
            return;
        }
    
        if (canAtk)
        {
            if (attackable.Contains(this))
            {
                if (HasUnit() && inRangeUnit.GetData().GetTeam() != GetUnit().GetData().GetTeam())
                {
                    //GetComponent<Renderer>().material.color = ATK_ACTIVE;
                    GetComponent<Renderer>().material.color = SHADING_UNDERLAY;
                    transform.GetChild(0).gameObject.SetActive(true);
                    transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = ATK_ACTIVE;
                }
                else if (!HasObstacle())
                {
                    GetComponent<Renderer>().material.color = SHADING_UNDERLAY;
                    transform.GetChild(0).gameObject.SetActive(true);
                    transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = ATK_INACTIVE;
                    //GetComponent<Renderer>().material.color = ATK_INACTIVE;
                }
            }
        }
        if (canMv)
        {
            if (visitable.Contains(this))
            {
                if (!(HasUnit() && canAtk))
                {
                    GetComponent<Renderer>().material.color = SHADING_UNDERLAY;
                    transform.GetChild(0).gameObject.SetActive(true);
                    transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = MOVE;
                    //GetComponent<Renderer>().material.color = MOVE;
                }
            }  
        }
    }

    public void SetLifeWall(bool lifetimeWall){
        this.lifetimeWall = lifetimeWall;
        //wallTrns = 1;
        CheckWall();
    }

    public void SetLifeDivine(bool lifetimeDivine){
        this.lifetimeDivine = lifetimeDivine;
        //divineTrns = 1;
        CheckDivine();
    }

    public void SetLifeSnow(bool lifetimeSnow){
        this.lifetimeSnow = lifetimeSnow;
        //snowTrns = 1;
        CheckFrozen();
    }

    public void SetLifeFire(bool lifetimeFire){
        this.lifetimeFire = lifetimeFire;
        //fireTrns = 1;
        CheckFire();
    }

    public void ForceHighlight()
    {
        if(this != null){
            GetComponent<Renderer>().material.color = SHADING_UNDERLAY;
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = Color.green;
            //GetComponent<Renderer>().material.color = MOVE;
        }
    }

    public void UnHighlight()
    {
        if(this != null){
            GetComponent<Renderer>().material.color = Color.white;
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    public void ReceiveGridObjectProxy(GridObjectProxy proxy)
    {
        if (!objectProxies.Contains(proxy) && proxy != null)
        {
            objectProxies.Add(proxy);
            proxy.SetPosition(tile.position);
        }
    }

    public void RemoveGridObjectProxy(GridObjectProxy proxy)
    {
        if (objectProxies.Contains(proxy))
        {
            Debug.Log("Grid Object Removed: " + proxy.name);
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

    public ObstacleProxy GetObstacle()
    {
        return (ObstacleProxy) objectProxies.ToList().Where(op => op is ObstacleProxy).First();
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

    void ResetTile(){
        fireTrns = 0;
        wallTrns = 0;
        divineTrns = 0;
        snowTrns = 0;
    }

    /*
      Fire
    */

    public void SetTurnsOnFire(int trns, UnitProxy unit){
        unitThatSetTileOnFire = unit;
        if (fireTrns == 0) {
            ResetTile();
        }
        fireTrns += trns;
        CheckFire();
    }

    void CheckFire(){
        if (OnFire()) {
            GetComponent<SpriteRenderer>().sprite = fireAlt;
        }
    }

    public bool OnFire(){
        return fireTrns > 0 || (lifetimeFire && !CheckTemps());
    }

    /*
      Wall
    */

    public void SetTurnsWall(int trns, UnitProxy unit){
        unitThatSetTileOnFire = unit;
        if (wallTrns == 0) {
            ResetTile();
        }
        wallTrns += trns;
        CheckWall();
    }

    void CheckWall(){
        if (IsWall()) {
            GetComponent<SpriteRenderer>().sprite = def;
            if (!HasObstacle()) {
                List<ObstacleProxy> obsList = new List<ObstacleProxy>(BoardProxy.instance.glossary.GetComponent<Glossary>().obstacles);
                ObstacleProxy[] obsArr = obsList.ToArray();
                HelperScripts.Shuffle(obsArr);
                ObstacleProxy obs = Instantiate(obsArr[0], transform);
                obs.Init();
                ReceiveGridObjectProxy(obs);
                obs.SnapToCurrentPosition();
            }
        }
    }

    public bool IsWall(){
        return wallTrns > 0 || (lifetimeWall && !CheckTemps());
    }

    /*
      Divine
    */

    public void SetTurnsDivine(int trns, UnitProxy unit){
        unitThatSetTileOnFire = unit;
        if (divineTrns == 0) {
            ResetTile();
        }
        divineTrns += trns;
        CheckDivine();
    }

    void CheckDivine(){
        if (IsDivine()) {
            GetComponent<SpriteRenderer>().sprite = divineAlt;
        }
    }

    public bool IsDivine(){
        return divineTrns > 0 || (lifetimeDivine && !CheckTemps());
    }

    /*
      Snow
    */

    public void SetTurnsFrozen(int trns, UnitProxy unit){
        unitThatSetTileOnFire = unit;
        if (snowTrns == 0) {
            ResetTile();
        }
        snowTrns += trns;
        CheckFrozen();
    }

    void CheckFrozen(){
        if (Frozen()) {
            GetComponent<SpriteRenderer>().sprite = snowAlt;
        }
    }

    public bool Frozen(){
        return snowTrns > 0 || (lifetimeSnow && !CheckTemps());
    }

    bool CheckTemps(){
        return snowTrns > 0 || divineTrns > 0 || wallTrns > 0 || fireTrns > 0;
    }

    void CheckAll(){
        CheckWall();
        CheckDivine();
        CheckFrozen();
        CheckFire();
    }

    public void DecrementTileEffects(){
        //if (OnFire() && HasUnit()) {
        //    //Only injure unit from fire if it's that unit's team's turn
        //    if (GetUnit().GetData().GetTeam() == TurnController.instance.currentTeam && GetUnit().IsAttackedEnvironment(1)){
        //        if (unitThatSetTileOnFire != null) {
        //            unitThatSetTileOnFire.GetData().SetLvl(unitThatSetTileOnFire.GetData().GetLvl() + 1);
        //        }
        //        ConditionTracker.instance.EvalDeath(GetUnit());
        //    }
        //}

        if (IsDivine() && HasUnit()) {
            //Divine tiles heal
            if (GetUnit().GetData().GetTeam() == TurnController.instance.currentTeam){
                FloatUp(Skill.Actions.None, "+1", Color.green, "Healed from tile");
                GetUnit().GetData().SetCurrHealth(GetUnit().GetData().GetCurrHealth() + 1);
            }
        }

        //if (Frozen() && HasUnit()) {
        //    //Snow tiles apply enfeeble and rooted at the end of the turn
        //    if (GetUnit().GetData().GetTeam() == TurnController.instance.currentTeam && stuckUnit != GetUnit()){
        //        stuckUnit = GetUnit();
        //        FloatUp(Skill.Actions.None, "enfeebled", Color.red, "Enfeebled from tile");
        //        FloatUp(Skill.Actions.None, "rooted", Color.red, "Rooted from tile");
        //        GetUnit().GetData().GetTurnActions().EnfeebledForTurns(1);
        //        GetUnit().GetData().GetTurnActions().RootForTurns(1);
        //    } else if (GetUnit().GetData().GetTeam() == TurnController.instance.currentTeam && stuckUnit == GetUnit()){
        //        stuckUnit = null;
        //    }
        //}

        if (!IsWall() && HasObstacle()) {
            RemoveGridObjectProxy(GetObstacle());
        }

        wallTrns = wallTrns - 1 > 0 ? wallTrns - 1 : 0;
        divineTrns = divineTrns - 1 > 0 ? divineTrns - 1 : 0;
        snowTrns = snowTrns - 1 > 0 ? snowTrns - 1 : 0;
        fireTrns = fireTrns - 1 > 0 ? fireTrns - 1 : 0;

        if (!OnFire() && !IsWall() && !Frozen() && !IsDivine()) {
            GetComponent<SpriteRenderer>().sprite = def;
            unitThatSetTileOnFire = null;
        } else {
            CheckAll();
        }
    }

    // Update is called once per frame
    void Update () {
       if (OnFire()) {
           timeLeft -= Time.deltaTime;
           if ( timeLeft <= 0 )
           {
              timeLeft = FIRE_DELAY_TIME;
              //FloatUp(Skill.Actions.None, "fire", Color.red, "Tile on fire");
              CreateAnimation(Glossary.fx.firePillar);
           }
       }
       if (Frozen()) {
           timeLeft -= Time.deltaTime;
           if ( timeLeft <= 0 )
           {
              timeLeft = FIRE_DELAY_TIME;
              //FloatUp(Skill.Actions.None, "snowy", Color.blue, "Tile is frozen");
              CreateAnimation(Glossary.fx.snowSmoke);
           }
       }
       if (IsDivine()) {
           timeLeft -= Time.deltaTime;
           if ( timeLeft <= 0 )
           {
              timeLeft = FIRE_DELAY_TIME;
              //FloatUp(Skill.Actions.None, "holy", Color.yellow, "Tile is holy");
              //CreateHealSmoke();
              CreateAnimation(Glossary.fx.healSmoke);
           }
       }
       if (IsWall()) {
           timeLeft -= Time.deltaTime;
           if ( timeLeft <= 0 )
           {
              timeLeft = FIRE_DELAY_TIME;
              //FloatUp(Skill.Actions.None, "wall", Color.magenta, "Tile is wall");
           }
       }
       //if (HasObstacle()) {
       //  transform.GetComponent<SpriteRenderer>().color = Color.red;
       //}
    }

    public void FloatUp(Skill.Actions interaction, string msg, Color color, string desc){
        AnimationInteractionController.InteractionAnimation(interaction, this, msg, color, desc);
    }

    //public void CreateSmoke(){
    //    StartCoroutine(CreateSmokeAnim());
    //}

    //IEnumerator CreateSmokeAnim(){
    //    Vector3 instPos = transform.position;
    //    instPos.y += .8f;
    //    GameObject smoke = Instantiate(BoardProxy.instance.glossary.GetComponent<Glossary>().fxSmoke1, instPos, Quaternion.identity);
    //    yield return new WaitForSeconds(1f);
    //    Destroy(smoke);
    //}

    //public void CreateSnow(){
    //    StartCoroutine(CreateSnowAnim());
    //}

    //IEnumerator CreateSnowAnim(){
    //    Vector3 instPos = transform.position;
    //    instPos.y += .8f;
    //    GameObject smoke2 = Instantiate(BoardProxy.instance.glossary.GetComponent<Glossary>().fxSmoke2, instPos, Quaternion.identity);
    //    yield return new WaitForSeconds(1f);
    //    Destroy(smoke2);
    //}

    //public void CreateFire(){
    //    StartCoroutine(CreateFireAnim());
    //}

    //IEnumerator CreateFireAnim(){
    //    Vector3 instPos = transform.position;
    //    instPos.y += .8f;
    //    GameObject fire = Instantiate(BoardProxy.instance.glossary.GetComponent<Glossary>().fxFirePillar, instPos, Quaternion.identity);
    //    yield return new WaitForSeconds(1f);
    //    Destroy(fire);
    //}

    //public void CreateHealSmoke(){
    //    StartCoroutine(CreateHealSmokeAnim());
    //}

    //IEnumerator CreateHealSmokeAnim(){
    //    Vector3 instPos = transform.position;
    //    instPos.y += .8f;
    //    GameObject healSmk = Instantiate(BoardProxy.instance.glossary.GetComponent<Glossary>().fxHealSmoke, instPos, Quaternion.identity);
    //    yield return new WaitForSeconds(1f);
    //    Destroy(healSmk);
    //}

    public void CreateAnimation(Glossary.fx fx, float wait = 0){
        StartCoroutine(PlayAnim(fx, wait));
    }

    IEnumerator PlayAnim(Glossary.fx fx, float wait = 0){
        yield return new WaitForSeconds(wait);
        Vector3 instPos = transform.position;
        instPos.y += .7f;
        GameObject fxAnim = null;
        switch(fx){
            case Glossary.fx.barrage:fxAnim=BoardProxy.instance.glossary.GetComponent<Glossary>().fxBarrage;break;
            case Glossary.fx.bloodExplosions:fxAnim=BoardProxy.instance.glossary.GetComponent<Glossary>().fxBloodExplosion;break;
            case Glossary.fx.bloodSplatter:fxAnim=BoardProxy.instance.glossary.GetComponent<Glossary>().fxBloodSplatter;break;
            case Glossary.fx.egExplosion:fxAnim=BoardProxy.instance.glossary.GetComponent<Glossary>().fxEGExplosion;break;
            case Glossary.fx.fireBaseLarge:fxAnim=BoardProxy.instance.glossary.GetComponent<Glossary>().fxFireBaseLarge;break;
            case Glossary.fx.fireBaseSmall:fxAnim=BoardProxy.instance.glossary.GetComponent<Glossary>().fxFireBaseSmall;break;
            case Glossary.fx.firePillar:fxAnim=BoardProxy.instance.glossary.GetComponent<Glossary>().fxFirePillar;break;
            case Glossary.fx.fireShield:fxAnim=BoardProxy.instance.glossary.GetComponent<Glossary>().fxFireShield;break;
            case Glossary.fx.healSmoke:fxAnim=BoardProxy.instance.glossary.GetComponent<Glossary>().fxHealSmoke;break;
            case Glossary.fx.hmExplosion:fxAnim=BoardProxy.instance.glossary.GetComponent<Glossary>().fxHMExplosion;break;
            case Glossary.fx.laser:fxAnim=BoardProxy.instance.glossary.GetComponent<Glossary>().fxLaser;break;
            case Glossary.fx.lpExplosion:fxAnim=BoardProxy.instance.glossary.GetComponent<Glossary>().fxLPExplosion;break;
            case Glossary.fx.smoke1:fxAnim=BoardProxy.instance.glossary.GetComponent<Glossary>().fxSmoke1;break;
            case Glossary.fx.smoke2:fxAnim=BoardProxy.instance.glossary.GetComponent<Glossary>().fxSmoke2;break;
            case Glossary.fx.smoke3:fxAnim=BoardProxy.instance.glossary.GetComponent<Glossary>().fxSmoke3;break;
            case Glossary.fx.snowSmoke:fxAnim=BoardProxy.instance.glossary.GetComponent<Glossary>().fxSnowSmoke;break;
        }
        GameObject healSmk = Instantiate(fxAnim, instPos, Quaternion.identity);
        yield return new WaitForSeconds(1f);
        Destroy(healSmk);
    }

    #region events
    public void OnPointerDown(PointerEventData eventData)
    {
        //if (TurnController.instance.PlayersTurn()){
        //    InteractivityManager.instance.OnTileSelected(this);
        //    foreach (var obj in objectProxies.ToList())
        //    {
        //        obj.OnSelected();
        //    }
        //}
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
       InteractivityManager.instance.OnClear(this);
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

  public void OnPointerClick(PointerEventData eventData)
  {
        if (TurnController.instance.PlayersTurn()){
            InteractivityManager.instance.OnTileSelected(this);
            foreach (var obj in objectProxies.ToList())
            {
                obj.OnSelected();
            }
        }
  }

  #endregion
}
