using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathPoint : MonoBehaviour, IGridObject
{
    public GridPosition gridPosition { get; set; }

    public GameObject GetGameObject()
    {
        return this.gameObject;
    }

}
