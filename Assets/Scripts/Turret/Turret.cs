using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour, IGridObject, IUpgradable
{
    //TurretAction is what happens when colliders are inside the turret range
    [SerializeField] ITurretAction _turretAction;

    [SerializeField] ITurretRange _turretRange;

    [SerializeField] UpgradePaths _upgrades;

    public GridPosition gridPosition { get; set; }


    [SerializeField] private TurretShootSpeedStrategySO actionSpeedStragety;

    public GameObject GetGameObject()
    {
        return this.gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        //If it does not have an action throw an error
        if (!TryGetComponent<ITurretAction>(out ITurretAction action)) Debug.LogError("TURRET HAS NO ACTION");
        _turretAction = action;

        //If it does not have an range throw an error
        if (!TryGetComponent<ITurretRange>(out ITurretRange range)) Debug.LogError("TURRET HAS NO RANGE");
        _turretRange = range;

        if (_turretAction == null) Debug.LogError("TURRET HAS NO ACTION");

        gridPosition = GridSystem.Instance.WorldToGridPosition(this.transform.position);
        _upgrades = GetComponent<UpgradePaths>();

        StartCoroutine("ActionCycle");
    }


    IEnumerator ActionCycle()
    {
        while (true)
        {
            Collider[] coll = _turretRange.GetCollidersInRadius();
            if (coll == null) yield return new WaitForSeconds(0.05f);
            else
            {
                _turretAction.TurretAction(coll);
                yield return new WaitForSeconds(actionSpeedStragety.actionSpeed);
            }
        }
    }
    public UpgradePaths GetTurretUpgrade()
    {
        return _upgrades;
    }

    public void DestroyTurret()
    {
        GridSystem.Instance.RemoveGridObjectAtLocation(gridPosition);
        DestroyImmediate(gameObject);
    }


    public void Upgrade(UpgradeStrategySO upgrade)
    {
        if (upgrade is TurretShootSpeedStrategySO)
        {
            actionSpeedStragety = (TurretShootSpeedStrategySO)upgrade;
        }
    }

}
