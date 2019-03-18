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
    public Sprite fireTile;
    public Sprite snowTile;
    public Sprite wallTile;
    public Sprite divineTile;

    public Sprite humanFaction;
    public Sprite egyptFaction;
    public Sprite chtulhuFaction;

    public Sprite endBattleOverlayHumanLeft;
    public Sprite endBattleOverlayEgyptLeft;
    public Sprite endBattleOverlayCthulhuLeft;
    public Sprite endBattleOverlaySusieLeft;

    public Sprite endBattleOverlayHumanRight;
    public Sprite endBattleOverlayEgyptRight;
    public Sprite endBattleOverlayCthulhuRight;
    public Sprite endBattleOverlaySusieRight;

    public Sprite endBattleBloodOverlay;

    public GameObject projectile;
    public GameObject exp;

    public Sprite[] ranks;

    public static fx GetAtkFx(Unit.FactionType faction, Unit.UnitType unit){
        switch(faction){
            case Unit.FactionType.Cthulhu:return fx.bloodExplosions;
            case Unit.FactionType.Egypt:return fx.fireBaseLarge;
            case Unit.FactionType.Human:return fx.egExplosion;
        }
        return fx.barrage;
    }

    public enum fx{
        barrage, bloodExplosions, bloodSplatter, egExplosion, firePillar, fireBaseLarge, fireBaseSmall, fireShield, healSmoke, 
        hmExplosion, laser, lpExplosion, smoke1, smoke2, smoke3, snowSmoke, none
    }
}
