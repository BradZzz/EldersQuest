using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class GridObjectProxyEdit : MonoBehaviour
{

    protected virtual GridObjectEdit data
    {
        get; set;
    }

    public virtual void SetPosition(Vector3Int pos)
    {
        data.SetPosition(pos);
    }
    public virtual Vector3Int GetPosition()
    {
        return data.GetPosition();
    }

    public virtual void SnapToCurrentPosition()
    {
        transform.position = BoardEditProxy.GetWorldPosition(data.GetPosition());
    }

    public abstract void OnSelected();

}
