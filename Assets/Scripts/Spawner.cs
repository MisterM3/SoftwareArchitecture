using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class Spawner : MonoBehaviour, IGridObject
{

    [SerializeField] GameObject enemy;

    public GridObjectVisual gridObjectVisual { get; set; }

    public GridPosition gridPosition { get; set; }

    public GameObject GetGameObject()
    {
        return this.gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        // gridPosition = system.WorldToGridPosition(this.transform.position);
        // GameObject enemys = Instantiate(enemy, GridSystem.Instance.GridToWorldPosition(gridPosition), Quaternion.identity);
        InvokeRepeating("SpawnEnemy", 1, 10);
    }




    // Update is called once per frame
    void Update()
    {
       // InvokeRepeating("SpawnEnemy", 1, 10);
        
    }

    public void SpawnEnemy()
    {
        // Debug.Log("spawned");
        GameObject enemys = Instantiate(enemy, GridSystem.Instance.MiddleGridToWorldPosition(gridPosition), Quaternion.identity);
    }
}
