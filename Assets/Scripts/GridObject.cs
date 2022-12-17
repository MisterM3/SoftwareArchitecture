using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Objects that are grid based (turrets, spawnpoints, endpoints...)

public interface IGridObject
{   
     GridObjectVisual gridObjectVisual { get; }
    GridPosition gridPosition { get; }

    GameObject GetGameObject();
}


