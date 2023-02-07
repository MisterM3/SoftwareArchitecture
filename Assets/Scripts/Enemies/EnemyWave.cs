using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Wave", menuName = "ScriptableObjects/Waves", order = 1)]
public class EnemyWave : ScriptableObject
{
    public List<GameObject> waveUnits;
    public float timeBetweenUnits;
}
