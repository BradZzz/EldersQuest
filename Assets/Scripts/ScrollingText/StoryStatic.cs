using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryStatic : MonoBehaviour
{
    public static string GetLevelStory(){
        PlayerMeta player = BaseSaver.GetPlayer();
        GameMeta.World world = player.world;
        Dests dest = (Dests)Enum.Parse(typeof(Dests), player.lastDest);
        Unit.FactionType faction = player.faction;
        switch(world){
          case GameMeta.World.nile: 
            switch(dest){
              case Dests.Dest1: return "Attention recruit! Enemy contact near the Doomed Bay! Use all your ingenuity to take him down!";
              case Dests.Dest2: return "The enemy grows stronger as we near the center of the attack. Be on the lookout for more egyptian scum!";
              case Dests.Dest3: return "It looks like these Gods really don't want us here! They're sending us more fodder for our rifles!";
              case Dests.Dest4: return "The strength of our enemy is increasing. Be careful for fire and ice attacks soldier!";
              case Dests.Dest5: return "The Egyptian menace has blocked our advance to the south. Let's get them soldiers!";
              case Dests.Dest6: return "";
              case Dests.Dest7: return "";
              case Dests.Dest8: return "";
              case Dests.Dest9: return "";
              case Dests.Dest10: return "Here it is recruit! The final battle! Just defeat the enemy here and we'll be able to pinpoint the location of the disturbance.";
            }
            break;
          case GameMeta.World.mountain: 
            switch(dest){
              case Dests.Dest1: return "On command from Isis, I have spotted the demonic interlopers. Hopefully her bestowed magic can put these intruders in their places.";
              case Dests.Dest2: return "As the humans advance on us from the south I fear we face a bigger threat from the ";
              case Dests.Dest3: return "";
              case Dests.Dest4: return "";
              case Dests.Dest5: return "";
              case Dests.Dest6: return "";
              case Dests.Dest7: return "";
              case Dests.Dest8: return "";
              case Dests.Dest9: return "";
              case Dests.Dest10: return "";
            }
            break;
          case GameMeta.World.pyramid: 
            switch(dest){
              case Dests.Dest1: return "On order from the elder gods I have set off to take care of the mortal scourge impeding our progress.";
              case Dests.Dest2: return "It seems as though the pestilence is not as easy to take care of as we originally thought.";
              case Dests.Dest3: return "The humans numbers are multiplying as we rally our forces. May the old ones curse us with darkness as we rid them of their souls.";
              case Dests.Dest4: return "";
              case Dests.Dest5: return "";
              case Dests.Dest6: return "";
              case Dests.Dest7: return "";
              case Dests.Dest8: return "";
              case Dests.Dest9: return "";
              case Dests.Dest10: return "";
            }
            break;
          case GameMeta.World.candy: 
            switch(faction){
              case Unit.FactionType.Human: 
                switch(dest){
                  case Dests.Dest1: return "";
                  case Dests.Dest2: return "";
                  case Dests.Dest3: return "";
                  case Dests.Dest4: return "";
                  case Dests.Dest5: return "";
                  case Dests.Dest6: return "";
                  case Dests.Dest7: return "";
                  case Dests.Dest8: return "";
                  case Dests.Dest9: return "";
                  case Dests.Dest10: return "";
                }
              break;
              case Unit.FactionType.Egypt: 
                switch(dest){
                  case Dests.Dest1: return "";
                  case Dests.Dest2: return "";
                  case Dests.Dest3: return "";
                  case Dests.Dest4: return "";
                  case Dests.Dest5: return "";
                  case Dests.Dest6: return "";
                  case Dests.Dest7: return "";
                  case Dests.Dest8: return "";
                  case Dests.Dest9: return "";
                  case Dests.Dest10: return "";
                }
              break;
              case Unit.FactionType.Cthulhu: 
                switch(dest){
                  case Dests.Dest1: return "";
                  case Dests.Dest2: return "";
                  case Dests.Dest3: return "";
                  case Dests.Dest4: return "";
                  case Dests.Dest5: return "";
                  case Dests.Dest6: return "";
                  case Dests.Dest7: return "";
                  case Dests.Dest8: return "";
                  case Dests.Dest9: return "";
                  case Dests.Dest10: return "";
                }
              break;
            }
            break;
        }
        return "";
    }

    public enum Dests{
      Dest1, Dest2, Dest3, Dest4, Dest5, Dest6, Dest7, Dest8, Dest9, Dest10, None
    }
}
