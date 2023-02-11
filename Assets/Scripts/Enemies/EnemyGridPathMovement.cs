using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyUnit))]
public class EnemyGridPathMovement : MonoBehaviour, IEnemyMovement
{

    [SerializeField] private float speed = 1.0f;

    [SerializeField] private EnemyWalkingStragetySO walkingStragety;

    private Queue<GridPosition> enemyPath;

    private Vector3 walkToPoint;

    //Distance to end based on amount of nodes need to walk
    private float distanceToEnd;

    public void Awake()
    {

        SetPath(EnemyPathManager.Instance.GetEnemyPath());
        NextPathPoint();
    }


    public void SetPath(Queue<GridPosition> pEnemyPath)
    {
        enemyPath = new Queue<GridPosition>(pEnemyPath);
    }

    public void UpdateMovement()
    {
        //EndPoint 
        if (walkToPoint == null) return;


        if (Vector3.Distance(walkToPoint, transform.position) < 0.01f)
        {
            NextPathPoint();
            distanceToEnd = enemyPath.Count;
        }
        else
        {
            //Calculate the distance between where you are and the current node
            distanceToEnd = (enemyPath.Count - 1) + Vector3.Distance(walkToPoint, transform.position);

            Vector3 between = (walkToPoint - transform.position).normalized;
            transform.position += between * (speed * walkingStragety.walkMultiplier) * Time.deltaTime;
        }
    }

    private void NextPathPoint()
    {
        if (enemyPath.TryDequeue(out GridPosition gridPointNextPoint))
        {
            walkToPoint = GridSystem.Instance.GridToWorldPosition(gridPointNextPoint) + new Vector3(0.5f, 0, 0.5f);
        }
        else
        {
            EnemyUnit unit = GetComponent<EnemyUnit>();
            unit.EnemyReachedEnd();

            Debug.LogWarning("No more points in queue!");
            Destroy(this.gameObject);
        }
    }

    public void ChangeWalkingStragety(EnemyWalkingStragetySO stragety)
    {
        walkingStragety = stragety;
    }


    public float GetDistanceToEnd()
    {
        return distanceToEnd;
    }
}
