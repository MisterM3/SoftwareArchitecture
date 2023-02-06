using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



public class EnemySpawnManager : MonoBehaviour
{
    public static EnemySpawnManager Instance { get; private set; }

    [SerializeField] Spawner spawnPoint;

    Queue<GameObject> spawnQueue;

    [SerializeField] List<EnemyWave> wavesList;

    private EnemyWave thisWave;


    //Change to get waveIndex from other manager;
    [SerializeField] private int waveIndex = -1;


    //When the wave is fully on screen
    public event EventHandler OnWaveCompleted;


    private IEnumerator coroutine;

    private bool waveSpawnDone = false;

    private int numberOfEnemiesAlive = 0;

    //Makes sure no enemy can be killed twice
    private List<EnemyUnit> killedEnemies;


    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Already a EnemySpawnManager in scene destroying: " + gameObject);
            Destroy(this);
            return;
        }

        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

        killedEnemies = new List<EnemyUnit>();

        GameStateManager.Instance.OnDuringWaveStart += Instance_OnDuringWaveStart;

        EnemyUnit.OnAnyEnemyKilled += EnemyUnit_OnAnyEnemyKilled;
        EnemyUnit.OnAnyEnemyReachEnd += EnemyUnit_OnAnyEnemyReachEnd;

        spawnPoint = GameObject.FindObjectOfType<Spawner>();
        coroutine = WaitAndSpawn(wavesList.First().timeBetweenUnits);
        //StartWave();
    }

    private void EnemyUnit_OnAnyEnemyReachEnd(object sender, EventArgs e)
    {
        if (killedEnemies.Contains((EnemyUnit)sender)) return;

        CheckIfAllEnemiesDead();
        killedEnemies.Add((EnemyUnit)sender);
    }

    private void EnemyUnit_OnAnyEnemyKilled(object sender, int e)
    {

        if (killedEnemies.Contains((EnemyUnit)sender)) return;

        CheckIfAllEnemiesDead();
        killedEnemies.Add((EnemyUnit)sender);
    }

    private void Instance_OnDuringWaveStart(object sender, int e)
    {
        NextWave(e);
        Debug.LogWarning("receiveEvent");
    }

    void StartWave()
    {
        coroutine = WaitAndSpawn(wavesList[waveIndex].timeBetweenUnits);
        StartCoroutine(coroutine);
        Debug.LogWarning("StartCoroutine");
    }


    public void NextWave(int waveNumber)
    {
        waveIndex = waveNumber;
        StartWave();
        Debug.LogWarning("StartWave");
    }

    // every 2 seconds perform the print()
    private IEnumerator WaitAndSpawn(float waitTime)
    {
        waveSpawnDone = false;
        Debug.LogWarning("start new wave");
        int i = 0;
        while (wavesList[waveIndex].waveUnits.Count > i)
        {

            spawnPoint.SpawnEnemy(wavesList[waveIndex].waveUnits[i]);
            numberOfEnemiesAlive++;
            i++;
            yield return new WaitForSeconds(waitTime);
        }

        waveSpawnDone = true;
    }



    private void CheckIfAllEnemiesDead()
    {

      

        numberOfEnemiesAlive--;

        if (!waveSpawnDone) return;

        if (numberOfEnemiesAlive < 0)
        {
            Debug.LogError("numberOfEnemiesAlive is negative!!!");
            numberOfEnemiesAlive = 0;
        }
        if (numberOfEnemiesAlive == 0)
        {
            OnWaveCompleted?.Invoke(this, EventArgs.Empty);
        }
    }






}


