using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Done
public class GridSystemVisuals : MonoBehaviour
{
    [SerializeField] GameObject gridVisualPrefab;

    GridObjectVisual[,] gridVisualsObjectsList;


    [SerializeField] GridSystem system;

    private int width;
    private int height;
    private float gridSize;

    public void Start()
    {



        system = GridSystem.Instance;

        width = system.GetWidth();
        height = system.GetHeight();
        gridSize = system.GetGridSize();


        gridVisualsObjectsList = new GridObjectVisual[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                GridPosition gridPosition = new GridPosition(x, z);

                Vector3 position = new Vector3(gridPosition.x, 0, gridPosition.z);
                position = gridSize * position;


                GameObject gride = Instantiate(gridVisualPrefab, position, Quaternion.identity);
                gride.GetComponent<GridObjectVisual>().SetText(gridPosition.ToString());
               // gride.GetComponent<GridObjectVisual>().GridPosition = gridPosition;
                gridVisualsObjectsList[x, z] = gride.GetComponent<GridObjectVisual>();
            }
        }

        DisableAllGridVisuals();

    }

    public void EnableGridVisuals(List<GridPosition> positions)
    {
        foreach (GridPosition position in positions)
        {
            gridVisualsObjectsList[position.x, position.z].gameObject.SetActive(true);
        }
    }


    public void EnabePlaceableGridVisuals()
    {
        EnableGridVisuals(system.GetPlaceablePositions());
    }

    public void DisableAllGridVisuals()
    {
        foreach (GridObjectVisual visual in gridVisualsObjectsList)
        {
            visual.gameObject.SetActive(false);
        }
    }
}
