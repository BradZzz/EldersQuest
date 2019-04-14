using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UnitProxy : GridObjectProxy
{
    public GameObject aegisObj;
    public GameObject rankObj;

    //public static float NO_ATK_WAIT = .5f;
    //public static float ATK_WAIT = 1.1f;
    public static float MV_TIME = .25f;

    private Unit _data;
    private float defaultAnimSpeed;
    private bool givenExp;
    private bool waited;

    protected override GridObject data
    {
        get { return _data; }
    }

    public override void OnSelected()
    {
        InteractivityManager.instance.OnUnitSelected(this);
    }

    void Awake(){
        aegisObj.SetActive(false);
        rankObj.SetActive(false);
        defaultAnimSpeed = transform.GetChild(0).GetComponent<Animator>().speed;
        givenExp = false;
        Debug.Log("defaultAnimSpeed: " + defaultAnimSpeed.ToString());
    }

    public void Init()
    {
        if (_data == null)
            _data = new Unit();
        SnapToCurrentPosition();
        int rnk = GetData().GetUnitClassRank();
        if (rnk > 0) {
          rankObj.SetActive(true);
          rankObj.GetComponent<SpriteRenderer>().sprite = BoardProxy.instance.glossary.GetComponent<Glossary>().ranks[rnk - 1];
        }
    }

    public void AddLevel(){
      //FloatUp(Skill.Actions.DidKill, "+1xp", Color.green, "gained +1 xp", true);
      AnimationInteractionController.InteractionAnimationGameobject(
        BoardProxy.instance.glossary.GetComponent<Glossary>().exp, gameObject, AnimationInteractionController.ANIMATION_WAIT_TIME_LIMIT, true);
      _data.SetLvl(_data.GetLvl()+1);
    }

    public bool IsAlive(){
        return GetData().GetCurrHealth() > 0;
    }

    public bool IsAttacked(UnitProxy oppUnit, bool useAttack = true)
    {
      Debug.Log("IsAttacked");
      if (useAttack) {
          oppUnit.GetData().GetTurnActions().Attack();
      }
      PanelControllerNew.SwitchChar(oppUnit);

      //Damage the unit
      /*
        Trigger the opponent's attack trigger here
      */

      Vector3Int animStart = oppUnit.GetPosition();
      Vector3Int animEnd = GetPosition();

      Debug.Log("animStart: " + animStart.ToString());
      Debug.Log("animEnd: " + animEnd.ToString());

      Vector3Int diff = animEnd - animStart;

      if (_data.GetAegis()) {
          Debug.Log("Aegis!");
          //_data.SetAegis(false);
          StartCoroutine(AttackAnim(oppUnit, oppUnit.gameObject.transform.GetChild(0).GetComponent<Animator>(), 
            oppUnit.gameObject.transform, diff, "", useAttack));
          //LostAegisAnim();
          SetAegis(false, AnimationInteractionController.ATK_WAIT);
          return false;
      }
      Debug.Log("No Aegis");

      StartCoroutine(AttackAnim(oppUnit, oppUnit.gameObject.transform.GetChild(0).GetComponent<Animator>(), 
        oppUnit.gameObject.transform, diff, "-" + oppUnit.GetData().GetAttack().ToString(), useAttack));

      GetData().IsAttacked(oppUnit.GetData().GetAttack());
      if (GetData().IsDead())
      {
        if (oppUnit == this) {
          BoardProxy.instance.GiveLowestCharLvl(this);
        } else {
          oppUnit.AddLevel();
        }
        return true;
      }
      return false;
    }

    void LostAegisAnim(float wait = 0){
      GainedAegisAnim(wait);
      AnimationInteractionController.InteractionAnimationGameobject(
        BoardProxy.instance.glossary.GetComponent<Glossary>().emoteAegisLost, gameObject, wait, true);
    }

    void GainedAegisAnim(float wait = 0){
      AnimationInteractionController.InteractionAnimationGameobject(
        BoardProxy.instance.glossary.GetComponent<Glossary>().emoteAegisGained, gameObject, wait, true);
    }

    void BideAnim(float wait = 0){
      AnimationInteractionController.InteractionAnimationGameobject(
        BoardProxy.instance.glossary.GetComponent<Glossary>().emoteBide, gameObject, wait, true);
    }

    void DmgAnim(float wait = 0){
      AnimationInteractionController.InteractionAnimationGameobject(
        BoardProxy.instance.glossary.GetComponent<Glossary>().emoteDmg, gameObject, wait, true);
    }

    void EnfeebleAnim(float wait = 0){
      AnimationInteractionController.InteractionAnimationGameobject(
        BoardProxy.instance.glossary.GetComponent<Glossary>().emoteEnfeeble, gameObject, wait, true);
    }

    void HealAnim(float wait = 0){
      AnimationInteractionController.InteractionAnimationGameobject(
        BoardProxy.instance.glossary.GetComponent<Glossary>().emoteHeal, gameObject, wait, true);
    }

    void HobbleAnim(float wait = 0){
      AnimationInteractionController.InteractionAnimationGameobject(
        BoardProxy.instance.glossary.GetComponent<Glossary>().emoteHobble, gameObject, wait, true);
    }

    void NullifyAnim(float wait = 0){
      AnimationInteractionController.InteractionAnimationGameobject(
        BoardProxy.instance.glossary.GetComponent<Glossary>().emoteNullify, gameObject, wait, true);
    }

    void QuickenAnim(float wait = 0){
      AnimationInteractionController.InteractionAnimationGameobject(
        BoardProxy.instance.glossary.GetComponent<Glossary>().emoteQuicken, gameObject, wait, true);
    }

    void RageAnim(float wait = 0){
      AnimationInteractionController.InteractionAnimationGameobject(
        BoardProxy.instance.glossary.GetComponent<Glossary>().emoteRage, gameObject, wait, true);
    }

    void RootedAnim(float wait = 0){
      AnimationInteractionController.InteractionAnimationGameobject(
        BoardProxy.instance.glossary.GetComponent<Glossary>().emoteRooted, gameObject, wait, true);
    }

    void SicklyAnim(float wait = 0){
      AnimationInteractionController.InteractionAnimationGameobject(
        BoardProxy.instance.glossary.GetComponent<Glossary>().emoteSickly, gameObject, wait, true);
    }

    public void SetNullified(bool nullified){
        _data.SetNullified(nullified);
        if (nullified) {
          NullifyAnim();
        }
    }

    public void SetQuickened(bool quickened){
        _data.GetTurnActions().SetMoves(_data.GetTurnActions().GetMoves() + 1);
        if (quickened) {
          QuickenAnim();
        }
    }

    public void SetRooted(int turns){
        _data.GetTurnActions().RootForTurns(turns);
        if (turns > 0) {
          RootedAnim();
        }
    }

    public void SetEnfeebled(int turns){
        _data.GetTurnActions().EnfeebledForTurns(turns);
        if (turns > 0) {
          EnfeebleAnim();
        }
    }

    public bool WaitedLastTurn(){
        return waited;
    }

    public void SetWaitedLastTurn(bool waited){
        this.waited = waited;
    }
    
    IEnumerator AttackAnim(UnitProxy oppUnit, Animator anim, Transform opp, Vector3Int diff, string msg, bool showProjectiles = true){
      if (anim != null && showProjectiles) {
          Vector3 theScale = opp.localScale;
          if (diff.x > 0) {
            //Debug.Log("right");
            theScale.x = -1;
            anim.SetBool("IDLE_FRONT_LEFT", false);
            while(anim.GetBool("IDLE_FRONT_LEFT")){ }
            anim.SetTrigger("ATK_BACK_LEFT");
          } else if (diff.x < 0) {
            //Debug.Log("left");
            theScale.x = 1;
            anim.SetBool("IDLE_FRONT_LEFT", true);
            while(!anim.GetBool("IDLE_FRONT_LEFT")){ }
            anim.SetTrigger("ATK_FRONT_LEFT");
          } else {
            //Defender is right below or above attacker
            if (diff.y > 0) {
              //Debug.Log("up");
              theScale.x = 1;
              anim.SetBool("IDLE_FRONT_LEFT", false);
              while(anim.GetBool("IDLE_FRONT_LEFT")){ }
              anim.SetTrigger("ATK_BACK_LEFT");
            } else if (diff.y < 0) {
              //Debug.Log("down");
              theScale.x = -1;
              anim.SetBool("IDLE_FRONT_LEFT", true);
              while(!anim.GetBool("IDLE_FRONT_LEFT")){ }
              anim.SetTrigger("ATK_FRONT_LEFT");
            }
          }
          opp.localScale = theScale;
      }
      Debug.Log("AttackAnim");
      FloatUp(Skill.Actions.DidAttack, msg, Color.red, "Was attacked", true);
      if (showProjectiles) {
          StartCoroutine(CreateProjectiles(oppUnit, opp.position));
      }
      yield return null;
    }

    IEnumerator CreateProjectiles(UnitProxy oppUnit, Vector3 dest){
        Vector3 start = dest;
        Vector3 finish = transform.position;

        start.y += .6f;
        finish.y += .6f;

        //GameObject baseProj = oppUnit.GetData().GetFactionType() == Unit.FactionType.None ? 
            //BoardProxy.instance.glossary.GetComponent<Glossary>().GetRandomGummi() : BoardProxy.instance.glossary.GetComponent<Glossary>().projectile;

        Unit.FactionType factionType = oppUnit.GetData().GetFactionType();
        Unit.UnitType unitType = oppUnit.GetData().GetUnitType();

        int num = 5;
        float delayBefore = .4f;
        float delayAfter = .1f;
        float projSpeed = 1;
        float chargeWait = 0;
        bool showProjectileAnimation = true;
        bool rotate = false;
        GameObject baseProj = BoardProxy.instance.glossary.GetComponent<Glossary>().projectile;
        TileProxy dTile = BoardProxy.instance.GetTileAtPosition(GetPosition());
        Color projColor = Color.white;

        float delayMiddleWait = 0;
        float xOffset = 0;
        float yOffset = 0;

        switch(factionType){
            case Unit.FactionType.Cthulhu:
              projColor= new Color(.4f,.2f,.6f);
              switch(unitType){
                case Unit.UnitType.Mage: xOffset += 0; yOffset += 0; num = 30; delayAfter = .005f; 
                  projSpeed = .5f; chargeWait = .001f; delayBefore = 0; baseProj = BoardProxy.instance.glossary.GetComponent<Glossary>().projectileSquare; break;
                case Unit.UnitType.Scout: showProjectileAnimation = false; break;
                case Unit.UnitType.Soldier: showProjectileAnimation = false; break;
              }
              break;
            case Unit.FactionType.Egypt:
              projColor=Color.yellow;
              switch(unitType){
                case Unit.UnitType.Mage: num = 20; delayAfter = .005f; projSpeed = .5f; chargeWait = .001f; delayBefore = .2f; break;
                case Unit.UnitType.Scout: num = 5; delayBefore = .1f; delayAfter = .02f; break;
                case Unit.UnitType.Soldier: projSpeed = .9f; rotate = true; num = 1; delayBefore = .45f; delayAfter = .3f; 
                  baseProj = BoardProxy.instance.glossary.GetComponent<Glossary>().scarab; break;
              }
              break;
            case Unit.FactionType.Human:
              projColor=Color.red;
              switch(unitType){
                case Unit.UnitType.Mage: num = 30; delayAfter = .01f; baseProj = BoardProxy.instance.glossary.GetComponent<Glossary>().projectileSquare; break;
                case Unit.UnitType.Scout: num = 2; delayBefore = .15f; delayAfter = .35f; projSpeed = .5f; break;
                case Unit.UnitType.Soldier: num = 1; delayBefore = .28f; delayAfter = .6f; break;
              }
              break;
            default:
              switch(unitType){
                case Unit.UnitType.Mage:
                  rotate = true; num = 1; delayBefore = .6f; projSpeed = .9f; baseProj = BoardProxy.instance.glossary.GetComponent<Glossary>().GetRandomGummi(); 
                break;
                //case Unit.UnitType.Scout:break;
                //case Unit.UnitType.Soldier:break;
                default: showProjectileAnimation = false; break;
              }
              break;
        }

        yield return new WaitForSeconds(delayBefore);
        for (int i = 0; i < num; i++) {
            if (showProjectileAnimation) {
              StartCoroutine(GenerateProjectile(factionType, unitType, dTile, baseProj, start, finish, projColor, rotate, chargeWait, xOffset, yOffset, projSpeed));
            } else {
              StartCoroutine(GenerateAttackAnims(oppUnit, baseProj, start, finish));
            }
            if (i == num / 2 && delayMiddleWait > 0) {
                yield return new WaitForSeconds(delayMiddleWait);
            }
            yield return new WaitForSeconds(delayAfter);
        }
        yield return null;
    }

    IEnumerator GenerateAttackAnims(UnitProxy oppUnit, GameObject baseProj, Vector3 start, Vector3 finish){
        TileProxy dTile = BoardProxy.instance.GetTileAtPosition(GetPosition());
        dTile.CreateAnimation(Glossary.GetAtkFx(oppUnit.GetData().GetFactionType(), oppUnit.GetData().GetUnitType()), AnimationInteractionController.NO_WAIT);
        yield return null;
    }

    IEnumerator GenerateProjectile(Unit.FactionType factionType, Unit.UnitType unitType, TileProxy destTile, GameObject baseProj, Vector3 start, Vector3 finish, Color projColor, bool rotate, float chargeWait, 
        float xOffset, float yOffset, float projSpeed){

        GameObject newProj = Instantiate(baseProj, start, Quaternion.identity);
        newProj.GetComponent<SpriteRenderer>().color = projColor;
        if (chargeWait > 0) {
            yield return new WaitForSeconds(chargeWait);
        }
        //yield return new WaitForSeconds(chargeWait);
        iTween.MoveTo(newProj, finish, projSpeed);
        if (rotate) {
            iTween.RotateBy(newProj, new Vector3(0,0,1), projSpeed);
        }
        yield return new WaitForSeconds(projSpeed - .1f);
        Destroy(newProj);
        destTile.CreateAnimation(Glossary.GetAtkFx(factionType, unitType), AnimationInteractionController.NO_WAIT);
        //Destroy(newProj);
    }

    public void SetAegis(bool aegis, float wait = 0){
        if (aegis) {
          _data.SetAegis(true);
          GainedAegisAnim(wait);
        } else {
          _data.SetAegis(false);
          LostAegisAnim(wait);
        }
    } 

    public bool IsAttackedEnvironment(int atkPwr, Skill.Actions act = Skill.Actions.None)
    {
      if (_data.GetAegis()) {
          SetAegis(false,AnimationInteractionController.ReturnWaitTime(act));
          return false;
      }

      //Damage the unit
      GetData().IsAttacked(atkPwr);
      //FloatUp("-" + atkPwr.ToString(), Color.red, ATK_WAIT);
      FloatUp(act, "-" + atkPwr.ToString(), Color.red, "Took env damage", true);
      if (GetData().IsDead() && !givenExp)
      {
        givenExp = true;
        BoardProxy.instance.GiveLowestCharLvl(this);
        return true;
      }
      return false;
    }

    public void ReceiveHPBuff(int buff){
        GetData().SetHpBuff(GetData().GetHpBuff() + buff);
        //int newHp = GetData().GetMaxHP() + buff;
        if (buff != 0) {
          if (buff > 0){
              BideAnim();
              //FloatUp(Skill.Actions.None, "+" + buff + " hp", Color.blue, "HP Buff", false);
          } else {
              SicklyAnim();
              //FloatUp(Skill.Actions.None, "-" + buff + " hp", Color.cyan, "HP Sickness", true);
          }
        }
    }

    public void ReceiveAtkBuff(int buff, Skill.Actions action = Skill.Actions.None){
        GetData().SetAttackBuff(GetData().GetAttackBuff() + buff);
        //int newAtk = GetData().GetAttack() + buff;
        if (buff != 0) {
          if (buff > 0){
              RageAnim(AnimationInteractionController.ReturnWaitTime(action));
              //FloatUp(action, "+" + buff + " atk", Color.blue, "atk buff", false);
          } else {
              HobbleAnim(AnimationInteractionController.ReturnWaitTime(action));
              //FloatUp(action, "-" + buff + " atk", Color.cyan, "atk sickness", true);
          }
        }
    }

    public void DelayedKill(UnitProxy obj, UnitProxy cUnit){
        StartCoroutine(DelayKill(obj, cUnit));
    }

    IEnumerator DelayKill(UnitProxy obj, UnitProxy cUnit){
        //Log the kill with the unit
        //cUnit.AddLevel();
        //Perform after kill skills
        cUnit.AcceptAction(Skill.Actions.DidKill,obj);

        //yield return new WaitForSeconds(UnitProxy.ATK_WAIT * 2);

        obj.FloatUp(Skill.Actions.DidKill, "Death", Color.red, "Killed Unit", false, true);

        //yield return new WaitForSeconds(UnitProxy.NO_ATK_WAIT);
        //Check the conditiontracker for game end
        //ConditionTracker.instance.EvalDeath(obj);                     
        //Turn off the tiles
        //StartCoroutine(ResetTiles());
        yield return null;
    }

    public void Shake(){
        StartCoroutine(ShakeChar());
    }

    IEnumerator ShakeChar()
    {
        iTween.ShakePosition(gameObject,new Vector3(.25f,0,0), .2f);
        yield return null;
    }

    public void Jump(){
        StartCoroutine(JumpChar());
    }

    IEnumerator JumpChar()
    {
        iTween.ShakePosition(gameObject,new Vector3(0,.25f,0), .2f);
        yield return null;
    }

    //public void FloatUp(string msg, Color color, float wait){
    //    StartCoroutine(FloatUpAnim(msg, color, wait));
    //}

    public void FloatUp(Skill.Actions interaction, string msg, Color color, string desc, bool shakeChar, bool deathConsideration = false){
        AnimationInteractionController.InteractionAnimation(interaction, this, msg, color, desc, shakeChar, deathConsideration);
    }

    public void SaySomething(Skill.Actions interaction){
        bool isSaying = UnityEngine.Random.Range(0,10) > 7;
        if (!isSaying) {
          return;
        }

        string dialog = UnitNameGenerator.GenerateRandomCatchphrase(interaction, GetData().GetFactionType());

        Color clr = Color.black;
        switch(interaction){
            case Skill.Actions.DidAttack: clr = Color.red;break;
            case Skill.Actions.DidKill: clr = Color.magenta;break;
        }

        AnimationInteractionController.InteractionDialogGameobject(interaction, BoardProxy.instance.GetTileAtPosition(GetPosition()), dialog, clr);
    }

    public void HealUnit(int value, Skill.Actions action){
       Debug.Log("Healing: " + GetData().characterMoniker + " for " + value.ToString());
       int nwHlth = GetData().GetCurrHealth() + value;
       nwHlth = nwHlth > GetData().GetMaxHP() ? GetData().GetMaxHP() : nwHlth;
       if (nwHlth != GetData().GetCurrHealth()) {
         GetData().SetCurrHealth(nwHlth);
         //FloatUp(action, "+" + value.ToString(), Color.green, "Unit Healed", false);
         HealAnim(AnimationInteractionController.ReturnWaitTime(action));
       }
    }

    public int GetMoveSpeed()
    {
        return _data.GetMoveSpeed();
    }

    public int GetAttackRange()
    {
        return _data.GetAtkRange();
    }

    public void PutData(Unit _data)
    {
      this._data = _data;
    }

    public Unit GetData()
    {
      return _data;
    }

    public void AcceptAction(Skill.Actions action, UnitProxy u1)
    {
      _data.AcceptAction(action, this, u1, null);
    }

    public void AcceptAction(Skill.Actions action, UnitProxy u1, List<TileProxy> path)
    {
      _data.AcceptAction(action, this, u1, path);
    }

    public virtual IEnumerator CreatePathToTileAndLerpToPosition(TileProxy destination, Action callback)
    {
        var currentTile = BoardProxy.instance.GetTileAtPosition(GetPosition());
        var path = BoardProxy.instance.GetPathAIConsideration(currentTile, destination, this);
        yield return StartCoroutine(SetPathAndLerpToEnd(path));
        if (callback != null){
            callback();
        } 
    }

    protected virtual IEnumerator SetPathAndLerpToEnd(Path<TileProxy> path)
    {
        yield return 0f;
        foreach (var t in path.Reverse())
        {
            yield return StartCoroutine(LerpToTile(t, MV_TIME, path.Reverse().First() == t));
        }
        Animator anim = transform.GetChild(0).GetComponent<Animator>();
        if (anim != null) {
            anim.SetBool("MV_BACK_LEFT", false);
            anim.SetBool("MV_FRONT_LEFT", false);
        }
        AcceptAction(Skill.Actions.DidMove, null, path.ToList());
        SnapToCurrentPosition();
    }

    public virtual IEnumerator LerpToTile(TileProxy tile, float time, bool firstTile = false)
    {
        Vector3 start = transform.position;
        Vector3 end = BoardProxy.GetWorldPosition(tile.GetPosition());
        /*
          Right here is where the animation needs to be set
        */
        Animator anim = transform.GetChild(0).GetComponent<Animator>();
        if (anim != null) {
          Vector3 theScale = transform.localScale;
          Vector3Int animStart = GetPosition();
          Vector3Int animEnd = tile.GetPosition();
          Vector3 diff = animEnd - animStart;

          if (diff.x > 0) {
            Debug.Log("right");
            theScale.x = -1;
            anim.SetBool("IDLE_FRONT_LEFT", false);
            while(anim.GetBool("IDLE_FRONT_LEFT")){ }
            anim.SetBool("MV_BACK_LEFT", true);
            anim.SetBool("MV_FRONT_LEFT", false);
          } else if (diff.x < 0) {
            Debug.Log("left");
            theScale.x = 1;
            anim.SetBool("IDLE_FRONT_LEFT", true);
            while(!anim.GetBool("IDLE_FRONT_LEFT")){ }
            anim.SetBool("MV_BACK_LEFT", false);
            anim.SetBool("MV_FRONT_LEFT", true);
          } else {
            //Defender is right below or above attacker
            if (diff.y > 0) {
              Debug.Log("up");
              theScale.x = 1;
              anim.SetBool("IDLE_FRONT_LEFT", false);
              while(anim.GetBool("IDLE_FRONT_LEFT")){ }
              anim.SetBool("MV_BACK_LEFT", true);
              anim.SetBool("MV_FRONT_LEFT", false);
            } else if (diff.y < 0) {
              Debug.Log("down");
              theScale.x = -1;
              anim.SetBool("IDLE_FRONT_LEFT", true);
              while(!anim.GetBool("IDLE_FRONT_LEFT")){ }
              anim.SetBool("MV_BACK_LEFT", false);
              anim.SetBool("MV_FRONT_LEFT", true);
            }
          }
          transform.localScale = theScale;
        }
        float timer = 0f;
        while (timer < time)
        {
            //            Debug.Log(transform.position);
            transform.position = Vector3.Lerp(start, end, timer / time);
            timer += Time.deltaTime;
            yield return 0f;
        }

        data.SetPosition(tile.GetPosition());
        if (!GetData().IsDead()) {
            tile.CreateAnimation(Glossary.fx.smoke1);
        }
        if (tile.OnFire() && !firstTile) {
            if (IsAttackedEnvironment(1)){
                transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
                transform.GetChild(0).GetComponent<Animator>().enabled = false;

                //ConditionTracker.instance.EvalDeath(this);
            }
        }
        //AnimationInteractionController.InteractionAnimationGameobject(
          //BoardProxy.instance.glossary.GetComponent<Glossary>().skull, BoardProxy.instance.GetTileAtPosition(GetPosition()).gameObject, AnimationInteractionController.NO_WAIT, true);
    }

    public void ZapToTile(TileProxy newTl, TileProxy oldTl, string actStr){  
        StartCoroutine(WaitForZap(newTl, oldTl, actStr));
    }

    IEnumerator WaitForZap(TileProxy newTl, TileProxy oldTl, string actStr){
        yield return new WaitForSeconds(AnimationInteractionController.ANIMATION_WAIT_TIME_LIMIT);

        newTl.ReceiveGridObjectProxy(this);
        //newTl.FloatUp(Skill.Actions.None, "whabam!", Color.blue, actStr);
        newTl.CreateAnimation(Glossary.fx.laser, 0);

        oldTl.RemoveGridObjectProxy(this);
        //oldTl.FloatUp(Skill.Actions.None, "poof", Color.cyan, actStr);
        oldTl.CreateAnimation(Glossary.fx.laser, 0);

        SetPosition(newTl.GetPosition());
        SnapToCurrentPosition();
    }

    float timeLeft = 3;

    void Update(){
        timeLeft -= Time.deltaTime;

        if (GetData() != null && GetData().GetAegis()) {
            aegisObj.SetActive(true);
        } else {
            if (aegisObj.activeInHierarchy) {
                StartCoroutine(PopAegis());
            }
        }
        SpriteRenderer rend = transform.GetChild(0).GetComponent<SpriteRenderer>();
        if (!GetData().GetTurnActions().CanMove() && !GetData().GetTurnActions().CanAttack()) {
            rend.color = new Color(1,.66f,.66f);
        } else {
            rend.color = new Color(1,1,1);
        }
        if (GetData().LowHP()) {
             if (timeLeft > 2.3f) {
                rend.color = new Color(1,.33f,.33f);
             }
        }
        if ( timeLeft < 0 )
        {
            timeLeft = 3;
        }
    }

    //IEnumerator LowHealthBlink(SpriteRenderer rend){
    //    Debug.Log("LowHealth Blink");
    //    rend.color = new Color(1,.33f,.33f);
    //    yield return new WaitForSeconds(.5f);
    //    //rend.color = new Color(1,1,1);
    //}

    IEnumerator PopAegis(){
        yield return new WaitForSeconds(AnimationInteractionController.ANIMATION_WAIT_TIME_LIMIT);
        aegisObj.SetActive(false);
    }

    public void CreateLaserHit(){
        StartCoroutine(CreateLaserAnim());
    }

    IEnumerator CreateLaserAnim(){
        yield return new WaitForSeconds(AnimationInteractionController.ATK_WAIT);
        Vector3 instPos = transform.position;
        instPos.y += .6f;
        GameObject smoke = Instantiate(BoardProxy.instance.glossary.GetComponent<Glossary>().fxLaser, instPos, Quaternion.identity);
        yield return new WaitForSeconds(1f);
        Destroy(smoke);
    }

    Vector2 ReturnIntersection(Vector3 left, Vector3 right, Vector3 up, Vector3 down){
        float A1 = right.y - left.y;
        float B1 = left.x - right.x;
        float C1 = A1 + B1;

        float A2 = right.y - left.y;
        float B2 = left.x - right.x;
        float C2 = A2 + B2;

        float delta = A1 * B2 - A2 * B1;
        
        if ((int) delta == 0) 
            return Vector2.zero;
        
        float x = (B2 * C1 - B1 * C2) / delta;
        float y = (A1 * C2 - A2 * C1) / delta;

        return new Vector2(x,y);
    }

    public void FocusOnUnit(){
        Camera.main.orthographicSize = 3;
        BoardProxy.instance.transform.position = new Vector3(1.5f,-1.5f,0);

        Vector2Int dims = BoardProxy.instance.GetDimensions();
        Vector3 bLeft = BoardProxy.instance.GetTileAtPosition(new Vector3Int(0,dims[1]-1,0)).transform.position;
        Vector3 bRight = BoardProxy.instance.GetTileAtPosition(new Vector3Int(dims[0]-1,0,0)).transform.position;
        Vector3 bUp = BoardProxy.instance.GetTileAtPosition(new Vector3Int(dims[0]-1,dims[1]-1,0)).transform.position;
        Vector3 bDown = BoardProxy.instance.GetTileAtPosition(new Vector3Int(0,0,0)).transform.position;
        Vector3 bCenter = ReturnIntersection(bLeft, bRight, bUp, bDown);

        Vector3 pos = transform.position;
        Vector3 diff = pos - bCenter;

        Debug.Log("bCenter pos: " + bCenter.ToString());
        Debug.Log("pos pos: " + pos.ToString());
        Debug.Log("diff pos: " + diff.ToString());

        bCenter.x -= diff.x;
        bCenter.y -= diff.y;

        BoardProxy.instance.transform.position = bCenter;
    }
}
