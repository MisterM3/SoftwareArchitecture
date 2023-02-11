using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



public class EnemySpawnManager : MonoBehaviour
{
    public static EnemySpawnManager Instance { get; private set; }

    [SerializeField] Spawner spawnPoint;

    [SerializeField] List<EnemyWave> wavesList;


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

        GameStateManager.Instance.OnDuringWaveStart += GameStateManager_OnDuringWaveStart;

        EnemyUnit.OnAnyEnemyKilled += EnemyUnit_OnAnyEnemyKilled;
        EnemyUnit.OnAnyEnemyReachEnd += EnemyUnit_OnAnyEnemyReachEnd;

        spawnPoint = GameObject.FindObjectOfType<Spawner>();
        coroutine = WaitAndSpawn(wavesList.First().timeBetweenUnits);
        //StartWave();
    }

    private void EnemyUnit_OnAnyEnemyReachEnd(object sender, EventArgs e)
    {
        DestroyedEnemy(sender);
    }

    private void EnemyUnit_OnAnyEnemyKilled(object sender, int money)
    {
        DestroyedEnemy(sender);
    }

    private void DestroyedEnemy(object sender)
    {
        //Check if the enemy was already killed by a tower this frame
        if (killedEnemies.Contains((EnemyUnit)sender)) return;

        CheckIfAllEnemiesDead();

        //Add it so no enemy gets killed twice
        killedEnemies.Add((EnemyUnit)sender);
    }

    private void GameStateManager_OnDuringWaveStart(object sender, int waveIndex)
    {
        NextWave(waveIndex);
    }

    void StartWave()
    {
        coroutine = WaitAndSpawn(wavesList[waveIndex].timeBetweenUnits);
        StartCoroutine(coroutine);
    }


    public void NextWave(int waveNumber)
    {
        waveIndex = waveNumber;
        StartWave();
    }

    // spawns every enemy of the wave, waiting a certain amount between spawning enemies
    private IEnumerator WaitAndSpawn(float waitTime)
    {
        waveSpawnDone = false;
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

        //If the wave has not finished spawning enemies don't end the wave
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


    private void OnDestroy()
    {
        GameStateManager.Instance.OnDuringWaveStart -= GameStateManager_OnDuringWaveStart;

        EnemyUnit.OnAnyEnemyKilled -= EnemyUnit_OnAnyEnemyKilled;
        EnemyUnit.OnAnyEnemyReachEnd -= EnemyUnit_OnAnyEnemyReachEnd;
    }


}


