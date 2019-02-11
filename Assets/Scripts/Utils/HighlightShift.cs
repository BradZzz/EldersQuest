using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightShift : MonoBehaviour
{
  public float y;

  void OnEnable()
  {
    if (GetComponent<Renderer>() != null)
    {
      foreach (Material mObj in GetComponent<Renderer>().materials)
      {
        mObj.color = new Color(
            mObj.color.r, mObj.color.g,
            mObj.color.b, 0);
      }
    }

    if (GetComponent<CanvasRenderer>() != null)
    {
      for (int i = 0; i < GetComponent<CanvasRenderer>().materialCount; i++)
      {
        Material mat = GetComponent<CanvasRenderer>().GetMaterial(i);
        mat.color = new Color(
            mat.color.r, mat.color.g,
            mat.color.b, 0);
      }
    }
    iTween.MoveTo(gameObject, iTween.Hash("y", y, "islocal", true, "time", 1.2f, "looptype", "pingPong", "easetype", "spring"));

    //The UI is weird with the FadeIn. Restrict only to sprites
    if (GetComponent<Renderer>() != null)
    {
      GetComponent<FadeMaterials>().FadeIn();
    }
  }
}
