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

        SpawnPathOnGrid();
    }


    //Builds a path based on the put in gridpositions
    private void SpawnPathOnGrid()
    {


        //Puts the spawner at the start of the path
        IGridObject spawnerObject = spawnerPrefab.GetComponent<IGridObject>();

        system.TryAddBuildingAtGridPosition(spawnerObject, enemyPath[0]);

        //The rest of the path is a path object
        IGridObject gridObject = pathPrefab.GetComponent<IGridObject>();
        for(int i = 1; i < enemyPath.Count; i++)
        {
            system.TryAddBuildingAtGridPosition(gridObject, enemyPath[i]);
        }
    }


    public Queue<GridPosition> GetEnemyPath()
    {
        return queuePath;
    }
}
