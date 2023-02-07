using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Objects that are grid based (turrets, spawnpoints, endpoints...)

public interface IGridObject
{   
    GridPosition gridPosition { get; set; }

    GameObject GetGameObject();
}


