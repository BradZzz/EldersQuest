using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TurnActionsBasicUnit : TurnActions
{
     /*
      * Build this out later to give different characters cool turn interactions
      */
      //public TurnActionsBasicUnit()
      //{
      //    mv = 1;
      //    atk = 1;
      //}

      public TurnActionsBasicUnit(int mv = 1, int atk = 1)
      {
          this.mv = mv;
          this.atk = atk;

          Debug.Log("Implementing turn actions: " + mv.ToString() + ":" + atk.ToString());
      }
}
