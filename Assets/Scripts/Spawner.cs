using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class Spawner : MonoBehaviour, IGridObject
{

    [SerializeField] GameObject enemy;

    public GridObjectVisual gridObjectVisual { get; set; }

    public GridPosition gridPosition { get; set; }

    private float timer = 0;
    

    public GameObject GetGameObject()
    {
        return this.gameObject;
    }

  //  private EnemyWavePoints currentWavePoint;

    // Start is called before the first frame update
    void Start()
    {
        // gridPosition = system.WorldToGridPosition(this.transform.position);
        // GameObject enemys = Instantiate(enemy, GridSystem.Instance.GridToWorldPosition(gridPosition), Quaternion.identity);
     //   InvokeRepeating("SpawnEnemy", 1, 10);
    }




    // Update is called once per frame
    void Update()
    {
       //  InvokeRepeating("SpawnEnemy", 1, 10);

        /*
        if (timer <= 0) SpawnEnemy();
        else timer -= Time.deltaTime;

        Debug.Log(timer);

        */

    }

    public void SpawnEnemy(GameObject enemy)
    {

        GameObject enemys = Instantiate(enemy, GridSystem.Instance.MiddleGridToWorldPosition(gridPosition), Quaternion.identity);


    }

    private void GetWavePoint()
    {

        // EnemySpawnManager thi = EnemySpawnManager.Instance;

      //  Debug.Log(EnemySpawnManager.Instance);
  //     if (EnemySpawnManager.Instance.TryGoToNextWavePoint(out EnemyWavePoints nextWavePoints))
  //      {
   //         currentWavePoint = nextWavePoints;
   //         enemy = currentWavePoint.enemyUnit;
   //         Debug.Log(currentWavePoint.cooldownBetweenUnitSpawn);
    //    }
    }
}
