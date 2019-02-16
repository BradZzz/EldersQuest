using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapStatic
{
    public static Dictionary<string, BoardMeta> ReturnTestBoardDests()
    {
      Dictionary<string, BoardMeta> board = new Dictionary<string, BoardMeta>();
      board.Add("Dest1",new BoardMeta(6, 6, CreateFromExp(1), new CondMeta[]{ new CondMeta() }, new string[]{ "Dest2" }));
      board.Add("Dest2",new BoardMeta(8, 8, CreateFromExp(2), new CondMeta[]{ new CondMeta() }, new string[]{ "Dest3" }));
      board.Add("Dest3",new BoardMeta(10, 10, CreateFromExp(3), new CondMeta[]{ new CondMeta() }, new string[]{ "Dest4" }));
      board.Add("Dest4",new BoardMeta(10, 10, CreateFromExp(5), new CondMeta[]{ new CondMeta() }, new string[]{ "Dest5" }));
      board.Add("Dest5",new BoardMeta(12, 12, CreateFromExp(7), new CondMeta[]{ new CondMeta() }, new string[]{ "Dest6" }));
      board.Add("Dest6",new BoardMeta(12, 12, CreateFromExp(9), new CondMeta[]{ new CondMeta() }, new string[]{ "Dest7" }));
      board.Add("Dest7",new BoardMeta(12, 12, CreateFromExp(11), new CondMeta[]{ new CondMeta() }, new string[]{ "Dest8" }));
      board.Add("Dest8",new BoardMeta(14, 14, CreateFromExp(13), new CondMeta[]{ new CondMeta() }, new string[]{ "Dest9" }));
      board.Add("Dest9",new BoardMeta(14, 14, CreateFromExp(15), new CondMeta[]{ new CondMeta() }, new string[]{ "Dest10" }));
      board.Add("Dest10",new BoardMeta(16, 16, CreateFromExp(20), new CondMeta[]{ new CondMeta() }, new string[]{ "Dest11" }));
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

    static Unit[] CreateFromExp(int lvl)
    {
        List<Unit> units = new List<Unit>();
        Array values = Enum.GetValues(typeof(Unit.UnitType));
        for(int i = 0; i < 3 && i < lvl; i++) {
            int exp = UnityEngine.Random.Range(0,lvl);
            Unit newUnit = Unit.BuildInitial(Unit.FactionType.Egypt, 
              (Unit.UnitType)values.GetValue(UnityEngine.Random.Range(0,values.Length-1)), BoardProxy.ENEMY_TEAM);
            if (i == 2) {
                newUnit.SetLvl(lvl);
            } else {
                newUnit.SetLvl(exp);
            }
            lvl -= exp;
        }
        if (lvl < 3) {
            return units.ToArray();
        }
        /*
          Upgrade units here
        */
        for(int i = 0; i < units.Count; i++){
            ClassNode cNode = units[i].GetCurrentClass();
            while(units[i].GetLvl() >= cNode.GetWhenToUpgrade()) {
                int choice = UnityEngine.Random.Range(0,2);
                ClassNode pickedUpgrade = cNode.GetChildren()[choice];
                units[i] = pickedUpgrade.UpgradeCharacter(units[i]);
                units[i].SetCurrentClass(pickedUpgrade.GetType().ToString());
            }
        }
        return units.ToArray();
    }
}
