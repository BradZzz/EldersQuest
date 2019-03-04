using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogoWaiter : MonoBehaviour
{
    public float secs;
    public string nextScene;
    public Image fadeImg;

    // Start is called before the first frame update
    void Start()
    {
       StartCoroutine(WaitForSecs(secs,nextScene)); 
    }

    public void updateColor(float val)
    {
       Color c = fadeImg.color;
       c.a = val;
       fadeImg.color = c;
    }

    IEnumerator WaitForSecs(float secs, string nextScene){
        iTween.ValueTo(gameObject,iTween.Hash("from",1f,"to",0f,"delay",0,"time",.5f,"onupdate","updateColor"));
        yield return new WaitForSeconds(.5f);

        yield return new WaitForSeconds(secs - 1);
        iTween.ValueTo(gameObject,iTween.Hash("from",0f,"to",1f,"delay",0,"time",.5f,"onupdate","updateColor"));
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene(nextScene);
    }
}
