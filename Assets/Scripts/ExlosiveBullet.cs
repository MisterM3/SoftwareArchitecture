using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExlosiveBullet : MonoBehaviour
{
    public float speed = 1f;
    public Vector3 direction { get; set; }

    [SerializeField] Collider bulletCollider;

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

    void OnTriggerEnter(Collider col)
    {

        Debug.Log("Hit");
        if (col.tag == "Enemy")
        {
            EnemyUnit enemy = col.gameObject.GetComponentInParent<EnemyUnit>();
            
            foreach(GameObject enemies in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                if (Vector3.Distance(this.transform.position, enemies.transform.position) < 0.2f)
                {

                }
            }

            Destroy(this.gameObject);
        }

    }
}
