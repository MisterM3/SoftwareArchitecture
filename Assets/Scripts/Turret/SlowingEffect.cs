using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowingEffect : MonoBehaviour, ITurretAction, IUpgradable
{
    [SerializeField] EnemyWalkingStragetySO normalWalkingSpeed;
    [SerializeField] EnemyWalkingStragetySO slowingSpeed;


    //Contains all enemies from last time
    List<Collider> oldEnemiesinArea;

    void Start()
    {
        oldEnemiesinArea = new List<Collider>();
    }


    public void TurretAction(Collider[] colliders)
    {
        ChangeSpeedEnemies(colliders);
    }




    void ChangeSpeedEnemies(Collider[] colliders)
    {
        List<Collider> newEnemiesInArea = new List<Collider>(colliders);

        SlowEnemies(newEnemiesInArea);

        ResetSpeedLeftEnemies(newEnemiesInArea);

        oldEnemiesinArea = newEnemiesInArea;
    }


    void SlowEnemies(List<Collider> newEnemiesCollider)
    {
        foreach (Collider collEnemy in newEnemiesCollider)
        {
            EnemyUnit enemy = collEnemy.GetComponentInParent<EnemyUnit>();
            IEnemyMovement enemyMovement = enemy.GetComponent<IEnemyMovement>();
            enemyMovement.ChangeWalkingStragety(slowingSpeed);
        }
    }

    void ResetSpeedLeftEnemies(List<Collider> newEnemiesCollider)
    {

        if (oldEnemiesinArea == null) return;

        foreach (Collider collEnemy in oldEnemiesinArea)
        {
            //Unit test for if enemy is dead
            if (newEnemiesCollider.Contains(collEnemy)) continue;


            //Unit test this
            if (collEnemy == null) continue;


            EnemyUnit enemy = collEnemy.GetComponentInParent<EnemyUnit>();

            //Could maybe go wrong if frame has two slow towers left one and joined other
            IEnemyMovement enemyMovement = enemy.GetComponent<IEnemyMovement>();
            enemyMovement.ChangeWalkingStragety(normalWalkingSpeed);
        }
    }

    public void Upgrade(UpgradeStrategySO upgrade)
    {
        if (upgrade is EnemyWalkingStragetySO)
        {
            slowingSpeed = (EnemyWalkingStragetySO)upgrade;
        }
    }

}
