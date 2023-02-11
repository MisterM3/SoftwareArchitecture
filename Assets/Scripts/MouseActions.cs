using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MouseActions : MonoBehaviour
{


    [SerializeField] GameObject UpgradeUI;

    IGridObject gridObject;
    private int costTower = 0;

    private GridSystem system;
    // Start is called before the first frame update
    void Start()
    {
        system = GridSystem.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GridPosition position = system.WorldToGridPosition(MouseRay.Instance.GetMousePosition());

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

    //Adds a new building that can be placed
    public void NewBuilding(IGridObject grid, int costTower)
    {
        gridObject = grid;
        this.costTower = costTower;
    }

    void BuildBuilding(GridPosition position)
    {
        if (!MoneyManager.Instance.HasEnoughMoney(costTower))
        {
            Debug.LogWarning("Not Enough Money to buy tower");
            return;
        }

        if (!system.TryAddBuildingAtGridPosition(gridObject, position)) return;
        MoneyManager.Instance.SpendMoney(costTower);

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
        UpgradeButtons button = UpgradeUI.GetComponent<UpgradeButtons>();
        //Change so certain can be upgraded
        button.ResetButtons((Turret)building);

    }
}
