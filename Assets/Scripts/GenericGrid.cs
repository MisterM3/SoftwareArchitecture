using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Done
public class GenericGrid<TGridObject>
{
    private int width;
    private int height;
    private float gridSize;

    TGridObject[,] gridObjects;

    //This genericgrid has null as a standard on position where there is nothing
    public GenericGrid(int width, int height, float gridSize)
    {
        this.width = width;
        this.height = height;
        this.gridSize = gridSize;

        gridObjects = new TGridObject[this.width, this.height];

    }


    public Vector3 GridToWorldPosition(GridPosition gridPosition)
    {
        return gridSize * new Vector3(gridPosition.x, 0, gridPosition.z);
    }

    public Vector3 MiddleGridToWorldPosition(GridPosition gridPosition)
    {
        return GridToWorldPosition(gridPosition) + new Vector3(gridSize * 0.5f, 0, gridSize * 0.5f);
    }

    public GridPosition WorldToGridPosition(Vector3 worldPosition)
    {
        // if (worldPosition.x < 0 || worldPosition.y < 0) return new GridPosition(-1,-1);
        return new GridPosition((int)(worldPosition.x / gridSize), (int)(worldPosition.z / gridSize));
    }

    public void AddObjectAtGridPosition(TGridObject gridObject, GridPosition position)
    {
        gridObjects[position.x, position.z] = gridObject;
    }


    public TGridObject GetObjectAtGridPosition(GridPosition position)
    {
        return gridObjects[position.x, position.z];
    }

    public List<GridPosition> GetPlaceablePositions()
    {
        List<GridPosition> positions = new List<GridPosition>();

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                if (gridObjects[x, z] != null) continue;

                GridPosition pos = new GridPosition(x, z);
                positions.Add(pos);
            }
        }
        return positions;
    }

    public void RemoveObjectAtLocation(GridPosition position)
    {
        //Sets the object at the gridPosition to the default (For references this is null)
        gridObjects[position.x, position.z] = default(TGridObject);
    }
}
