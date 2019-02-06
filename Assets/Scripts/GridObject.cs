using UnityEngine;

public abstract class GridObject
{
    protected Vector3Int position;

    public virtual Vector3Int GetPosition()
    {
        return position;
    }

    //need logic for layers/collision

    public virtual void SetPosition(Vector3Int pos)
    {
        position = pos;
    }

}