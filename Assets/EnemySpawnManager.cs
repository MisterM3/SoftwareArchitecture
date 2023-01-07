using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{

    [SerializeField] Spawner spawnPoint;

    Queue<GameObject> spawnQueue;

    [SerializeField] List<EnemyWave> waves;


    [Serializable]
    public struct EnemyWavePoints
    {

        public GameObject enemyUnit;
        public int amount;
        public float cooldownBetweenUnitSpawn;
        public float timeBeforeNextWavePoint;

    }

    [Serializable]
    public struct EnemyWave
    {
        [SerializeField] List<EnemyWavePoints> enemyWavePoint;

        public Queue<EnemyWavePoints> enemyWaveQueue;

        public void Start()
        {
            enemyWaveQueue = new Queue<EnemyWavePoints>(enemyWavePoint);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (waves[0].enemyWaveQueue.Peek().amount > 0)
        {
      //      waves[0].enemyWaveQueue.First().amount -= 1;
            SpawnNextEnemy();
        }else
        {
          //  waves[1].enemyWavePoint[0].;
                
        }
    }

    void SpawnNextEnemy()
    {
        spawnPoint.SpawnEnemy();
    }
}
