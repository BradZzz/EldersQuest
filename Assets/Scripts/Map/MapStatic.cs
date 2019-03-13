using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapStatic
{
    public static Dictionary<string, BoardMeta> ReturnTestBoardDests(bool tutorial)
    {
      Dictionary<string, BoardMeta> board = new Dictionary<string, BoardMeta>();
      board.Add("Dest1",new BoardMeta(6, 6, CreateFromExp(1, tutorial), new CondMeta[]{ new CondMeta() }, new string[]{ "Dest2" }));
      board.Add("Dest2",new BoardMeta(6, 6, CreateFromExp(2, tutorial), new CondMeta[]{ new CondMeta() }, new string[]{ "Dest3" }));
      board.Add("Dest3",new BoardMeta(8, 8, CreateFromExp(4, tutorial), new CondMeta[]{ new CondMeta() }, new string[]{ "Dest4" }));
      board.Add("Dest4",new BoardMeta(8, 8, CreateFromExp(6, tutorial), new CondMeta[]{ new CondMeta() }, new string[]{ "Dest5" }));
      board.Add("Dest5",new BoardMeta(9, 9, CreateFromExp(8, tutorial), new CondMeta[]{ new CondMeta() }, new string[]{ "Dest6" }));
      board.Add("Dest6",new BoardMeta(9, 9, CreateFromExp(10, tutorial), new CondMeta[]{ new CondMeta() }, new string[]{ "Dest7" }));
      board.Add("Dest7",new BoardMeta(10, 10, CreateFromExp(12, tutorial), new CondMeta[]{ new CondMeta() }, new string[]{ "Dest8" }));
      board.Add("Dest8",new BoardMeta(11, 11, CreateFromExp(14, tutorial), new CondMeta[]{ new CondMeta() }, new string[]{ "Dest9" }));
      board.Add("Dest9",new BoardMeta(12, 12, CreateFromExp(16, tutorial), new CondMeta[]{ new CondMeta() }, new string[]{ "Dest10" }));
      board.Add("Dest10",new BoardMeta(14, 14, CreateFromExp(20, tutorial), new CondMeta[]{ new CondMeta() }, new string[]{ "Dest11" }));
      return board;
    }

    static Unit[] CreateUnits(int units)
    {
        Unit[] unitAtt = new Unit[units];
        for (int i = 0; i < units; i++)
        {
            unitAtt[i] = new Unit();
        }
        return unitAtt;
    }

    static Unit[] CreateFromExp(int lvl, bool tutorial)
    {
        if (lvl > 3 && tutorial) {
            lvl = 3;
        } 

        PlayerMeta player = BaseSaver.GetPlayer();
        Unit.FactionType eFaction = Unit.FactionType.Egypt;
        switch(player.faction){
            case Unit.FactionType.Human: eFaction = Unit.FactionType.Egypt; break;
            case Unit.FactionType.Egypt: eFaction = Unit.FactionType.Cthulhu; break;
            case Unit.FactionType.Cthulhu: eFaction = Unit.FactionType.Human; break;
        }

        List<Unit> units = new List<Unit>();
        Array values = Enum.GetValues(typeof(Unit.UnitType));
        int max = 3 > lvl ? lvl : 3;
        int lvlCnt = lvl;
        for(int i = 0; i < max; i++) {
            int exp = UnityEngine.Random.Range(0,lvlCnt);
            Unit newUnit = Unit.BuildInitial(player.world != GameMeta.World.candy ? eFaction : (Unit.FactionType)UnityEngine.Random.Range(0, 3), 
              (Unit.UnitType)values.GetValue(UnityEngine.Random.Range(0,values.Length-1)), BoardProxy.ENEMY_TEAM);
            //Unit newUnit = Unit.BuildInitial(player.world != GameMeta.World.candy ? eFaction : (Unit.FactionType)UnityEngine.Random.Range(0, 3), 
              //Unit.UnitType.Mage, BoardProxy.ENEMY_TEAM);
            if (i == max - 1) {
                newUnit.SetLvl(lvlCnt);
            } else {
                newUnit.SetLvl(exp);
            }
            lvlCnt -= exp;
            if (tutorial) {
                newUnit.SetMaxHP(1);
                newUnit.SetSkills(new string[]{ });
            }
            units.Add(newUnit);
        }
        Debug.Log("Units: " + units.Count.ToString());
        if (units.Count < 3) {
            return units.ToArray();
        }
        /*
          Upgrade units here
        */
        for(int i = 0; i < units.Count; i++){
            while(units[i].GetLvl() >= units[i].GetCurrentClass().GetWhenToUpgrade() && units[i].GetCurrentClass().GetChildren().Length > 0) {
                int choice = UnityEngine.Random.Range(0,units[i].GetCurrentClass().GetChildren().Length);
                ClassNode pickedUpgrade = units[i].GetCurrentClass().GetChildren()[choice];
                units[i] = pickedUpgrade.UpgradeCharacter(units[i]);
                units[i].SetCurrentClass(pickedUpgrade.GetType().ToString());    
            }
        }
        return units.ToArray();
    }
}
