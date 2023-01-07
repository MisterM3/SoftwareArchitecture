using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : MonoBehaviour
{
    [SerializeField] int health = 100;
    [SerializeField] float speed = 1.0f;
    [SerializeField] int money = 10;

    GridSystem system;

    Queue<GridPosition> enemyPath;

    private Vector3 walkToPoint;


    //Distance to end based on amount of nodes need to walk
    private float distanceToEnd;

    

    public void Awake()
    {
        Debug.Log("Stop");
        system = GameObject.FindObjectOfType<GridSystem>();
        enemyPath = new Queue<GridPosition>(EnemyPathManager.Instance.GetEnemyPath());
        NextPathPoint();
    }


    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    //Rewrite for generic type <T>
    public void SetPath(Queue<GridPosition> pEnemyPath)
    {
        enemyPath = pEnemyPath;
    }


    public void Update()
    {
        //Debug.Log(walkToPoint);
        if (walkToPoint == null) return;
       // Debug.Log(Vector3.Distance(walkToPoint, transform.position));
        if (Vector3.Distance(walkToPoint, transform.position) < 0.01f)
        {
            NextPathPoint();
            distanceToEnd = enemyPath.Count;
        }
        else
        {
            //Calculate the distance between where you are and the current node
            distanceToEnd = (enemyPath.Count - 1) + Vector3.Distance(walkToPoint, transform.position);

            Vector3 between = ( walkToPoint - transform.position).normalized;
           // Debug.Log(between);
            transform.position += between * speed * Time.deltaTime;
        }
    }

    private void NextPathPoint()
    {
        if (enemyPath.TryDequeue(out GridPosition gridPointNextPoint))
        {
            walkToPoint = system.GridToWorldPosition(gridPointNextPoint) + new Vector3(0.5f, 0, 0.5f);
        }
        else Debug.LogWarning("No more points in queue!");
    }

    public float GetDistanceToEnd()
    {
        return distanceToEnd;
    }
}
