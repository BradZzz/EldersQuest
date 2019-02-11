using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteadyComp : MonoBehaviour
{
  void OnEnable()
  {
    foreach (Material mObj in GetComponent<Renderer>().materials)
    {
      mObj.color = new Color(
          mObj.color.r, mObj.color.g,
          mObj.color.b, 0);
    }
    GetComponent<FadeMaterials>().FadeIn();
  }
}
