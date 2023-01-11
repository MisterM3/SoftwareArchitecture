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


    private IEnumerator coroutine;


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


        spawnPoint = GameObject.FindObjectOfType<Spawner>();
        coroutine = WaitAndSpawn(wavesList.First().timeBetweenUnits);
        StartCoroutine(coroutine);
    }




    // every 2 seconds perform the print()
    private IEnumerator WaitAndSpawn(float waitTime)
    {
        int i = 0;
        while (wavesList.First().waveUnits.Count > i)
        {

            spawnPoint.SpawnEnemy(wavesList.First().waveUnits[i]);
            i++;
            yield return new WaitForSeconds(waitTime);
        }
    }






}


