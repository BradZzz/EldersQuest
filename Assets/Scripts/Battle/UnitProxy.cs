using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UnitProxy : GridObjectProxy
{
    private Unit _data;
    protected override GridObject data
    {
        get { return _data; }
    }

    public override void OnSelected()
    {
        InteractivityManager.instance.OnUnitSelected(this);
    }


    public void Init()
    {
        if (_data == null)
            _data = new Unit();
        SnapToCurrentPosition();
    }

    public void AddLevel(){
      _data.SetLvl(_data.GetLvl()+1);
    }

    public bool IsAttacked(UnitProxy oppUnit, bool useAttack = true)
    {
      Debug.Log("IsAttacked");
      if (useAttack) {
          oppUnit.GetData().GetTurnActions().Attack();
      }
      PanelControllerNew.SwitchChar(oppUnit);

      if (_data.GetAegis()) {
          Debug.Log("Aegis!");
          _data.SetAegis(false);
          FloatUp("-aegis", Color.blue);
          return false;
      }
      Debug.Log("No Aegis");

      //Damage the unit
      /*
        Trigger the opponent's attack trigger here
      */
      Animator anim = oppUnit.gameObject.transform.GetChild(0).GetComponent<Animator>();
      if (anim != null) {
          if (anim.GetBool("IDLE_FRONT_LEFT")) {
              anim.SetTrigger("ATK_FRONT_LEFT");
          } else {
              anim.SetTrigger("ATK_BACK_LEFT");
          }
      }
      GetData().IsAttacked(oppUnit.GetData().GetAttack());
      Shake();
      FloatUp("-" + oppUnit.GetData().GetAttack().ToString(), Color.red);
      if (GetData().IsDead())
      {
        return true;
      }
      return false;
    }

    public bool IsAttackedEnvironment(int atkPwr)
    {
      if (_data.GetAegis()) {
          _data.SetAegis(false);
          FloatUp("-aegis", Color.blue);
          return false;
      }

      //Damage the unit
      GetData().IsAttacked(atkPwr);
      FloatUp("-" + atkPwr.ToString(), Color.red);
      if (GetData().IsDead())
      {
        return true;
      }
      return false;
    }

    public void ReceiveHPBuff(int buff){
        GetData().SetHpBuff(GetData().GetHpBuff() + buff);
        //int newHp = GetData().GetMaxHP() + buff;
        if (buff != 0) {
          if (buff > 0){
              FloatUp("+" + buff + " hp", Color.blue);
          } else {
              FloatUp("-" + buff + " hp", Color.cyan);
          }
        }
    }

    public void ReceiveAtkBuff(int buff){
        GetData().SetAttackBuff(GetData().GetAttackBuff() + buff);
        //int newAtk = GetData().GetAttack() + buff;
        if (buff != 0) {
          if (buff > 0){
              FloatUp("+" + buff + " atk", Color.blue);
          } else {
              FloatUp("-" + buff + " atk", Color.cyan);
          }
        }
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

    public void FloatUp(string msg, Color color){
        StartCoroutine(FloatUpAnim(msg, color));
    }

    IEnumerator FloatUpAnim(string msg, Color color)
    {
        Debug.Log("FloatUpAnim");
        Vector3 pos = this.transform.position;
        Debug.Log("FloatUpAnim pos: " + pos.ToString());
        pos.y += 1.3f;
        //GameObject numObj = Instantiate(new GameObject(), pos, Quaternion.identity, this.transform);
        //GameObject numObj = Instantiate(new GameObject(), this.transform, true);
        GameObject numObj = new GameObject();
        numObj.transform.position = pos;
        numObj.transform.rotation = Quaternion.identity;
        numObj.transform.parent = transform;
        numObj.AddComponent<TextMesh>();
        numObj.GetComponent<MeshRenderer>().sortingOrder = 20000;
        numObj.GetComponent<TextMesh>().characterSize = .2f;
        numObj.GetComponent<TextMesh>().text = msg;
        numObj.GetComponent<TextMesh>().color = color;
        iTween.ShakePosition(numObj,new Vector3(0,.25f,0), .5f);
        iTween.MoveTo(numObj,new Vector3(pos.x,pos.y + .2f,pos.z), .5f);
        yield return new WaitForSeconds(1f);
        Destroy(numObj);
        yield return null;
    }

    public void FloatString(string num, Color color){
        FloatUp(num, color);
    }

    public void HealUnit(int value){
       int nwHlth = GetData().GetCurrHealth() + value;
       nwHlth = nwHlth > GetData().GetMaxHP() ? GetData().GetMaxHP() : nwHlth;
       if (nwHlth != GetData().GetCurrHealth()) {
         GetData().SetCurrHealth(nwHlth);
         FloatUp("+" + value.ToString(), Color.green);
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
        var path = BoardProxy.instance.GetPath(currentTile, destination, this);
        yield return StartCoroutine(SetPathAndLerpToEnd(path));
        if (callback != null)
            callback();
    }

    protected virtual IEnumerator SetPathAndLerpToEnd(Path<TileProxy> path)
    {
        yield return 0f;
        foreach (var t in path.Reverse())
        {
            yield return StartCoroutine(LerpToTile(t, .15f));
        }
        Animator anim = transform.GetChild(0).GetComponent<Animator>();
        if (anim != null) {
            //if (anim.GetBool("MV_BACK_LEFT")) {
            //    anim.SetBool("IDLE_FRONT_LEFT", false);
            //} else {
            //    anim.SetBool("IDLE_FRONT_LEFT", true);
            //}
            anim.SetBool("MV_BACK_LEFT", false);
            anim.SetBool("MV_FRONT_LEFT", false);
        }
        AcceptAction(Skill.Actions.DidMove, null, path.ToList());
        SnapToCurrentPosition();
    }

    public virtual IEnumerator LerpToTile(TileProxy tile, float time)
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
  
          Debug.Log("animStart: " + animStart.ToString());
          Debug.Log("animEnd: " + animEnd.ToString());

          Vector3 diff = animEnd - animStart;
          float turnWait = .1f;

          Debug.Log("Diff: " + diff.ToString());

          if (diff.x > 0) {
            Debug.Log("right");
            //if ((int)theScale.x == 1) {
                theScale.x = -1;
            //    yield return new WaitForSeconds(turnWait);
            //}
            if (anim.GetBool("IDLE_FRONT_LEFT")) {
                anim.SetBool("IDLE_FRONT_LEFT", false);
                while(anim.GetBool("IDLE_FRONT_LEFT")){ }
                //yield return new WaitForSeconds(turnWait);
            }
            anim.SetBool("MV_BACK_LEFT", true);
            anim.SetBool("MV_FRONT_LEFT", false);
            //theScale.x = -1;
          } else if (diff.x < 0) {
            Debug.Log("left");
            //wrong up and to left
            //if ((int)theScale.x == -1) {
                theScale.x = 1;
            //    yield return new WaitForSeconds(turnWait);
            //}
            if (!anim.GetBool("IDLE_FRONT_LEFT")) {
                anim.SetBool("IDLE_FRONT_LEFT", true);
                while(!anim.GetBool("IDLE_FRONT_LEFT")){ }
                //yield return new WaitForSeconds(turnWait);
            }
            anim.SetBool("MV_BACK_LEFT", false);
            anim.SetBool("MV_FRONT_LEFT", true);
            //theScale.x = 1;
          } else {
            //Defender is right below or above attacker
            if (diff.y > 0) {
              Debug.Log("up");
              //if ((int)theScale.x == -1) {
                theScale.x = 1;
              //  yield return new WaitForSeconds(turnWait);
              //}
              //correct
              if (anim.GetBool("IDLE_FRONT_LEFT")) {
                  anim.SetBool("IDLE_FRONT_LEFT", false);
                  while(anim.GetBool("IDLE_FRONT_LEFT")){ }
                  //yield return new WaitForSeconds(turnWait);
              }
              anim.SetBool("MV_BACK_LEFT", true);
              anim.SetBool("MV_FRONT_LEFT", false);
              //theScale.x = 1;
            } else if (diff.y < 0) {
              Debug.Log("down");
              //if ((int)theScale.x == 1) {
                theScale.x = -1;
              //  yield return new WaitForSeconds(turnWait);
              //}
              //wrong. anim up and to the right
              if (!anim.GetBool("IDLE_FRONT_LEFT")) {
                  anim.SetBool("IDLE_FRONT_LEFT", true);
                  while(!anim.GetBool("IDLE_FRONT_LEFT")){ }
                  //yield return new WaitForSeconds(turnWait);
              }
              anim.SetBool("MV_BACK_LEFT", false);
              anim.SetBool("MV_FRONT_LEFT", true);
              //theScale.x = -1;
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

    }
}
