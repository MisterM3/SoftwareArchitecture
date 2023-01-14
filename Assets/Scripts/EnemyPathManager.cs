using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathManager : MonoBehaviour
{
    public static EnemyPathManager Instance { get; private set; }


    [SerializeField] GameObject spawnerPrefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] GridSystem system;
    [SerializeField] List<GridPosition> enemyPath;

    Queue<GridPosition> queuePath;

    // Start is called before the first frame update
    void Start()
    {

        if (Instance != null)
        {
            Debug.LogError("Already a EnemyPathManager in scene destroying: " + gameObject);
            Destroy(this);
            return;
        }

        Instance = this;

        queuePath = new Queue<GridPosition>(enemyPath);


        PathVisual();
    }


    //ReWrite Path so the first element is the start and the last is the ending point
    public void PathVisual()
    {
        queuePath = new Queue<GridPosition>(enemyPath);

        IGridObject gridObject = pathPrefab.GetComponent<IGridObject>();

        IGridObject spawnerObject = spawnerPrefab.GetComponent<IGridObject>();

        system.TryAddObjectAtGridPosition(spawnerObject, enemyPath[0]);

        for(int i = 1; i < enemyPath.Count; i++)
        {
            system.TryAddObjectAtGridPosition(gridObject, enemyPath[i]);
        }
    }


    public Queue<GridPosition> GetEnemyPath()
    {
        return queuePath;
    }
}
