using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEBullet : StandardBullet
{
    public override void OnHit(EnemyUnit enemy)
    {
        Collider[] enemiesColliders = Physics.OverlapSphere(this.gameObject.transform.position, 1, 1 << 3);

        foreach (Collider enemyCollider in enemiesColliders)
        {
            Debug.Log(enemyCollider.gameObject.name);
            EnemyUnit enemyCol = enemyCollider.GetComponentInParent<EnemyUnit>();

            if (enemyCol == null) continue;

            enemyCol.TakeDamage(damage);

        }
    }
}
