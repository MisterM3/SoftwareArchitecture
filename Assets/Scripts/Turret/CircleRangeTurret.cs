using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[RequireComponent(typeof(Turret))]
public class CircleRangeTurret : MonoBehaviour, ITurretRange, IUpgradable
{

    [SerializeField] private TurretRangeStragetySO turretRangeStragety;

    public EventHandler<float> onRangeChanged { get; set; }

    private Turret mainComponent;
    void Start()
    {
        mainComponent = gameObject.GetComponent<Turret>();
    }


    public Collider[] GetCollidersInRadius()
    {
        if (mainComponent == null) mainComponent = GetComponent<Turret>();
        Vector3 center = GridSystem.Instance.MiddleGridToWorldPosition(mainComponent.gridPosition);

        Collider[] enemiesColliders = Physics.OverlapSphere(center, turretRangeStragety.turretRange, 1 << 3);
        return enemiesColliders;
    }

    public float getMaxRange()
    {
        return turretRangeStragety.turretRange;
    }

    public void Upgrade(UpgradeStrategySO upgrade)
    {
        if (upgrade is TurretRangeStragetySO)
        {
            turretRangeStragety = (TurretRangeStragetySO)upgrade;

            onRangeChanged?.Invoke(this, getMaxRange());
        }
    }
}
