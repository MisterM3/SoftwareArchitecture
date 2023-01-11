using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


/*
public class EnemySpawnManager : MonoBehaviour
{
    public static EnemySpawnManager Instance { get; private set; }

    [SerializeField] Spawner spawnPoint;

    Queue<GameObject> spawnQueue;

    [SerializeField] List<EnemyWave> waves;

    private EnemyWave thisWave;




    // Start is called before the first frame update
    void Start()
    {
        if (Instance != null)
        {
            Debug.LogError("Already a EnemySpawnManager in scene destroying: " + gameObject);
            Destroy(this);
            return;
        }

        Instance = this;
        NextWavePoint();
    }


    public EnemyWavePoints GetWavePoint()
    {
        return waves[0].enemyWaveQueue.Peek();
    }

    public void NextWavePoint()
    {
        thisWave = waves[0];
    }

    /// <summary>
    /// Continues the wave if there are still elements in it, otherwise returns false (and a empty enemywavepoints)
    /// </summary>
    /// <returns></returns>
    public bool TryGoToNextWavePoint(out EnemyWavePoints nextWavePoint)
    {
          if (thisWave.enemyWaveQueue.TryDequeue(out EnemyWavePoints currentPoint))
          {
              nextWavePoint = currentPoint;
              return true;
          }

          nextWavePoint = new EnemyWavePoints();
          return false;
      }
        
    
}

*/
