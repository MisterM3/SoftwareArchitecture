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

    GridObjectVisual[,] gridVisulaObjectsList;

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
        gridVisulaObjectsList= new GridObjectVisual[width,height];
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
                    gride.GetComponent<GridObjectVisual>().GridPosition = gridPosition;
                gridVisulaObjectsList[x, y] = gride.GetComponent<GridObjectVisual>();
                //gridVisual.SetText(gridPosition.ToString()); 
            }
        }

        DisableAllGridVisuals();
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

       // if (worldPosition.x < 0 || worldPosition.y < 0) return new GridPosition(-1,-1);
        return new GridPosition((int)(worldPosition.x / gridSize), (int)(worldPosition.z / gridSize));
    }

    public void AddObjectAtGridPosition(IGridObject gridObject, GridPosition position)
    {
        GameObject gridObjectPrefab = Instantiate(gridObject.GetGameObject(), GridToWorldPosition(position), Quaternion.identity);
        IGridObject gridObjectIn = gridObjectPrefab.GetComponent<IGridObject>();
        gridObjectIn.gridPosition = position;
        gridObjects[position.x, position.y] = gridObjectIn;
        
    }

    public bool TryAddObjectAtGridPosition(IGridObject gridObject, GridPosition position)
    {
        if (gridObjects[position.x, position.y] != null)
        {
            Debug.LogError("Already a building on this position!");
            return false;
        }

        AddObjectAtGridPosition(gridObject, position);
        return true;
    }

    public IGridObject GetBuildingAtGridPosition(GridPosition position) 
    {
        return gridObjects[position.x, position.y];
    }

    public List<GridPosition> GetPlaceablePositions()
    {
        List<GridPosition> positions = new List<GridPosition>();


        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (gridObjects[x, y] != null) continue;

                GridPosition pos = new GridPosition(x, y);
                positions.Add(pos);
            }
        }
        return positions;
    }


    public void EnableGridVisuals(List<GridPosition> positions)
    {
        foreach(GridPosition position in positions)
        {
            gridVisulaObjectsList[position.x, position.y].gameObject.SetActive(true);
        }
    }

    public void EnabePlaceableGridVisuals()
    {
        EnableGridVisuals(GetPlaceablePositions());
    }

    public void DisableAllGridVisuals()
    {
        foreach(GridObjectVisual visual in gridVisulaObjectsList)
        {
            visual.gameObject.SetActive(false);
        }
    }


    //Does not destroy it yet only sets list position to null!!
    public void DesroyGridObjectAtLocation(GridPosition position)
    {
        gridObjects[position.x, position.y] = null;
    }


}
