using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class Spawner : MonoBehaviour, IGridObject
{

    [SerializeField] GameObject enemy;
    public GridPosition gridPosition { get; set; }

    public GameObject GetGameObject()
    {
        return this.gameObject;
    }

    public void SpawnEnemy(GameObject enemy)
    {

        if (!enemy.TryGetComponent<EnemyUnit>(out EnemyUnit unit))
        {
            Debug.LogError("TRIED TO SPAWN A NON ENEMY UNIT");
            return;
        }
        
        GameObject enemys = Instantiate(enemy, GridSystem.Instance.MiddleGridToWorldPosition(gridPosition), Quaternion.identity);

    }
}
