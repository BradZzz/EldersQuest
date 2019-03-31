using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

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

    public static float ReturnWaitTime(Skill.Actions interaction){
        switch(interaction){
            case Skill.Actions.DidKill: return AFTER_KILL;
            case Skill.Actions.DidAttack: return ATK_WAIT;
            case Skill.Actions.DidDefend: return ATK_WAIT;
            default: return 0;
        }
    }

    public static float ReturnWaitTimeDialog(Skill.Actions interaction){
        switch(interaction){
            case Skill.Actions.DidKill: return AFTER_KILL;
            case Skill.Actions.DidAttack: return NO_ATK_WAIT;
            case Skill.Actions.DidDefend: return ATK_WAIT;
            default: return 0;
        }
    }

    //Something is happening with the unit that needs an animation
    public static void InteractionAnimation(Skill.Actions interaction, UnitProxy unit, string msg, Color color, string animDesc, bool shakeChar, bool deathConsideration = false){
        //Debug.Log("Animation reason: " + animDesc);
        instance.StartCoroutine(instance.FloatUpAnim(msg, color, ReturnWaitTime(interaction), unit.transform, shakeChar, deathConsideration));
        instance.timeLeft = ANIMATION_WAIT_TIME_LIMIT;
    }

    //Something is happening with the tile that needs an animation
    public static void InteractionAnimation(Skill.Actions interaction, TileProxy tile, string msg, Color color, string animDesc){
       //Debug.Log("Animation reason: " + animDesc);
       instance.StartCoroutine(instance.FloatUpAnim(msg, color, ReturnWaitTime(interaction), tile.transform));
       //instance.timeLeft = ANIMATION_WAIT_TIME_LIMIT;
    }


    public static void InteractionAnimationGameobject(GameObject objToFloat, GameObject parent, float wait, bool shakeChar = false){
       //Debug.Log("Animation reason: " + animDesc);
       instance.StartCoroutine(instance.FloatUpGameObj(objToFloat, parent, wait, shakeChar));
       //instance.timeLeft = ANIMATION_WAIT_TIME_LIMIT;
    }

    public static void InteractionDialogGameobject(Skill.Actions interaction, TileProxy tile, string msg, Color color){
       //Debug.Log("Animation reason: " + animDesc);
       instance.StartCoroutine(instance.DialogAnim(msg, color, ReturnWaitTimeDialog(interaction), tile.GetUnit().transform));
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
        if (oTransform != null){
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
                PlayImpactSound();
            }
            if (deathConsideration) {
                UnitProxy unit = oTransform.GetComponent<UnitProxy>();
                if (unit.GetData().IsDead()) {
                    ConditionTracker.instance.EvalDeath(unit);  
                }
            } else {
                GameObject numObj = new GameObject();
                numObj.transform.position = pos;
                numObj.transform.rotation = Quaternion.identity;
                numObj.transform.parent = oTransform;
                numObj.AddComponent<TextMesh>();
                numObj.GetComponent<MeshRenderer>().sortingLayerName = "FX";
                numObj.GetComponent<TextMesh>().characterSize = .2f;
                numObj.GetComponent<TextMesh>().text = msg;
                numObj.GetComponent<TextMesh>().color = color;
                iTween.ShakePosition(numObj,new Vector3(0,.5f,0), .3f);
                iTween.MoveTo(numObj,new Vector3(pos.x,pos.y + .3f,pos.z), .3f);
                yield return new WaitForSeconds(.4f);
                Destroy(numObj);
            }
        }
    }

    IEnumerator DialogAnim(string msg, Color color, float wait, Transform oTransform)
    {
        yield return new WaitForSeconds(wait);
        //Debug.Log("FloatUpAnim");
        if (oTransform != null){
            Vector3 pos = oTransform.position;
            pos.x += .7f;
            pos.y += .8f;

            //GameObject numObj = new GameObject();
            //numObj.transform.position = pos;
            //numObj.transform.rotation = Quaternion.identity;
            //numObj.transform.parent = oTransform;
            //numObj.AddComponent<SpriteRenderer>();
            //numObj.GetComponent<SpriteRenderer>().sprite = BoardProxy.instance.glossary.GetComponent<Glossary>().battleMsgBubble;

            //GameObject numObjChild = Instantiate(BoardProxy.instance.glossary.GetComponent<Glossary>().battleMsgBubble, oTransform, false);
            //numObjChild.transform.GetChild(0).GetComponent<Canvas>().worldCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
            //numObjChild.transform.GetChild(0).GetComponent<Canvas>().sortingLayerName = "FX";
            //numObjChild.transform.position = pos;

            GameObject numObj = new GameObject();
            numObj.transform.parent = oTransform;
            numObj.transform.position = pos;
            //Vector3 pos = numObj.transform.position;
            //pos.x += .5f;
            //numObj.transform.position = pos;
            numObj.AddComponent<TextMesh>();
            numObj.GetComponent<MeshRenderer>().sortingLayerName = "Dialog";
            numObj.GetComponent<TextMesh>().characterSize = .1f;
            numObj.GetComponent<TextMesh>().text = msg;
            numObj.GetComponent<TextMesh>().color = color;
            numObj.GetComponent<TextMesh>().anchor = TextAnchor.UpperCenter;

            GameObject numObjChild = new GameObject();
            numObjChild.transform.parent = numObj.transform;
            //if (msg.Length > 5) {
            numObjChild.transform.localPosition = new Vector3(0,-.05f,0);
            numObjChild.transform.localScale = new Vector3(.7f,.5f,.5f);
            //} else {
            //    numObjChild.transform.localPosition = new Vector3(.2f,-.05f,0);
            //    numObjChild.transform.localScale = new Vector3(.5f,.5f,.5f);
            //}
            numObjChild.AddComponent<SpriteRenderer>();
            numObjChild.GetComponent<SpriteRenderer>().sortingLayerName = "Dialog";
            numObjChild.GetComponent<SpriteRenderer>().sprite = BoardProxy.instance.glossary.GetComponent<Glossary>().battleMsgBubble;

            //GameObject numObj = new GameObject();
            //numObj.transform.position = pos;
            //numObj.transform.rotation = Quaternion.identity;
            //numObj.transform.parent = oTransform;
            //numObj.AddComponent<TextMesh>();
            //numObjChild.GetComponent<MeshRenderer>().sortingLayerName = "FX";
            //numObj.GetComponent<TextMesh>().characterSize = .1f;
            //numObj.GetComponent<TextMesh>().text = msg;
            //numObj.GetComponent<TextMesh>().color = color;
            //iTween.ShakePosition(numObj,new Vector3(0,.5f,0), .3f);
            iTween.MoveTo(numObj,new Vector3(pos.x,pos.y + .3f,pos.z), .3f);
            yield return new WaitForSeconds(1.2f);
            Destroy(numObj);
        }
    }

    IEnumerator FloatUpGameObj(GameObject objToFloat, GameObject parent, float wait, bool shakeChar = false)
    {
        yield return new WaitForSeconds(wait);
        Vector3 pos = parent.transform.position;
        pos.y += .7f;
        GameObject newObj = Instantiate(objToFloat, pos, Quaternion.identity);
        if (shakeChar){
            yield return new WaitForSeconds(.2f);
            iTween.ShakePosition(newObj,new Vector3(.25f,0,0), .2f);
        }
        iTween.MoveTo(newObj,new Vector3(pos.x,pos.y + .6f,pos.z), .3f);
        yield return new WaitForSeconds(.6f);
        Destroy(newObj);
        //yield return null;
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
