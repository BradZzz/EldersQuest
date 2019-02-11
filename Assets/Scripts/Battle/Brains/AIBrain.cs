using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIBrain : MonoBehaviour
{
  //public AIBrain instance;

  //void Awake()
  //{
  //    instance = this;
  //}

  internal abstract void StartThinking();
  internal abstract string BeginProcess();
}
