using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glossary : MonoBehaviour
{
    public static bool PROD = false;

    public UnitProxy[] units;
    public ObstacleProxy[] obstacles;

    public UnitProxy humanSoldier;
    public UnitProxy humanScout;
    public UnitProxy humanMage;

    public UnitProxy egyptSoldier;
    public UnitProxy egyptScout;
    public UnitProxy egyptMage;

    public UnitProxy cthulhuSoldier;
    public UnitProxy cthulhuScout;
    public UnitProxy cthulhuMage;    
    public UnitProxy cthulhuWisp;

    public UnitProxy finalBlack;
    public UnitProxy finalRed;
    public UnitProxy finalBlue;

    public UnitProxy summonedSkeleton;

    //public GameObject Smoke;
    //public GameObject Laser;

    public GameObject fxBarrage;
    public GameObject fxBloodExplosion;
    public GameObject fxBloodSplatter;
    public GameObject fxEGExplosion;
    public GameObject fxFirePillar;
    public GameObject fxFireBaseLarge;
    public GameObject fxFireBaseSmall;
    public GameObject fxFireShield;
    public GameObject fxHealSmoke;
    public GameObject fxHMExplosion;
    public GameObject fxLaser;
    public GameObject fxLPExplosion;
    public GameObject fxSmoke1;
    public GameObject fxSmoke2;
    public GameObject fxSmoke3;
    public GameObject fxSnowSmoke;

    public UnitProxyEditor playerTile;
    public UnitProxyEditor enemyTile;
    public ObstacleProxyEdit obstacleEditTile;

    public Sprite grassTile;
    public Sprite grassTile2;
    public Sprite grassTile3;
    public Sprite grassTile4;

    public Sprite[] grassCamp1;
    public Sprite[] grassCamp2;
    public Sprite[] grassCamp3;
    public Sprite[] grassCamp4;

    public Sprite fireTile;
    public Sprite snowTile;
    public Sprite wallTile;
    public Sprite divineTile;

    public Sprite humanFaction;
    public Sprite egyptFaction;
    public Sprite chtulhuFaction;

    public Sprite battleMsgBubble;

    public Sprite endBattleWinOverlayHuman1;
    public Sprite endBattleWinOverlayHuman2;
    public Sprite endBattleWinOverlayEgypt1;
    public Sprite endBattleWinOverlayEgypt2;
    public Sprite endBattleWinOverlayCthulhu1;
    public Sprite endBattleWinOverlayCthulhu2;

    public Sprite endBattleOverlayHumanLeft;
    public Sprite endBattleOverlayEgyptLeft;
    public Sprite endBattleOverlayCthulhuLeft;
    public Sprite endBattleOverlaySusieLeft;

    public Sprite endBattleOverlayHumanRight;
    public Sprite endBattleOverlayEgyptRight;
    public Sprite endBattleOverlayCthulhuRight;
    public Sprite endBattleOverlaySusieRight;

    public Sprite endBattleBloodOverlay;

    public GameObject emoteAegisGained;
    public GameObject emoteAegisLost;
    public GameObject emoteBide;
    public GameObject emoteDmg;
    public GameObject emoteEnfeeble;
    public GameObject emoteHeal;
    public GameObject emoteHobble;
    public GameObject emoteNullify;
    public GameObject emoteQuicken;
    public GameObject emoteRage;
    public GameObject emoteRooted;
    public GameObject emoteSickly;
    
    public GameObject projectile;
    public GameObject projectileSquare;
    public GameObject exp;
    public GameObject scarab;
    public GameObject skull;

    public GameObject bearBlack;
    public GameObject bearBlue;
    public GameObject bearGreen;
    public GameObject bearOrange;
    public GameObject bearPurple;
    public GameObject bearRed;
    public GameObject bearYellow;

    public GameObject particlesHighlight;

    public Sprite[] ranks;

    public GameObject GetRandomGummi(){
        GameObject[] gummis = new GameObject[]{ bearBlack, bearBlue, bearGreen, bearOrange, bearPurple, bearRed, bearYellow };
        HelperScripts.Shuffle(gummis);
        return gummis[0];
    }

    public Sprite GetGrassTile(GameMeta.World wrld){
      Sprite[] gTiles;
      switch(wrld){
          case GameMeta.World.tutorial: return grassTile;
          case GameMeta.World.mountain: gTiles = grassCamp2; break;
          case GameMeta.World.pyramid: gTiles = grassCamp3; break;
          case GameMeta.World.candy: gTiles = grassCamp4; break;
          default: gTiles = grassCamp1; break;
      }
      HelperScripts.Shuffle(gTiles);
      return gTiles[0];
    }

    public static fx GetAtkFx(Unit.FactionType faction, Unit.UnitType unit){
        switch(faction){
            case Unit.FactionType.Cthulhu:return fx.bloodExplosions;
            case Unit.FactionType.Egypt:return fx.fireBaseLarge;
            case Unit.FactionType.Human:return fx.hmExplosion;
        }
        return fx.egExplosion;
    }

    public enum fx{
        barrage, bloodExplosions, bloodSplatter, egExplosion, firePillar, fireBaseLarge, fireBaseSmall, fireShield, healSmoke, 
        hmExplosion, laser, lpExplosion, smoke1, smoke2, smoke3, snowSmoke, none
    }
}
