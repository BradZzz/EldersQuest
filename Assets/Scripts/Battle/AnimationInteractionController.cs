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

    public static AnimationInteractionController instance;

    private int animationQueue;

    void Awake(){
        instance = this;
        animationQueue = 0;
    }

    float ReturnWaitTime(Skill.Actions interaction){
        switch(interaction){
            case Skill.Actions.DidKill: return AFTER_KILL;
            case Skill.Actions.DidAttack: return ATK_WAIT;
            case Skill.Actions.DidDefend: return ATK_WAIT;
            default: return 0;
        }
    }

    public static void InteractionAnimation(Skill.Actions interaction, UnitProxy unit, string msg, Color color, string animDesc){
        Debug.Log("Animation reason: " + animDesc);
        instance.StartCoroutine(instance.FloatUpAnim(msg, color, instance.ReturnWaitTime(interaction), unit.transform));
    }

    public static void InteractionAnimation(Skill.Actions interaction, TileProxy tile, string msg, Color color, string animDesc){
       Debug.Log("Animation reason: " + animDesc);
       instance.StartCoroutine(instance.FloatUpAnim(msg, color, instance.ReturnWaitTime(interaction), tile.transform));
    }

    public static float GetClipLEngthByName(Animator anim, string cName){
      return anim.runtimeAnimatorController.animationClips.First(x => x.name == cName).length;
    }

    public static bool AllAnimationsFinished(){
        return instance.animationQueue <= 0;
    }

    IEnumerator FloatUpAnim(string msg, Color color, float wait, Transform oTransform)
    {
        animationQueue++;
        yield return new WaitForSeconds(wait);
        Debug.Log("FloatUpAnim");
        Vector3 pos = oTransform.position;
        Debug.Log("FloatUpAnim pos: " + pos.ToString());
        pos.x -= .3f;
        pos.y += .3f;
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
        yield return new WaitForSeconds(.4f);
        Destroy(numObj);
        yield return null;
        animationQueue--;
    }
}
