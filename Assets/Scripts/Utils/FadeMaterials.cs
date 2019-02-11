using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class FadeMaterials : MonoBehaviour
{
  public void FadeOut()
  {
    iTween.ValueTo(gameObject, iTween.Hash(
        "from", 1.0f, "to", 0.0f,
        "time", .5f, "easetype", "linear",
        "onupdate", "setAlpha"));
  }
  public void FadeIn()
  {
    iTween.ValueTo(gameObject, iTween.Hash(
        "from", 0f, "to", 1f,
        "time", .5f, "easetype", "linear",
        "onupdate", "setAlpha"));
  }
  public void setAlpha(float newAlpha)
  {
    if (GetComponent<Renderer>() != null)
    {
      foreach (Material mObj in GetComponent<Renderer>().materials)
      {
        mObj.color = new Color(
          mObj.color.r, mObj.color.g,
          mObj.color.b, newAlpha);
      }
    }

    if (GetComponent<CanvasRenderer>() != null)
    {
      for (int i = 0; i < GetComponent<CanvasRenderer>().materialCount; i++)
      {
        Material mat = GetComponent<CanvasRenderer>().GetMaterial(i);
        mat.color = new Color(
          mat.color.r, mat.color.g,
          mat.color.b, newAlpha);
      }
    }
  }

}