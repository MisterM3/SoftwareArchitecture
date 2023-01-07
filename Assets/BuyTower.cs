using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyTower : MonoBehaviour
{


    [SerializeField] int cost;
    [SerializeField] GameObject GridGameObject;
    IGridObject GridObject;


    MouseRay mouse;

    // Start is called before the first frame update
    void Start()
    {
        GridObject = GridGameObject.GetComponent<IGridObject>();
       mouse = GameObject.FindObjectOfType<MouseRay>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuyTowers()
    {
        mouse.NewGridObject(GridObject);
    }
}
