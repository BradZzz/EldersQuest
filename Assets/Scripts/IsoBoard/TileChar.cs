using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileChar : MonoBehaviour
{
    public Character character;

    private Transform[] currentPos;
    
    public void PutCurrentPos(Transform[] currentPos)
    {
      this.currentPos = currentPos;
    }

    public Transform[] GetCurrentPos()
    {
      return currentPos;
    }
}
