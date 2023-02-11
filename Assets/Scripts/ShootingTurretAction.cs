using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Turret))]
public class ShootingTurretAction : MonoBehaviour, ITurretAction, IUpgradable
{
    [SerializeField] GameObject bullet;

    [SerializeField] DamageAmountStrategySO damageStrategy;


    private EnemyUnit targetedEnemy = null;
    private Turret mainComponent;



    void Start()
    {
        mainComponent = GetComponent<Turret>();
    }
    public void TurretAction(Collider[] colliders)
    {
        Aiming(colliders);

        if (targetedEnemy != null)
        {
            Shoot(targetedEnemy.transform.position);       
        }

    }

    void Aiming(Collider[] objectcolliders)
    {
        float firstEnemy = float.MaxValue;
        targetedEnemy = null;

        foreach (Collider enemyCollider in objectcolliders)
        {
            Debug.Log(enemyCollider.gameObject.name);
            EnemyUnit enemy = enemyCollider.GetComponentInParent<EnemyUnit>();

            if (enemy == null) continue;

            if (enemy.GetDistanceToEnd() < firstEnemy)
            {
                firstEnemy = enemy.GetDistanceToEnd();
                targetedEnemy = enemy;
            }
        }

    }

    void Shoot(Vector3 shootToLocation)
    {

        if (shootToLocation == null) return;
        if (mainComponent == null) mainComponent = GetComponent<Turret>();
        Vector3 direction = shootToLocation - GridSystem.Instance.MiddleGridToWorldPosition(mainComponent.gridPosition);
        direction.Normalize();

        Vector3 spawnPosition = GridSystem.Instance.MiddleGridToWorldPosition(mainComponent.gridPosition);
        spawnPosition += new Vector3(0, 0.5f, 0);
        GameObject bulletObject = Instantiate(bullet, spawnPosition, Quaternion.identity);
        StandardBullet bul = bulletObject.GetComponent<StandardBullet>();
        bul.direction = direction;
        bul.damage = damageStrategy.damageAmount;


    }

    public void Upgrade(UpgradeStrategySO upgrade)
    {
        if (upgrade is DamageAmountStrategySO)
        {
            damageStrategy = (DamageAmountStrategySO)upgrade;
        }
    }


}
