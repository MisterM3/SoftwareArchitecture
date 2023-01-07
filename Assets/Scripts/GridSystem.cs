using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    [SerializeField] GameObject prefabGridObject;
    [SerializeField] int width = 10;
    [SerializeField] int height = 10;
    [SerializeField] float gridSize = 1.0f;

    IGridObject[,] gridObjects;

    public static GridSystem Instance;


    public void Start()
    {

        if (Instance != null)
        {
            Destroy(this);
            Debug.Log("Already a GridSystem, destroying:" + name);
            return;
        }

        Instance = this;

        gridObjects = new IGridObject[width,height];
        for(int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GridPosition gridPosition = new GridPosition(x, y);

                Vector3 position = new Vector3(gridPosition.x, 0, gridPosition.y);
                position = gridSize * position;
                //prefabGridObject.gridPosition = gridPosition;
                //GridObjectVisual gridVisual = 
                  GameObject gride =  Instantiate(prefabGridObject, position, Quaternion.identity);
                gride.GetComponent<GridObjectVisual>().SetText(gridPosition.ToString());
                //gridVisual.SetText(gridPosition.ToString()); 
            }
        }
    }

    public Vector3 GridToWorldPosition(GridPosition gridPosition)
    {
        return gridSize * new Vector3(gridPosition.x, 0, gridPosition.y);
    }

    public Vector3 MiddleGridToWorldPosition(GridPosition gridPosition)
    {
        return gridSize * new Vector3(gridPosition.x, 0, gridPosition.y) + new Vector3(gridSize * 0.5f, 0, gridSize * 0.5f);
    }

    public GridPosition WorldToGridPosition(Vector3 worldPosition)
    {
        return new GridPosition((int)(worldPosition.x / gridSize), (int)(worldPosition.z / gridSize));
    }

    public void AddObjectAtGridPosition(IGridObject gridObject, GridPosition position)
    {
        if (gridObjects[position.x,position.y] != null)
        {
            Debug.LogError("Already a building on this position!");
            return;
        }

        GameObject gridObjectPrefab = Instantiate(gridObject.GetGameObject(), GridToWorldPosition(position), Quaternion.identity);
        IGridObject gridObjectIn = gridObjectPrefab.GetComponent<IGridObject>();
        gridObjectIn.gridPosition = position;
        gridObjects[position.x, position.y] = gridObjectIn;
        
    }


}
