using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObstacleProxyEdit : GridObjectProxyEdit
{
    private ObstacleEdit _data;
    protected override GridObjectEdit data
    {
        get { return _data; }
    }

    public void Init()
    {
        if (_data == null)
            _data = new ObstacleEdit();
        SnapToCurrentPosition();
    }

    public override void OnSelected()
    {
      //throw new NotImplementedException();
    }
}
