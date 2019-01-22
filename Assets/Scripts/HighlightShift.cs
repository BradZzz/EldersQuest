using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightShift : MonoBehaviour
{
  private void Awake()
  {
    iTween.MoveTo(gameObject, iTween.Hash("y", .29f, "islocal", true, "time", 1.2f, "looptype", "pingPong", "easetype", "spring"));
  }
}
