using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
