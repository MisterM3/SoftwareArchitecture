using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathManager : MonoBehaviour
{
    public static EnemyPathManager Instance { get; private set; }

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

    private void PathVisual()
    {
        IGridObject gridObject = pathPrefab.GetComponent<IGridObject>();

        foreach(GridPosition position in queuePath)
        {
            system.AddObjectAtGridPosition(gridObject, position);
        }
    }


    public Queue<GridPosition> GetEnemyPath()
    {
        return queuePath;
    }
}
