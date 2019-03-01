using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//Controls all of the battle animation handling
public class AnimationInteractionController : MonoBehaviour
{
    public static float NO_WAIT = 0;
    public static float NO_ATK_WAIT = .5f;
    public static float ATK_WAIT = 1.4f;
    public static float AFTER_KILL = NO_ATK_WAIT + ATK_WAIT;

    public static float ANIMATION_WAIT_TIME_LIMIT = ATK_WAIT * 2;
    public static AnimationInteractionController instance;

    public float timeLeft;

    void Awake(){
        instance = this;
        timeLeft = 2f;
    }

    float ReturnWaitTime(Skill.Actions interaction){
        switch(interaction){
            case Skill.Actions.DidKill: return AFTER_KILL;
            case Skill.Actions.DidAttack: return ATK_WAIT;
            case Skill.Actions.DidDefend: return ATK_WAIT;
            default: return 0;
        }
    }

    //Something is happening with the unit that needs an animation
    public static void InteractionAnimation(Skill.Actions interaction, UnitProxy unit, string msg, Color color, string animDesc, bool shakeChar, bool deathConsideration = false){
        //Debug.Log("Animation reason: " + animDesc);
        instance.StartCoroutine(instance.FloatUpAnim(msg, color, instance.ReturnWaitTime(interaction), unit.transform, shakeChar, deathConsideration));
        instance.timeLeft = ANIMATION_WAIT_TIME_LIMIT;
    }

    //Something is happening with the tile that needs an animation
    public static void InteractionAnimation(Skill.Actions interaction, TileProxy tile, string msg, Color color, string animDesc){
       //Debug.Log("Animation reason: " + animDesc);
       instance.StartCoroutine(instance.FloatUpAnim(msg, color, instance.ReturnWaitTime(interaction), tile.transform));
       //instance.timeLeft = ANIMATION_WAIT_TIME_LIMIT;
    }

    public static float GetClipLengthByName(Animator anim, string cName){
      return anim.runtimeAnimatorController.animationClips.First(x => x.name == cName).length;
    }

    public static void ResetTime(){
        instance.timeLeft = ANIMATION_WAIT_TIME_LIMIT;
    }

    public static bool AllAnimationsFinished(){
        return instance.timeLeft <= 0;
    }

    IEnumerator FloatUpAnim(string msg, Color color, float wait, Transform oTransform, bool shakeChar = false, bool deathConsideration = false)
    {
        yield return new WaitForSeconds(wait);
        //Debug.Log("FloatUpAnim");
        Vector3 pos = oTransform.position;
        //Debug.Log("FloatUpAnim pos: " + pos.ToString());
        //pos.x += .3f;
        //pos.y += 1f;
        if (oTransform.GetComponent<TileProxy>() != null) {
            pos.x -= .3f;
            pos.y += .5f;
        } else {
            //pos.x -= .3f;
            pos.y += 1f;
        }
        if (shakeChar){
            yield return new WaitForSeconds(.2f);
            oTransform.GetComponent<UnitProxy>().Shake();
        }
        GameObject numObj = new GameObject();
        numObj.transform.position = pos;
        numObj.transform.rotation = Quaternion.identity;
        numObj.transform.parent = oTransform;
        numObj.AddComponent<TextMesh>();
        numObj.GetComponent<MeshRenderer>().sortingOrder = 20000;
        numObj.GetComponent<TextMesh>().characterSize = .2f;
        numObj.GetComponent<TextMesh>().text = msg;
        numObj.GetComponent<TextMesh>().color = color;
        iTween.ShakePosition(numObj,new Vector3(0,.5f,0), .3f);
        iTween.MoveTo(numObj,new Vector3(pos.x,pos.y + .3f,pos.z), .3f);
        PlayImpactSound();
        yield return new WaitForSeconds(.4f);
        Destroy(numObj);
        yield return null;
        if (deathConsideration) {
            UnitProxy unit = oTransform.GetComponent<UnitProxy>();
            if (unit.GetData().IsDead()) {
                ConditionTracker.instance.EvalDeath(unit);  
            }
        }
    }

    void Update()
    {
        if (timeLeft > 0) {
            timeLeft -= Time.deltaTime;
        }
     }

    #region SFX

    private void PlayImpactSound()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(FMODPaths.ImpactDamageSound, this.gameObject);
    }

    #endregion
}
