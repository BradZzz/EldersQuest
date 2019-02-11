using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapStatic
{
    public static Dictionary<string, BoardMeta> ReturnTestBoardDests()
    {
      Dictionary<string, BoardMeta> board = new Dictionary<string, BoardMeta>();
      board.Add("Dest1",new BoardMeta(6, 6, CreateUnits(1), new CondMeta[]{ new CondMeta() }, new string[]{ "Dest2" }));
      board.Add("Dest2",new BoardMeta(8, 8, CreateUnits(2), new CondMeta[]{ new CondMeta() }, new string[]{ "Dest3" }));
      board.Add("Dest3",new BoardMeta(10, 10, CreateUnits(3), new CondMeta[]{ new CondMeta() }, new string[]{ "Dest4" }));
      board.Add("Dest4",new BoardMeta(12, 12, CreateUnits(4), new CondMeta[]{ new CondMeta() }, new string[]{ "Dest5" }));
      board.Add("Dest5",new BoardMeta(14, 14, CreateUnits(5), new CondMeta[]{ new CondMeta() }, new string[]{ "Dest6" }));
      board.Add("Dest6",new BoardMeta(15, 15, CreateUnits(6), new CondMeta[]{ new CondMeta() }, new string[]{ "Dest7" }));
      board.Add("Dest7",new BoardMeta(16, 16, CreateUnits(7), new CondMeta[]{ new CondMeta() }, new string[]{ "Dest8" }));
      board.Add("Dest8",new BoardMeta(17, 17, CreateUnits(8), new CondMeta[]{ new CondMeta() }, new string[]{ "Dest9" }));
      board.Add("Dest9",new BoardMeta(18, 18, CreateUnits(9), new CondMeta[]{ new CondMeta() }, new string[]{ "Dest10" }));
      board.Add("Dest10",new BoardMeta(20, 20, CreateUnits(10), new CondMeta[]{ new CondMeta() }, new string[]{ "Dest11" }));
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
}
