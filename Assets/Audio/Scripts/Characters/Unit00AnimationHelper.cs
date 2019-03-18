using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit00AnimationHelper : MonoBehaviour
{
   public void Attack()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(FMODPaths.Unit001AttackSound, this.gameObject);
    }

    public void CthuluAttackSound()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(FMODPaths.CthuluAttackSound, this.gameObject);
    }

    #region Human

    public void HumanSoldierAttackSound()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(FMODPaths.HumanSoldierSound, this.gameObject);
    }

    public void HumanScoutAttackSound()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(FMODPaths.HumanScoutSound, this.gameObject);
    }

    public void HumanMageAttackSound()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(FMODPaths.HumanMageSound, this.gameObject);
    }

    #endregion


    #region Cthulu

    public void CthuluSoldierAttackSound()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(FMODPaths.CthuluSoldierSound, this.gameObject);
    }

    public void CthuluScoutAttackSound()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(FMODPaths.CthuluScoutSound, this.gameObject);
    }

    public void CthuluMageAttackSound()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(FMODPaths.CthuluMageSound, this.gameObject);
    }

    public void CthuluSkeAttackSound()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(FMODPaths.CthuluSkeSound, this.gameObject);
    }


    #endregion

    #region Egypt

    public void EgyptSoldierAttackSound()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(FMODPaths.EgyptSoldierSound, this.gameObject);
    }

    public void EgyptScoutAttackSound()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(FMODPaths.EgyptScoutSound, this.gameObject);
    }

    public void EgyptMageAttackSound()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(FMODPaths.EgyptMageSound, this.gameObject);
    }


    #endregion
}
