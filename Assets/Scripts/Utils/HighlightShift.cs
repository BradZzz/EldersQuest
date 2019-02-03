using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightShift : MonoBehaviour
{
  private void Start()
  {
    //Color clr = Color.white;
    //clr.a = 0;
    //GetComponent<SpriteRenderer>().color = clr;
    //foreach (Material mObj in GetComponent<Renderer>().materials)
    //{
    //  mObj.color = new Color(
    //      mObj.color.r, mObj.color.g,
    //      mObj.color.b, 0);
    //}
    //iTween.MoveTo(gameObject, iTween.Hash("y", .29f, "islocal", true, "time", 1.2f, "looptype", "pingPong", "easetype", "spring"));
    //GetComponent<FadeMaterials>().FadeIn();
    //StartCoroutine(FadeIn());
  }

  void OnEnable()
  {
    foreach (Material mObj in GetComponent<Renderer>().materials)
    {
      mObj.color = new Color(
          mObj.color.r, mObj.color.g,
          mObj.color.b, 0);
    }
    iTween.MoveTo(gameObject, iTween.Hash("y", .29f, "islocal", true, "time", 1.2f, "looptype", "pingPong", "easetype", "spring"));
    GetComponent<FadeMaterials>().FadeIn();
  }

  //IEnumerator FadeIn()
  //{
  //  //iTween.FadeTo(gameObject, 1, .5f);
  //  //yield return new WaitForSeconds(.5f);
  //  GetComponent<FadeMaterials>().FadeIn();
  //  yield return new WaitForSeconds(.5f);
  //  iTween.MoveTo(gameObject, iTween.Hash("y", .29f, "islocal", true, "time", 1.2f, "looptype", "pingPong", "easetype", "spring"));
  //}
}
