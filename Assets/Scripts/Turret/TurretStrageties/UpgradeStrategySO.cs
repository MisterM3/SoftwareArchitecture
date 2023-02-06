using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeStrategy", menuName = "ScriptableObjects/TurretUpgrades", order = 1)]
public abstract class UpgradeStrategySO : ScriptableObject
{
    public string nameUpgrade;
    public int costUpgrade;
}

