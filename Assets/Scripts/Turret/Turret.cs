using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour, IGridObject, IUpgradable
{



    //TurretAction is what happens when colliders are inside the turret range
    [SerializeField] ITurretAction turretAction;

    [SerializeField] ITurretRange _turretRange;

    [SerializeField] UpgradePaths upgrades;


    public GridObjectVisual gridObjectVisual { get; set; }

    public GridPosition gridPosition { get; set; }



    private EnemyUnit targetedEnemy = null;

    public event EventHandler OnTurretUpgrade;

    [SerializeField] private TurretShootSpeedStrategySO actionSpeedStragety;

    public GameObject GetGameObject()
    {
        return this.gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {

        //Maybe change to tryget and do a nullcheck
        turretAction = GetComponent<ITurretAction>();

        _turretRange = GetComponent<ITurretRange>();

        if (turretAction == null) Debug.LogError("TURRET HAS NO ACTION");


        gridPosition = GridSystem.Instance.WorldToGridPosition(this.transform.position);
        upgrades = GetComponent<UpgradePaths>();

        StartCoroutine("ActionCycle");
    }


    IEnumerator ActionCycle()
    {
        while (true)
        {
            Collider[] coll = _turretRange.GetCollidersInRadius();
            if (coll == null) yield return new WaitForSeconds(0.1f);
            else
            {
                turretAction.TurretAction(coll);
                yield return new WaitForSeconds(actionSpeedStragety.shootSpeed);
            }
        }
    }


    public void UpgradeFirst()
    {
        upgrades.UpgradeFirst();
    }

    public void UpgradeSecond()
    {
        upgrades.UpgradeSecond();
    }


    public void ChangeActionSpeedStragety(TurretShootSpeedStrategySO actionSpeedSO)
    {
        actionSpeedStragety = actionSpeedSO;
    }


    public string GetFirstUpgradeNameAndCost()
    {
       return upgrades.GetFirstUpgradeNameAndCost();
    }

    public string GetSecondUpgradeNameAndCost()
    {
        return upgrades.GetSecondUpgradeNameAndCost();
    }
    public int GetFirstCost()
    {
        return upgrades.GetFirstUpgradeCost();
    }

    public int GetSecondCost()
    {
        return upgrades.GetSecondUpgradeCost();
    }


    public void DestroyTurret()
    {
        GridSystem.Instance.DesroyGridObjectAtLocation(gridPosition);
        DestroyImmediate(gameObject);
    }


    public void ChangeStrategy(UpgradeStrategySO upgrade)
    {
        if (upgrade is TurretShootSpeedStrategySO)
        {
            actionSpeedStragety = (TurretShootSpeedStrategySO)upgrade;
        }
    }




}
