using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Unit : GridObject
{
    public string characterName;
    public string characterMoniker;

    public int mxHlth = 1;

    public TurnActions turnActions;

    public UnitType uType;

    [SerializeField]
    private int cHlth = 1;
    [SerializeField]
    private int atk = 1;
    [SerializeField]
    private int moveSpeed = 3;
    [SerializeField]
    private int atkRange = 3;
    [SerializeField]
    private int lvl = 1;
    [SerializeField]
    private int team = 0;

    [SerializeField]
    private int trnMvs = 1;
    [SerializeField]
    private int trnAtks = 1;
    [SerializeField]
    private string[] skills;

    /* Skill Related Things */
    [SerializeField]
    private bool summoned;
    [SerializeField]
    private bool aegis;

    [SerializeField]
    private int atkBuff;
    [SerializeField]
    private int trnAtkBuff;
    [SerializeField]
    private int hpBuff;


    public enum UnitType
    {
        Mage, Scout, Soldier, None
    };

    public Unit()
    {
        this.characterName = "0";
        this.characterMoniker = "Null"; 
        this.uType = UnitType.Soldier;
        this.skills = new string[0];
        turnActions = new TurnActionsBasicUnit();
    }

    public Unit(string cName, string cMonik, int cLvl, int team, int mxHlth, int atk, int moveSpeed, int atkRange,
      int trnMvs, int trnAtks, string[] skills, UnitType uType = UnitType.Soldier)
    {
        Setup(cName + UnityEngine.Random.Range(0,1).ToString(),cMonik, cLvl, team, 
          mxHlth, atk, moveSpeed, atkRange, trnAtks, trnMvs, skills, uType);
    }

    public Unit(Unit unit)
    {
        Setup(unit.characterName, unit.characterMoniker, unit.GetLvl(), unit.team, 
          unit.mxHlth, unit.GetAttack(), unit.GetMoveSpeed(), unit.GetAtkRange(), 
        unit.trnAtks, unit.trnMvs, unit.GetSkills(),unit.uType);
    }

    void Setup(string cName, string cMonik, int cLvl, int team, int mxHlth, int atk, int moveSpeed, int atkRange,
      int trnMvs, int trnAtks, string[] skills, UnitType uType = UnitType.Soldier)
    {
        this.characterName = cName + UnityEngine.Random.Range(0,1).ToString();
        this.characterMoniker = cMonik;
        this.team = team;
        this.mxHlth = cHlth = mxHlth;
        this.atk = atk;
        this.moveSpeed = moveSpeed;
        this.atkRange = atkRange;
        this.lvl = cLvl;
        this.uType = uType;
        this.trnAtks = trnAtks;
        this.trnMvs = trnMvs;
        this.skills = skills;
        this.turnActions = new TurnActionsBasicUnit(trnMvs, trnAtks);
    }

    public Unit SummonedData(int team, int strength){
        return new Unit("Skele" + UnityEngine.Random.Range(0,float.MaxValue), "Summoned Skele", strength, team, strength, strength, 3, 2, 1, 1, new string[]{ });
    }
  
    public void Init()
    {
        cHlth = GetMaxHP();
    }

    public void BeginTurn(){
        SetTurnAttackBuff(0);
        GetTurnActions().BeginTurn();
    }

    public bool GetAegis(){
        return aegis;
    }

    public void SetAegis(bool aegis){
        this.aegis = aegis;
    }

    public int GetTurnAttackBuff(){
        return this.trnAtkBuff;
    }

    public void SetTurnAttackBuff(int buff){
        this.trnAtkBuff = buff;
    }

    public int GetAttackBuff(){
        return this.atkBuff;
    }

    public void SetAttackBuff(int buff){
        this.atkBuff = buff;
    }

    public int GetHpBuff(){
        return this.hpBuff;
    }

    public void SetHpBuff(int buff){
        this.hpBuff = buff;
        if (GetMaxHP() < GetCurrHealth()) {
            SetCurrHealth(GetMaxHP());
        }
    }

    public bool GetSummoned(){
        return summoned;
    }

    public void SetSummoned(bool summoned){
        this.summoned = summoned;
    }

    public int GetLvl()
    {
        return lvl;
    }
  
    public string[] GetSkills(){
        return skills;
    }

    public TurnActions GetTurnActions()
    {
        return turnActions;
    }

    public void IsAttacked(int atk)
    {
        cHlth -= atk;
    }

    public bool IsDead()
    {
        return cHlth <= 0;
    }

    public int GetMoveSpeed()
    {
        return moveSpeed;
    }

    public int GetCurrHealth()
    {
        return cHlth;
    }

    public void SetCurrHealth(int cHlth)
    {
        this.cHlth = cHlth;
    }

    public int GetMaxHP(){
        return mxHlth + hpBuff > 0 ? mxHlth + hpBuff : 1;
    }

    public int GetAttack()
    {
        int nwAtk = atk + GetAttackBuff() + GetTurnAttackBuff();
        return nwAtk > 0 ? nwAtk : 1;
    }

    public int GetAtkRange()
    {
        return atkRange;
    }

    public int GetTeam()
    {
        return team;
    }

    public void AcceptAction(Skill.Actions action, UnitProxy u1, UnitProxy u2, List<TileProxy> path)
    {
        Debug.Log("Cycling through skills: " + skills.Length.ToString());
        foreach(string skill in skills){
            Skill tSkill = Skill.ReturnSkillByString((Skill.SkillClasses)Enum.Parse(typeof(Skill.SkillClasses), skill));
            tSkill.value = 1;
            if (tSkill != null){
                tSkill.RouteBehavior(action, u1, u2, path);
            }
        }
    }

    /*
      Utility Functions
    */

    public static Unit BuildInitial(UnitType type, int team)
    {
        List<string> avoidNames = team == BoardProxy.PLAYER_TEAM ? 
          new List<string>(BaseSaver.GetPlayer().characters.Select(chr => chr.characterMoniker)) : 
          new List<string>();

        string uName = GenerateRandomName(avoidNames);
        switch (type)
        {
          case UnitType.Mage: 
            return new Unit(uName, uName, 1, team, 3, 2, 3, 4, 1, 1, new string[1]{ "SkeleKill" }, UnitType.Mage);
          case UnitType.Scout: 
            return new Unit(uName, uName, 1, team, 3, 1, 4, 3, 2, 1, new string[1]{ "SkeleKill" }, UnitType.Scout);
          case UnitType.Soldier: 
            return new Unit(uName, uName, 1, team, 4, 1, 3, 3, 1, 2, new string[1]{ "SkeleKill" }, UnitType.Soldier);
          default:
            return new Unit();
        }
    }

    public static string GetCharacterDesc(UnitType type)
    {
        switch (type)
        {
          case UnitType.Mage: 
            return "The Mage has: \n+1 atk pwr\n+1 atk rng";
          case UnitType.Scout: 
            return "The Scout has: \n+1 mv per turn\n+1 mv rng";
          case UnitType.Soldier: 
            return "The Soldier has: \n+1 atk per turn\n+1 hp";
          default:
            return "";
        }
    }
  
    public static string GenerateRandomName(List<string> dontPick)
    {
        string gotName = GetRandomName();
        while (dontPick.Contains(gotName))
        {
            gotName = GetRandomName();
        }
        return gotName;
    }
  
    static string GetRandomName()
    {
        string[] firsts = new string[]{ "Phil", "Marla", "Steve", "Gary", "Phil", "Cindy", "Reginald", "Herbert", "Alphonse", "Gloria", "Bertram", "Silvia", 
          "Natashia", "Bruce", "Silvio" };
        string[] lasts = new string[]{ "Hitshard", "Sweetcakes", "Robobot", "Chipcheeks", "Nitro", "Flavortown", "Killdoom", "Everyman", "Rocketshark", 
          "Looselips", "Karatease", "Danceswiftly", "Smoulderlust" };
        return firsts[UnityEngine.Random.Range(0, firsts.Length)] + " " + lasts[UnityEngine.Random.Range(0, lasts.Length)];
    }
}
