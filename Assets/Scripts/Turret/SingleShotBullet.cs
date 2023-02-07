using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleShotBullet : StandardBullet
{
    public override void OnHit(EnemyUnit enemy)
    {
        enemy.TakeDamage(damage);
    }
}
