using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightShift : MonoBehaviour
{
  public float y;

  void OnEnable()
  {
    foreach (Material mObj in GetComponent<Renderer>().materials)
    {
      mObj.color = new Color(
          mObj.color.r, mObj.color.g,
          mObj.color.b, 0);
    }
    iTween.MoveTo(gameObject, iTween.Hash("y", y, "islocal", true, "time", 1.2f, "looptype", "pingPong", "easetype", "spring"));
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
