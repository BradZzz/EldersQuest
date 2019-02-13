using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObstacleProxy : GridObjectProxy
{
    private Obstacle _data;
    protected override GridObject data
    {
        get { return _data; }
    }

    public void Init()
    {
        if (_data == null)
            _data = new Obstacle();
        SnapToCurrentPosition();
    }

    public override void OnSelected()
    {
      //throw new NotImplementedException();
    }
}
