using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathPoint : MonoBehaviour, IGridObject
{
    public GridObjectVisual gridObjectVisual { get; set; }

    public GridPosition gridPosition { get; set; }

    public GameObject GetGameObject()
    {
        return this.gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
