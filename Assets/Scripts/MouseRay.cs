using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class MouseRay : MonoBehaviour
{
    //Camera MainCamera = Camera.main;

    [SerializeField] LayerMask groundLayer;
    [SerializeField] GridSystem system;
    [SerializeField] GameObject gridGameObject;

    [SerializeField] GameObject UpgradeUI;
    IGridObject gridObject;

    public int costTower = 0;



    public void Start()
    {
        gridObject = gridGameObject.GetComponent<IGridObject>();
    }

    public void NewGridObject(IGridObject grid)
    {
        gridObject = grid;
    }

    public void Update()
    {
        //Debug.Log(Camera.main.ScreenPointToRay(Input.mousePosition));
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        


        Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, groundLayer);
       // Debug.Log(system.WorldToGridPosition(hit.point));
        Debug.DrawRay(ray.origin, ray.direction);

        if (Input.GetMouseButtonDown(0))
        {
            GridPosition position = system.WorldToGridPosition(hit.point);

            //A tower has been pressed so a tower will be build
            if (gridObject != null)
            {
                BuildBuilding(position);
                system.DisableAllGridVisuals();
                gridObject = null;
            }

            //Check if there is a tower on the grid, if there is open the upgrade menu
            else UpgradeMenu(position);
        }
    }


    void BuildBuilding(GridPosition position) 
    {
        if (!GameStateManager.Instance.HasEnoughMoney(costTower))
        {
            Debug.LogWarning("Not Enough Money to buy tower");
            return;
        }
       
        if (!system.TryAddObjectAtGridPosition(gridObject, position))
        {

            return;
        }

        GameStateManager.Instance.SpendMoney(costTower);

    }


    void UpgradeMenu(GridPosition position)
    {
        IGridObject building = system.GetBuildingAtGridPosition(position);

        if (building == null)
        {
            UpgradeUI.SetActive(false);
            return;
        }


        if (!(building is Turret)) return;

        UpgradeUI.SetActive(true);
        UpgradeButton button = UpgradeUI.GetComponent<UpgradeButton>();
        //Change so certain can be upgraded
        button.ResetButtons((Turret)building);

    }
}
