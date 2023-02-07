using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//1st pass change mouse getting
public class BuyTower : MonoBehaviour
{


    [SerializeField] int cost;
    [SerializeField] GameObject GridGameObject;
    IGridObject GridObject;


    MouseActions mouse;

    // Start is called before the first frame update
    void Start()
    {
        GridObject = GridGameObject.GetComponent<IGridObject>();
       mouse = GameObject.FindObjectOfType<MouseActions>();
    }

    public void BuyTowers()
    {
        mouse.NewBuilding(GridObject, cost);
    }

    public int getCost()
    {
        return cost;
    }
}
