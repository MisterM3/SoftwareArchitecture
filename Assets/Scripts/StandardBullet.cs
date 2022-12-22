using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardBullet : MonoBehaviour, IBullet
{
    public float speed = 1f;

    public Vector3 direction { get; set; }

    public void Move()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    public void OnHit()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
}
