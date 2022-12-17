using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class RangeRenderer : MonoBehaviour
{

    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private int positionCount = 360;
    [SerializeField] private float lineWidth = .1f;
    [SerializeField] private float range = 2.0f;

    // Start is called before the first frame update
    void Start()
    {


        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = positionCount;
        lineRenderer.startWidth= lineWidth;
        float dAngle = 360.0f / positionCount;


        for(int i = 0; i < positionCount; i++)
        {
            float angle = (dAngle * i) * Mathf.Deg2Rad;

            Vector3 position = new Vector3(Mathf.Cos(angle) * range, 0, Mathf.Sin(angle) * range);

            lineRenderer.SetPosition(i, position + transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
