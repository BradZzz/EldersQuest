using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class GridObjectProxy : MonoBehaviour
{

    protected virtual GridObject data
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
        transform.position = BoardProxy.GetWorldPosition(data.GetPosition());
    }

    public abstract void OnSelected();

}
