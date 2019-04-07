using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public abstract class Skill
{
    public int value;

    public abstract void RouteBehavior(Actions action, UnitProxy u1, UnitProxy u2, List<TileProxy> path);
    //public abstract void RouteBehavior(Actions action, UnitProxy u1, UnitProxy u2, List<TileProxy> path);
  
    //Implemented
    public abstract void BeginningGame(UnitProxy unit);
    public abstract void EndTurn(UnitProxy unit);
    public abstract void DidMove(UnitProxy unit, List<TileProxy> path);
    public abstract void DidWait(UnitProxy unit);
    public abstract void DidAttack(UnitProxy attacker, UnitProxy defender);
    public abstract void DidKill(UnitProxy attacker, UnitProxy defender);

    public abstract string PrintDetails();
    public abstract string PrintStackDetails();
    public abstract SkillGen GetSkillGen();
    public abstract SkillTypes[] GetSkillTypes();

    public static Skill ReturnSkillByString(SkillClasses sClass){
        switch(sClass){
            case SkillClasses.AegisAlliesAtk: return new AegisAlliesAtk();
            case SkillClasses.AegisAtk: return new AegisAtk();
            case SkillClasses.AegisBegin: return new AegisBegin();
            case SkillClasses.AegisKill: return new AegisKill();
            case SkillClasses.AegisTurn: return new AegisTurn();
            case SkillClasses.AegisWait: return new AegisWait();
            case SkillClasses.AoeAtk: return new AoeAtk();
            case SkillClasses.BideAlliesAtk: return new BideAlliesAtk();
            case SkillClasses.BideAlliesWait: return new BideAlliesWait();
            case SkillClasses.BideKill: return new BideKill();
            case SkillClasses.BideWait: return new BideWait();
            case SkillClasses.DivineMove: return new DivineMove();
            case SkillClasses.EnfeebleAtk: return new EnfeebleAtk();
            case SkillClasses.EnfeebleEnemiesWait: return new EnfeebleEnemiesWait();
            case SkillClasses.FireAtk: return new FireAtk();
            case SkillClasses.FireDef: return new FireDef();
            case SkillClasses.FireKill: return new FireKill();
            case SkillClasses.FireMove: return new FireMove();
            case SkillClasses.ForceAtk: return new ForceAtk();  
            case SkillClasses.HealAlliesAtk: return new HealAlliesAtk(); 
            case SkillClasses.HealAlliesWait: return new HealAlliesWait(); 
            case SkillClasses.HealAtk: return new HealAtk(); 
            case SkillClasses.HealKill: return new HealKill();
            case SkillClasses.HealTurn: return new HealTurn();
            case SkillClasses.HealWait: return new HealWait();
            case SkillClasses.HobbleAtk: return new HobbleAtk();
            case SkillClasses.NullifyAtk: return new NullifyAtk();
            case SkillClasses.NullifyEnemiesWait: return new NullifyEnemiesWait();
            case SkillClasses.QuickenAlliesAtk: return new QuickenAlliesAtk();
            case SkillClasses.QuickenAlliesWait: return new QuickenAlliesWait();
            case SkillClasses.RageAlliesWait: return new RageAlliesWait();
            case SkillClasses.RageAtk: return new RageAtk();
            case SkillClasses.RageDef: return new RageDef();
            case SkillClasses.RageKill: return new RageKill();
            case SkillClasses.RageMove: return new RageMove();
            case SkillClasses.RageWait: return new RageWait();
            case SkillClasses.RootAtk: return new RootAtk();
            case SkillClasses.RootEnemiesWait: return new RootEnemiesWait();
            case SkillClasses.SicklyAtk: return new SicklyAtk();
            case SkillClasses.WispKill: return new WispKill();
            case SkillClasses.SnowAtk: return new SnowAtk();
            case SkillClasses.SnowMove: return new SnowMove();
            case SkillClasses.ThornDef: return new ThornDef();       
            case SkillClasses.VoidAtk: return new VoidAtk();  
            case SkillClasses.WallMove: return new WallMove();
            case SkillClasses.WarpAtk: return new WarpAtk();       
            default: return null;
        }
    }

    public static string ReturnBlurbByString(SkillGen sGen){
        switch(sGen){
            case SkillGen.Aegis:return "An (aegis) shield blocks a unit's next incoming attack.";
            case SkillGen.Aoe:return "An (aoe) skill deals extra damage in a range. Other skills still only effect main unit targeted.";
            case SkillGen.Bide:return "(Bide) raises the max health of effected units.";
            case SkillGen.Divine:return "A tile with (divine) will heal a unit for +1 at the end of the turn.";
            case SkillGen.Enfeeble:return "An (enfeebled) unit has 1 less attack on it's next turn.";
            case SkillGen.Fire:return "A tile on (fire) burns any unit that walks through it for 1 dmg.";
            case SkillGen.Force:return "An ability with (force) pushes an enemy away from the unit.";
            case SkillGen.Heal:return "An ability with (heal) restores a unit's lost hit points.";
            case SkillGen.Hobble:return "(Hobble) lowers the attack of effected units down to 1.";
            case SkillGen.Nullify:return "(Nullify) prevents a unit's skills from activating for a turn.";
            case SkillGen.Quicken:return "(Quicken) gives a unit another movement this turn.";
            case SkillGen.Rage:return "(Rage) raises the attack of effected units.";
            case SkillGen.Root:return "A (root)ed unit loses 1 turn movement on their next turn.";
            case SkillGen.Sickly:return "(Sickly) lowers the max hp of effected units down to 1.";
            case SkillGen.WispKill:return "A (wisp) ability summons a friendly wisp unit when used.";
            case SkillGen.Snow:return "A tile with (snow) costs 2 movement points to walk through instead of 1.";
            case SkillGen.Thorn:return "(Thorn) damages enemy units in range by 1 when activated.";
            case SkillGen.Void:return "An ability with (void) pulls an enemy towards the unit.";
            case SkillGen.Wait:return "A unit successfully (wait)s when they end the turn with at least one attack and move left.";
            case SkillGen.Wall:return "A (tile) with wall will spawn an obstacle.";
            case SkillGen.Warp:return "An ability with (warp) teleports an enemy to a random open space away from the unit.";
            default: return "";
        }
    }

    public static string ReturnStackTypeByString(SkillStack sGen){
        switch(sGen){
            case SkillStack.rng:return "Multiple stacks increase range of effect by 1.";
            case SkillStack.buff:return "Multiple stacks increase buff by 1.";
            case SkillStack.move:return "Multiple stacks increase tile effect turns by 1.";
            case SkillStack.skele:return "Multiple stacks increase wisp hp and atk by 1.";
            case SkillStack.turns:return "Multiple stacks increase turns of effect.";
            case SkillStack.nostack:return "Effect does not stack.";
            default: return "";
        }
    }

    public static Animator ReturnSkillAnimation(SkillGen sGen, Glossary glossy){
        switch(sGen){
            case SkillGen.Aoe:return glossy.fxBarrage.GetComponent<Animator>();
            case SkillGen.Fire:return glossy.fxFirePillar.GetComponent<Animator>();
            case SkillGen.Force:return glossy.fxLaser.GetComponent<Animator>();
            case SkillGen.Heal:return glossy.fxHealSmoke.GetComponent<Animator>();
            case SkillGen.WispKill:return glossy.cthulhuWisp.transform.GetChild(0).GetComponent<Animator>();
            case SkillGen.Thorn:return glossy.fxBloodExplosion.GetComponent<Animator>();
            case SkillGen.Void:return glossy.fxLaser.GetComponent<Animator>();
            case SkillGen.Warp:return glossy.fxLaser.GetComponent<Animator>();
            default: return null;
        }
    }

    //public static UIGlossary.uiFX ReturnSkillUIGlossaryMapping(SkillGen sGen, Glossary glossy){
    //    switch(sGen){
    //        case SkillGen.Aoe:return UIGlossary.uiFX.fxAoe;
    //        case SkillGen.Fire:return UIGlossary.uiFX.fxAoe;
    //        case SkillGen.Force:return UIGlossary.uiFX.fxAoe;
    //        case SkillGen.Heal:return UIGlossary.uiFX.fxAoe;
    //        case SkillGen.WispKill:return UIGlossary.uiFX.fxAoe;
    //        case SkillGen.Thorn:return glossy.fxBloodExplosion.GetComponent<Animator>();
    //        case SkillGen.Void:return glossy.fxLaser.GetComponent<Animator>();
    //        case SkillGen.Warp:return glossy.fxLaser.GetComponent<Animator>();
    //        default: return null;
    //    }
    //}

    public static Sprite ReturnSkillSprite(SkillGen sGen, Glossary glossy){
        switch(sGen){
            case SkillGen.Aegis:return glossy.emoteAegisGained.GetComponent<SpriteRenderer>().sprite;
            case SkillGen.Bide:return glossy.emoteBide.GetComponent<SpriteRenderer>().sprite;
            case SkillGen.Divine:return glossy.divineTile;
            case SkillGen.Enfeeble:return glossy.emoteEnfeeble.GetComponent<SpriteRenderer>().sprite;
            case SkillGen.Hobble:return glossy.emoteHobble.GetComponent<SpriteRenderer>().sprite;
            case SkillGen.Nullify:return glossy.emoteNullify.GetComponent<SpriteRenderer>().sprite;
            case SkillGen.Quicken:return glossy.emoteQuicken.GetComponent<SpriteRenderer>().sprite;
            case SkillGen.Rage:return glossy.emoteRage.GetComponent<SpriteRenderer>().sprite;
            case SkillGen.Root:return glossy.emoteRooted.GetComponent<SpriteRenderer>().sprite;
            case SkillGen.Sickly:return glossy.emoteSickly.GetComponent<SpriteRenderer>().sprite;
            case SkillGen.Snow:return glossy.snowTile;
            case SkillGen.Wall:return glossy.obstacles[0].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
            default: return null;
        }
    }

    public enum Actions{
        BeginGame, EndedTurn, DidAttack, DidMove, DidWait, DidKill, DidDefend, None
    }

    public enum SkillGen{
        Aegis, Aoe, Bide, Divine, Enfeeble, Fire, Force, Heal, Hobble, Nullify, Quicken, Rage, Root, Sickly, WispKill, Snow, Thorn, Void, Wait, Wall, Warp, None
    }

    public enum SkillStack{
        rng, buff, move, turns, nostack, skele, None
    }

    public enum SkillClasses{
        AegisAlliesAtk, AegisAtk, AegisBegin, AegisKill, AegisTurn, AegisWait, AoeAtk, BideAlliesAtk, BideAlliesWait, BideKill, BideWait, DivineMove,
        EnfeebleAtk, EnfeebleEnemiesWait, FireAtk, FireDef, FireKill, FireMove, ForceAtk, 
        HealAtk, HealAlliesAtk, HealAlliesWait, HealKill, HealTurn, HealWait, HobbleAtk, NullifyAtk, NullifyEnemiesWait, QuickenAlliesAtk, QuickenAlliesWait,
        RageAlliesWait, RageAtk, RageDef, RageKill, RageMove, RageWait, RootAtk, RootEnemiesWait, SicklyAtk, WispKill, SnowAtk, SnowMove, ThornDef, VoidAtk, 
        WallMove, WarpAtk, None
    }

    public enum SkillTypes{
        Offense, Defense, Utility, None
    }
}
