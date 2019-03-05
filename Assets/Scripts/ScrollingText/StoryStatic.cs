using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryStatic : MonoBehaviour
{
    public static string INTRO_STRING = "The day before Christmas, sweet 6 year old Susie writes a letter to Santa, asking for a box with a ballerina inside. She assures him she’s been good and patient with her baby brother. While Susie is sweet, she is not a great speller and addressed her letter to Satan, postmarking her letter with a shiny unicorn sticker before throwing it into the roaring fireplace, thinking it would go up the chimney where Santa could find it.\n" + 
      "Satan sat sprawled across a recliner made of skulls, sipping on cheap Chardonnay when sweet Susie’s letter appeared in the flames of his fireplace. He plucked it out from between two logs and saw an opportunity for mischief. He snapped his clawed fingers and a weasel-like demon appeared under hoof, Pandora’s Box clasped between it’s paws.\n" + 
      "By the time Satan arrived at the chimney of Susie’s house, Santa had come and gone, leaving shiny wrapped gifts waited under the sparkling fir in the living room, and taking a bite of the cookies laid out for him. Satan, unphased by Santa’s sloppy seconds, gobbled down the rest of the cookies, commenting on their mediocrity before wiping his hands and the corners of his mouth with the velvet curtain in the window and belching. Satan kicked aside Santa’s gifts, placing his own front and center. Satan fluffed the bow, and straightened the box before making his way back to the fireplace. Satan paused before stepping through the flames, pilfering candy from each hanging stocking and giggling to himself.\n" +
      "Susie sprang out of bed Christmas morning, her muscle memory kicking in before her eyes could open fully. Her feet thundered against the hardwood stairs. Her mother yelled exasperated and not yet awake, “Wait for me to get my camera!” But the sight of the box entrances Susie, she walked towards the ornate gold papered present, she touched the blood red bow, the traced the outline of her name, written in the fancy script her mother uses to sign permission slips, and unceremoniously destroyed the wrapping, uncovering the music box she had asked for. Susie lifted the lid, hoping that there would be a ballerina inside that had the same hair color as she did.\n";

    public static string HUMAN_INTRO = "With Susie possessed, and the northern hemisphere slowly falling under her control, the world has banded together to capture and control the Beast formerly known as Susie. Governments have entered treaties and locked themselves away to argue over how to use her powers for the betterment of their own countries, when suddenly, the Egyptian Gods of old re-enter the atmosphere.\n" +
      "If only we could get our hands on that box…";

    public static string[] HUMAN_DEATHS = new string[]{ 
      "The enemy has overtook your platoon and killed you and your friends.",
      "You were no match against the power of magic.",
      "Everybody in the world is dead now.",
    };

    public static string HUMAN_WIN = "With the demons and Gods defeated, our soldiers marched and closed Pandora’s box. The world soon divided again, fighting over the power of Pandora’s Box, until one day it was lost. Susie was sent to a Maximum Security Orphanage where she lived the rest of her days hating Christmas. ";

    public static string EGYPT_INTRO = "With Susie possessed, and the northern hemisphere slowly falling under her control, Ra feels his power and worship waning as the sun begins to shine on Susie. Unwilling to give up his reign over the world, Ra commands his followers to control the demon girl, for there may only be one race of Gods that rule this world.\n" +
      "If only we could destroy that box…";

    public static string[] EGYPT_DEATHS = new string[]{ 
      "Ra looks upon you in shame.",
      "Your light is no match for the darkness.",
      "You died and returned as an undead to kill your friends.",
    };

    public static string EGYPT_WIN = "With the demons defeated, and the mortals put back in their place, Ra destroyed Pandora’s Box and heralded the beginning of a thousand year era of world peace and Heliocentric Worship.";

    public static string CTHULHU_INTRO = "With Susie possessed, and the northern hemisphere slowly falling under her control, Ra feels his power and worship waning as the sun begins to shine on Susie. Unwilling to give up his reign over the world, Ra commands his followers to control the demon girl, for there may only be one race of Gods that rule this world.\n" +
      "If only we could destroy that box…";

    public static string[] CTHULHU_DEATHS = new string[]{ 
      "You suddenly find yourself back in hell. Cthulhu calls you a wuss and you are reincarnated.",
      "The Elder Gods made a mistake when they sent you...",
      "You’re just going to let a bunch of weaklings tell you what to do? I think not!",
    };

    public static string CTHULHU_WIN = "With the humans oppressed, and the Egyptian Gods defeated, Cthulhu moved into his mansion carved into the side of the mountains, overlooking Blood Lake. Demons ran rampant as darkness fell over the land. Let the fun begin...";

    public static string GetLevelStory(){
        PlayerMeta player = BaseSaver.GetPlayer();
        GameMeta.World world = player.world;
        Dests dest = (Dests)Enum.Parse(typeof(Dests), player.lastDest);
        Unit.FactionType faction = player.faction;
        switch(world){
          case GameMeta.World.nile: 
            switch(dest){
              case Dests.Dest1: return "Attention, recruit! Enemy contact! It’s some sort of Egyptian monster! Use all your ingenuity to take them down!";
              case Dests.Dest2: return "The enemy grows stronger as we near the center of the attack. Be on the lookout for more Egyptian scum!";
              case Dests.Dest3: return "Looks like these Gods don’t want us here! They’re sending us more fodder for our rifles! Keep track of your troops, we all have families at home.";
              case Dests.Dest4: return "The strength of our enemy is increasing! Be careful of fire and ice attacks, soldier!";
              case Dests.Dest5: return "The Egyptian menace has blocked our advance, let’s get them, soldier!";
              case Dests.Dest6: return "We can’t let these gods get to that box first! Take out these gods!";
              case Dests.Dest7: return "Their strength and numbers are increasing exponentially! Strategize, soldier!";
              case Dests.Dest8: return "We must be getting close to their home base, there’s even more vermin than ever!";
              case Dests.Dest9: return "They’re rallying against us! We can’t afford to lose now, soldier!";
              case Dests.Dest10: return "Here we are, recruit! The final battle! Just defeat the big guy and we’ll be able to pinpoint the exact location of the disturbance!";
            }
            break;
          case GameMeta.World.mountain: 
            switch(dest){
              case Dests.Dest1: return "On command from Isis, I have spotted the demonic interlopers. May her blessing put these intruders in their place.";
              case Dests.Dest2: return "With the power of Ra flowing through my veins, we may just win against these creatures from the deep.";
              case Dests.Dest3: return "Tentacles, wings, horns, and claws. These monsters were born in the deepest pits and there they shall return.";
              case Dests.Dest4: return "They do not deserve the blessed light from Ra. Send them to their deaths!";
              case Dests.Dest5: return "The abominations advance, we must stop them!";
              case Dests.Dest6: return "Beware the toxic stingers of poison and pray we survive the night.";
              case Dests.Dest7: return "Cthulhu’s minions grow stronger each day. We must not let them open the portal wider!";
              case Dests.Dest8: return "These sick demons appear to enjoy fighting. Let’s give them a taste of their own tonic!";
              case Dests.Dest9: return "Their dark magic is nothing against the power of Ra!";
              case Dests.Dest10: return "We have arrived! The final battle! Destroy the enemy!";
            }
            break;
          case GameMeta.World.pyramid: 
            switch(dest){
              case Dests.Dest1: return "On orders from the Elder Gods, I have set off to take care of the mortal scourge impeding our progress.";
              case Dests.Dest2: return "It seems the mortal pestilence is not as easy to take care of as we originally thought.";
              case Dests.Dest3: return "The human numbers are multiplying as we rally our forces. May the old ones bless us with darkness as we rid them of their souls.";
              case Dests.Dest4: return "These mortals send us infinite snacks, our demons will become fat and complacent if this continues.";
              case Dests.Dest5: return "Perhaps we have underestimated the human infestation. They are multiplying faster than we can reap them!";
              case Dests.Dest6: return "Hordes of enemies are approaching! Let’s show them true fear.";
              case Dests.Dest7: return "The smell of death and chaos pleases the Elder gods greatly, they demand more blood sacrifice.";
              case Dests.Dest8: return "The Elder gods demand a symphony of blood-curdling screams to be composed as a part of their epic opera. Pre-sale tickets available for a limited time at the box office on Level 3 of Hell.";
              case Dests.Dest9: return "With the influx of souls reaped, Hell is overflowing. We will need to kill more construction workers to expand the misery initiative.";
              case Dests.Dest10: return "This is it! The final battle! Once we get through these mortal fools, the box is ours and we can plunge the world into eternal darkness! Whee!";
            }
            break;
          case GameMeta.World.candy: 
            switch(faction){
              case Unit.FactionType.Human: 
                switch(dest){
                  case Dests.Dest1: return "Objective: Get our hands on Pandora’s Box.";
                  case Dests.Dest2: return "Do not get distracted by the chocolate falls and the cotton candy clouds! She’s bringing the dead back to life! Be careful, recruit!";
                  case Dests.Dest3: return "Enemies everywhere! You can’t be afraid now, soldier!";
                  case Dests.Dest4: return "Egyptian AND demonic enemies are closing in around us, soldier! We’ll have to scrap our way out of this one!";
                  case Dests.Dest5: return "We must be getting closer to Pandora, the enemies are getting stronger!";
                  case Dests.Dest6: return "We’re on the right track! The enemy is getting scared!";
                  case Dests.Dest7: return "No time for shell shock, soldier. You’ve made it this far, let’s see this to the end!";
                  case Dests.Dest8: return "The enemy is getting desperate to kill us, don’t give them the satisfaction!";
                  case Dests.Dest9: return "Can you feel that in the air? The end is near...";
                  case Dests.Dest10: return "It’s her. It’s Susie. Get that box closed and back to our government!";
                }
              break;
              case Unit.FactionType.Egypt: 
                switch(dest){
                  case Dests.Dest1: return "By the power of Anubis, I can feel that we are in the end game.";
                  case Dests.Dest2: return "The Greeks did not heed our warning, and now we must clean up their mess.";
                  case Dests.Dest3: return "The demoness appears to be bringing her enemies back from the dead!";
                  case Dests.Dest4: return "Do not fear, Ra shines his blessed light upon us. There can be only one true God!";
                  case Dests.Dest5: return "The demoness grows stronger with each passing day! We must not fail!";
                  case Dests.Dest6: return "There can be no others with magic. We must destroy her source of power, we must destroy the box!";
                  case Dests.Dest7: return "Do not fall prey to the grotesque illusions the she-beast creates!";
                  case Dests.Dest8: return "The dead grow stronger as we near the demoness!";
                  case Dests.Dest9: return "Can you feel that in the air? The end is near…";
                  case Dests.Dest10: return "It’s her. The little Demoness. We must destroy the box!";
                }
              break;
              case Unit.FactionType.Cthulhu: 
                switch(dest){
                  case Dests.Dest1: return "Protect the girl at all costs!";
                  case Dests.Dest2: return "Human Beings, Egyptian Magic Beings, they’re coming from everywhere!";
                  case Dests.Dest3: return "It hardly seems fair to have so much vermin ganging up on us.";
                  case Dests.Dest4: return "If we could only get that box opened a little wider, the big demons could fit through the portal.";
                  case Dests.Dest5: return "Wouldn’t it be fun to see this world fall to chaos?";
                  case Dests.Dest6: return "What are we all fighting for anyway? Can’t we just all go back to Blood Lake and bond over a nice chalice of human misery?";
                  case Dests.Dest7: return "Oh what a delicious mess we’ve made here!";
                  case Dests.Dest8: return "It seems we’ve angered the enemy!";
                  case Dests.Dest9: return "Can you feel that in the air? The end is near…";
                  case Dests.Dest10: return "It’s her. It’s Pandora. Protect her!";
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
