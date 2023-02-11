using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyMovement
{
    void ChangeWalkingStragety(EnemyWalkingStragetySO stragety);

    void UpdateMovement();

    float GetDistanceToEnd();
}
