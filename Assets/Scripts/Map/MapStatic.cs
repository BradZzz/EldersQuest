using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapStatic
{
    public static Dictionary<string, BoardMeta> ReturnTestBoardDests()
    {
      Dictionary<string, BoardMeta> board = new Dictionary<string, BoardMeta>();
      board.Add("Dest1",new BoardMeta(6, 6, new Unit[]{ new Unit() }, new CondMeta[]{ new CondMeta() }, new string[]{ "Dest2" }));
      board.Add("Dest2",new BoardMeta(8, 8, new Unit[]{ new Unit(), new Unit() }, new CondMeta[]{ new CondMeta() }, new string[]{ "Dest3" }));
      board.Add("Dest3",new BoardMeta(10, 10, new Unit[]{ new Unit(), new Unit(), new Unit() }, new CondMeta[]{ new CondMeta() }, new string[]{ "Dest4" }));
      board.Add("Dest4",new BoardMeta(12, 12, new Unit[]{ new Unit(), new Unit(), new Unit(), new Unit() }, new CondMeta[]{ new CondMeta() }, new string[]{ "Dest5" }));
      board.Add("Dest5",new BoardMeta(14, 14, new Unit[]{ new Unit(), new Unit(), new Unit(), new Unit(), new Unit() }, new CondMeta[]{ new CondMeta() }, new string[]{ }));
      return board;
    }
}
