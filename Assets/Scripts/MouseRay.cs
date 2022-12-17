using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRay : MonoBehaviour
{
    //Camera MainCamera = Camera.main;

    [SerializeField] LayerMask groundLayer;
    [SerializeField] GridSystem system;
    [SerializeField] GameObject gridGameObject;
    IGridObject gridObject;

    public void Start()
    {
        gridObject = gridGameObject.GetComponent<IGridObject>();
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
            system.AddObjectAtGridPosition(gridObject, position);
        }
    }
}
