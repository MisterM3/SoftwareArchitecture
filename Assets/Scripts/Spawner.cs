using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour, IGridObject
{

    [SerializeField] GameObject enemy;

    public GridObjectVisual gridObjectVisual { get; set; }

    public GridPosition gridPosition { get; }

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
        InvokeRepeating("SpawnEnemy", 1, 1);
        
    }

    void SpawnEnemy()
    {
        Debug.Log("spawned");
        Instantiate(enemy, this.transform);
    }
}
