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

    public void SpawnEnemy(GameObject enemy)
    {

        GameObject enemys = Instantiate(enemy, GridSystem.Instance.MiddleGridToWorldPosition(gridPosition), Quaternion.identity);


    }
}
