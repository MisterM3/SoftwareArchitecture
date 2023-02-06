using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyMovement
{
   // EnemyWalkingStragetySO walkingStragety { get; set; }
    void ChangeStragety(EnemyWalkingStragetySO stragety);

    void UpdateMovement();

    float GetDistanceToEnd();

}
