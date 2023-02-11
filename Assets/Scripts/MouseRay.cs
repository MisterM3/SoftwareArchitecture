using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MouseRay : MonoBehaviour
{
    public static MouseRay Instance;

    [SerializeField] LayerMask groundLayer;
 


    public void Awake()
    {
        Instance = this;
    }



    public Vector3 GetMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, groundLayer);

        return hit.point;
    }

}
