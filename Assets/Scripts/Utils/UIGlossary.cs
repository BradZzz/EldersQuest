using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGlossary : MonoBehaviour
{
    public Sprite[] HumanSoldier;
    public Sprite[] HumanScout;
    public Sprite[] HumanMage;

    public Sprite[] EgyptSoldier;
    public Sprite[] EgyptScout;
    public Sprite[] EgyptMage;

    public Sprite[] CthulhuSoldier;
    public Sprite[] CthulhuScout;
    public Sprite[] CthulhuMage;   

    public Sprite[] finalBlack;
    public Sprite[] finalRed;
    public Sprite[] finalBlue;

    public Sprite[] fxAoe;
    public Sprite[] fxFire;
    public Sprite[] fxForce;
    public Sprite[] fxHeal;
    public Sprite[] fxThorn;
    public Sprite[] fxVoid;
    public Sprite[] fxWarp;
    public Sprite[] fxWispKill;

    public Sprite[] GetSprites(uiFX uiImg){
        switch(uiImg){
            case uiFX.HumanSoldier: return HumanSoldier;
            case uiFX.HumanScout: return HumanScout;
            case uiFX.HumanMage: return HumanMage;
            case uiFX.EgyptSoldier: return EgyptSoldier;
            case uiFX.EgyptScout: return EgyptScout;
            case uiFX.EgyptMage: return EgyptMage;
            case uiFX.CthulhuSoldier: return CthulhuSoldier;
            case uiFX.CthulhuScout: return CthulhuScout;
            case uiFX.CthulhuMage: return CthulhuMage;
            case uiFX.finalBlack: return finalBlack;
            case uiFX.finalRed: return finalRed;
            case uiFX.finalBlue: return finalBlue;
            case uiFX.fxAoe: return fxAoe;
            case uiFX.fxFire: return fxFire;
            case uiFX.fxForce: return fxForce;
            case uiFX.fxHeal: return fxHeal;
            case uiFX.fxThorn: return fxThorn;
            case uiFX.fxVoid: return fxVoid;
            case uiFX.fxWarp: return fxWarp;
            case uiFX.fxWispKill: return fxWispKill;
            default: return HumanSoldier;
        }
    }

    public enum uiFX{
        HumanSoldier, HumanScout, HumanMage, EgyptSoldier, EgyptScout, EgyptMage, CthulhuSoldier, CthulhuScout, CthulhuMage, 
        finalBlack, finalRed, finalBlue, fxAoe, fxFire, fxForce, fxHeal, fxThorn, fxVoid, fxWarp, fxWispKill, none
    }
}
