using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Done
public class GridSystem : MonoBehaviour
{
    [SerializeField] int width = 10;
    [SerializeField] int height = 10;
    [SerializeField] float gridSize = 1.0f;


    private GenericGrid<IGridObject> grid;
    private GridSystemVisuals gridVisuals;


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


        grid = new GenericGrid<IGridObject>(width, height, gridSize);

        gridVisuals = GetComponent<GridSystemVisuals>();

        if (gridVisuals == null)
        {
            Debug.LogError("NO GRIDVISUALS ATTACHED TO GRIDSYSTEM!!");
        }
        
    }


    public int GetWidth()
    {
        return width;
    }
    public int GetHeight()
    {
        return height;
    }    
    public float GetGridSize()
    {
        return gridSize;
    }

    public Vector3 GridToWorldPosition(GridPosition gridPosition) => grid.GridToWorldPosition(gridPosition);
    public Vector3 MiddleGridToWorldPosition(GridPosition gridPosition) => grid.MiddleGridToWorldPosition(gridPosition);
    public GridPosition WorldToGridPosition(Vector3 worldPosition) => grid.WorldToGridPosition(worldPosition);
    public IGridObject GetBuildingAtGridPosition(GridPosition position) => grid.GetObjectAtGridPosition(position);

    public void AddBuildingAtGridPosition(IGridObject gridObject, GridPosition position)
    {
        GameObject gridObjectPrefab = Instantiate(gridObject.GetGameObject(), GridToWorldPosition(position), Quaternion.identity);
        IGridObject gridObjectIn = gridObjectPrefab.GetComponent<IGridObject>();
        gridObjectIn.gridPosition = position;


        //Adds the object to the grid
        grid.AddObjectAtGridPosition(gridObjectIn, position);
    }

    public bool TryAddBuildingAtGridPosition(IGridObject gridObject, GridPosition position)
    {
        if (GetBuildingAtGridPosition(position) != null)
        {
            Debug.LogError("Already a building on this position!");
            return false;
        }

        AddBuildingAtGridPosition(gridObject, position);
        return true;
    }

    
    
    //Here and not in GenericGrid as the generic doesn't have to be null
    public List<GridPosition> GetPlaceablePositions()
    {
        List<GridPosition> positions = new List<GridPosition>();

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GridPosition pos = new GridPosition(x, y);
                if (GetBuildingAtGridPosition(pos) != null) continue;

                positions.Add(pos);
            }
        }
        return positions;
    }

    //Sets the position to null (Empty)
    public void RemoveGridObjectAtLocation(GridPosition position) => grid.RemoveObjectAtLocation(position);



    //Visuals passThrough
    public void EnableGridVisuals(List<GridPosition> positions) => gridVisuals.EnableGridVisuals(positions);
    public void EnabePlaceableGridVisuals() => gridVisuals.EnabePlaceableGridVisuals();
    public void DisableAllGridVisuals() => gridVisuals.DisableAllGridVisuals();

}
