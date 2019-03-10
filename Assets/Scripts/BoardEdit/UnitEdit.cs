using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class UnitEdit : GridObjectEdit
{
    public string characterName;
    public string characterMoniker;

    public int mxHlth = 3;

    public TurnActions turnActions;

    [SerializeField]
    private UnitType uType;
    [SerializeField]
    private FactionType fType;

    [SerializeField]
    private int cHlth = 3;
    [SerializeField]
    private int atk = 1;
    [SerializeField]
    private int moveSpeed = 3;
    [SerializeField]
    private int atkRange = 2;
    [SerializeField]
    private int lvl = 0;
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
    private bool nullified;

    [SerializeField]
    private int atkBuff;
    [SerializeField]
    private int trnAtkBuff;
    [SerializeField]
    private int hpBuff;
    [SerializeField]
    private string currentClass;


    public enum UnitType
    {
        Mage, Scout, Soldier, None
    };

    public enum FactionType
    {
        Human, Egypt, Cthulhu, None
    };

    public static string GetFactionDesc(FactionType faction){
        switch(faction){
            case FactionType.Human: return "Balanced";
            case FactionType.Egypt: return "Tactics";
            case FactionType.Cthulhu: return "Magic and Abilities";
            default: return "";
        }
    }

    public UnitEdit()
    {
        currentClass = "HumanBaseMage";
        characterName = "0";
        characterMoniker = "Null"; 
        uType = UnitType.Soldier;
        skills = new string[0];
        turnActions = new TurnActionsBasicUnit();
    }

    public UnitEdit(string cName, string cMonik, int cLvl, int team, int mxHlth, int atk, int moveSpeed, int atkRange,
      int trnMvs, int trnAtks, string[] skills, string currentClass, UnitType uType = UnitType.Soldier, FactionType fType = FactionType.Human)
    {
        Setup(cName + UnityEngine.Random.Range(0,1).ToString(),cMonik, cLvl, team, 
          mxHlth, atk, moveSpeed, atkRange, trnMvs, trnAtks, skills, currentClass, uType, fType);
    }

    public UnitEdit(UnitEdit unit)
    {
        Setup(unit.characterName, unit.characterMoniker, unit.GetLvl(), unit.team, 
          unit.mxHlth, unit.GetBaseAttack(), unit.GetMoveSpeed(), unit.GetAtkRange(), 
        unit.trnMvs, unit.trnAtks, unit.GetSkills(), unit.currentClass, unit.uType, unit.fType);
    }

    void Setup(string cName, string cMonik, int cLvl, int team, int mxHlth, int atk, int moveSpeed, int atkRange,
      int trnMvs, int trnAtks, string[] skills, string currentClass, UnitType uType = UnitType.Soldier, FactionType fType = FactionType.Human)
    {
        this.characterName = cName + UnityEngine.Random.Range(0,1).ToString();
        this.characterMoniker = cMonik;
        this.team = team;
        this.mxHlth = cHlth = mxHlth;
        this.atk = atk;
        this.moveSpeed = moveSpeed;
        this.atkRange = atkRange;
        this.lvl = cLvl;
        this.currentClass = currentClass;
        this.uType = uType;
        this.fType = fType;
        this.trnAtks = trnAtks;
        this.trnMvs = trnMvs;
        this.skills = skills;
        this.turnActions = new TurnActionsBasicUnit(trnMvs, trnAtks);
    }

    public int GetTurnAttacks(){
        return trnAtks;
    }

    public void SetTurnAttacks(int trnAtks){
        this.trnAtks = trnAtks;
    }

    public int GetTurnMoves(){
        return trnMvs;
    }

    public void SetTurnMoves(int trnMvs){
        this.trnMvs = trnMvs;
    }

    public UnitEdit SummonedData(int team, int strength){
        return new UnitEdit("Skele" + UnityEngine.Random.Range(0,float.MaxValue), "Summoned Skele", 
          strength, team, strength, strength, 3, 2, 1, 1, new string[]{ }, "");
    }
  
    public void Init()
    {
        cHlth = GetMaxHP();
    }

    public void BeginTurn(){
        SetTurnAttackBuff(0);
        GetTurnActions().BeginTurn();
    }

    public UnitType GetUnitType(){
        return uType;
    }

    public FactionType GetFactionType(){
        return fType;
    }

    public void SetCurrentClass(string currentClass){
        this.currentClass = currentClass;
    }

    public ClassNode GetCurrentClass(){
        if (currentClass.Length == 0) {
            return null;
        }
        return StaticClassRef.GetClass((StaticClassRef.AvailableClasses)Enum.Parse(typeof(StaticClassRef.AvailableClasses), currentClass));
    }

    public string GetCurrentClassString(){
        return currentClass;
    }

    public bool GetAegis(){
        return aegis;
    }

    public void SetAegis(bool aegis){
        this.aegis = aegis;
    }

    public bool GetNullified(){
        return nullified;
    }

    public void SetNullified(bool nullified){
        this.nullified = nullified;
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

    public void SetAttack(int atk){
        this.atk = atk;
    }

    public void SetMoveSpeed(int moveSpeed){
        this.moveSpeed = moveSpeed;
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

    public void SetLvl(int lvl)
    {
        Debug.Log("Adding exp to : " + characterMoniker);
        this.lvl = lvl;
    }
  
    public string[] GetSkills(){
        return nullified ? new string[]{ }  : skills;
    }

    public void SetSkills(string[] skills){
        this.skills = skills;
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

    public int GetUnitClassRank(){
      int rnk = 0;
      ClassNode clss = GetCurrentClass();
      while(clss.GetParent() != null){
        clss = clss.GetParent();
        rnk ++;
      }
      return rnk;
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
        this.cHlth = cHlth > mxHlth ? mxHlth : cHlth;
    }

    public int GetMaxHP(){
        return mxHlth + hpBuff > 0 ? mxHlth + hpBuff : 1;
    }

    public void SetMaxHP(int mxHlth){
        this.mxHlth = this.cHlth = mxHlth;
    }

    public int GetBaseAttack()
    {
        return atk;
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

    public void SetAtkRange(int atkRange)
    {
        this.atkRange = atkRange;
    }

    public int GetTeam()
    {
        return team;
    }

    public void SetTeam(int team)
    {
        this.team = team;
    }

    //public void AcceptAction(Skill.Actions action, UnitProxy u1, UnitProxy u2, List<TileProxy> path)
    //{
    //    //Debug.Log("Cycling through skills: " + GetSkills().Length.ToString());
    //    var gSkills = GetSkills().GroupBy(skill => skill);
    //    foreach(var skill in gSkills){
    //        Skill tSkill = Skill.ReturnSkillByString((Skill.SkillClasses)Enum.Parse(typeof(Skill.SkillClasses), skill.Key));
    //        tSkill.value = skill.Count();
    //        if (tSkill != null){
    //            tSkill.RouteBehavior(action, u1, u2, path);
    //        }
    //    }
    //}

    public string GetSkillDescription(){
        string rStr = "";
        var gSkills = GetSkills().GroupBy(skill => skill);
        foreach(var skill in gSkills){
            rStr += "(" + skill.Count().ToString() + ")" + skill.Key + "\n";
        }
        return rStr;
    }

    //public static Skill GetSkillByName(string skill){
    //    return Skill.ReturnSkillByString((Skill.SkillClasses)Enum.Parse(typeof(Skill.SkillClasses), skill));
    //}

    /*
      Utility Functions
    */

    //public static UnitEdit BuildInitial(FactionType fType, UnitType uType, int team)
    //{
    //    List<string> avoidNames = team == BoardProxy.PLAYER_TEAM ? 
    //      new List<string>(BaseSaver.GetPlayer().characters.Select(chr => chr.characterMoniker)) : 
    //      new List<string>();

    //    string uName = UnitNameGenerator.GenerateRandomName(avoidNames, fType);

    //    UnitEdit bUnit = new Unit(uName, uName, 0, team, 3, 1, 3, 2, 1, 1, new string[]{ }, "", uType, fType);
    //    ClassNode classObj = ClassNode.ComputeClassObject(fType,uType);
    //    bUnit = classObj.UpgradeCharacter(bUnit);
    //    bUnit.SetCurrentClass(classObj.GetType().Name);
    //    return bUnit;
    //}

    //public static string GetCharacterDesc(UnitType type)
    //{
    //    switch (type)
    //    {
    //      case UnitType.Mage: 
    //        return "The Mage has: \n+1 atk pwr\n+1 atk rng";
    //      case UnitType.Scout: 
    //        return "The Scout has: \n+1 mv per turn\n+1 mv rng";
    //      case UnitType.Soldier: 
    //        return "The Soldier has: \n+1 atk per turn\n+1 hp";
    //      default:
    //        return "";
    //    }
    //}
  
    //public static string GenerateRandomName(List<string> dontPick)
    //{
    //    string gotName = GetRandomName();
    //    while (dontPick.Contains(gotName))
    //    {
    //        gotName = GetRandomName();
    //    }
    //    return gotName;
    //}
  
    //static string GetRandomName()
    //{
    //    string[] firsts = new string[]{ "Phil", "Marla", "Steve", "Gary", "Phil", "Cindy", "Reginald", "Herbert", "Alphonse", "Gloria", "Bertram", "Silvia", 
    //      "Natashia", "Bruce", "Silvio", "Paula", "Chris", "Olivia", "Byron", "Audrey", "Brier" };
    //    string[] lasts = new string[]{ "Hitshard", "Sweetcakes", "Robobot", "Chipcheeks", "Nitro", "Flavortown", "Killdoom", "Everyman", "Rocketshark", 
    //      "Looselips", "Karatease", "Danceswiftly", "Smoulderlust", "Vandersmoot", "Judosmith", "Eagletigerbear", "Fancypants", "Fancycheeks", "Doughnutface" };
    //    return firsts[UnityEngine.Random.Range(0, firsts.Length)] + " " + lasts[UnityEngine.Random.Range(0, lasts.Length)];
    //}

    public string ToString(){
        return characterMoniker + " : " + GetLvl();
    }
}
