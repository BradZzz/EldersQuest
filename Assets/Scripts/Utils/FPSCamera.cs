using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FPSCamera : MonoBehaviour
{
   public TextMeshProUGUI txt;
  
   void Update()
   {
       //GUI.Label(new Rect(0, 0, 100, 100), ((int)(1.0f / Time.smoothDeltaTime)).ToString());   
      //Debug.Log("fps: " + ((int)(1.0f / Time.smoothDeltaTime)).ToString());   
      txt.text = "fps: " + ((int)(1.0f / Time.smoothDeltaTime)).ToString();  
   }
}
