using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//1st pass change mouse getting
public class BuyTower : MonoBehaviour
{


    [SerializeField] int cost;
    [SerializeField] GameObject gridGameObject;
    IGridObject gridObject;


    MouseActions mouse;

    // Start is called before the first frame update
    void Start()
    {
        gridObject = gridGameObject.GetComponent<IGridObject>();
       mouse = GameObject.FindObjectOfType<MouseActions>();
    }

    public void BuyTowers()
    {
        mouse.NewBuilding(gridObject, cost);
    }

    public int GetCost()
    {
        return cost;
    }
}
