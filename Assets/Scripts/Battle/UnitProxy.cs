using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

    public bool IsAttacked(UnitProxy oppUnit)
    {
      if (_data.GetAegis()) {
          _data.SetAegis(false);
          FloatNumber(0, Color.blue);
          return false;
      }

      //Decrement the attacking units attack actions and update the ui
      oppUnit.GetData().GetTurnActions().Attack();
      PanelControllerNew.SwitchChar(oppUnit);

      //Damage the unit
      GetData().IsAttacked(oppUnit.GetData().GetAttack());
      Shake();
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
          FloatNumber(0, Color.blue);
          return false;
      }

      //Damage the unit
      GetData().IsAttacked(atkPwr);
      FloatNumber(atkPwr, Color.red);
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
              FloatNumber(buff, Color.blue);
          } else {
              FloatNumber(buff, Color.cyan);
          }
        }
    }

    public void ReceiveAtkBuff(int buff){
        GetData().SetAttackBuff(GetData().GetAttackBuff() + buff);
        //int newAtk = GetData().GetAttack() + buff;
        if (buff != 0) {
          if (buff > 0){
              FloatNumber(buff, Color.blue);
          } else {
              FloatNumber(buff, Color.cyan);
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

    public void FloatNumber(int num, Color color){
        Jump();
    }

    public void HealUnit(int value){
       int nwHlth = GetData().GetCurrHealth() + value;
       nwHlth = nwHlth > GetData().GetMaxHP() ? GetData().GetMaxHP() : nwHlth;
       if (nwHlth != GetData().GetCurrHealth()) {
         GetData().SetCurrHealth(nwHlth);
         FloatNumber(value, Color.green);
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
        AcceptAction(Skill.Actions.DidMove, null, path.ToList());
        SnapToCurrentPosition();

    }

    public virtual IEnumerator LerpToTile(TileProxy tile, float time)
    {
        Vector3 start = transform.position;
        Vector3 end = BoardProxy.GetWorldPosition(tile.GetPosition());
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
