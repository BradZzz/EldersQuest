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
    public int cHlth = 1;
    public int atk = 1;
    public int moveSpeed = 3;
    public int atkRange = 3;
    public int lvl = 1;
    public int team = 0;

    public int trnMvs = 1;
    public int trnAtks = 1;

    public enum UnitType
    {
        Mage, Scout, Soldier, None
    };

    public Unit()
    {
        this.characterName = "0";
        this.characterMoniker = "Null"; 
        this.uType = UnitType.Soldier;
        turnActions = new TurnActionsBasicUnit();
    }

    public Unit(string cName, string cMonik, int cLvl, int team, int mxHlth, int atk, int moveSpeed, int atkRange,
      int trnMvs, int trnAtks, UnitType uType = UnitType.Soldier)
    {
        Setup(cName + UnityEngine.Random.Range(0,1).ToString(),cMonik, cLvl, team, 
          mxHlth, atk, moveSpeed, atkRange, trnAtks, trnMvs, uType);
    }

    public Unit(Unit unit)
    {
        Setup(unit.characterName, unit.characterMoniker, unit.GetLvl(), unit.team, 
          unit.mxHlth, unit.GetAttack(), unit.GetMoveSpeed(), unit.GetAtkRange(), unit.trnAtks, unit.trnMvs, unit.uType);
    }

    void Setup(string cName, string cMonik, int cLvl, int team, int mxHlth, int atk, int moveSpeed, int atkRange,
      int trnMvs, int trnAtks, UnitType uType = UnitType.Soldier)
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
        this.turnActions = new TurnActionsBasicUnit(trnMvs, trnAtks);
    }
  
    public void Init()
    {
        cHlth = mxHlth;
    }

    public int GetLvl()
    {
        return lvl;
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

    public int GetAttack()
    {
        return atk;
    }

    public int GetAtkRange()
    {
        return atkRange;
    }

    public int GetTeam()
    {
        return team;
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
            return new Unit(uName, uName, 1, team, 3, 2, 3, 4, 1, 1, UnitType.Mage);
          case UnitType.Scout: 
            return new Unit(uName, uName, 1, team, 3, 1, 4, 3, 2, 1, UnitType.Scout);
          case UnitType.Soldier: 
            return new Unit(uName, uName, 1, team, 4, 1, 3, 3, 1, 2, UnitType.Soldier);
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
