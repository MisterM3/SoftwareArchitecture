using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StandardBullet : MonoBehaviour
{
    public float speed = 1f;

    public int damage { get; set; }
    public Vector3 direction { get; set; }

    [SerializeField] Collider bulletCollider;

    private bool alreadyHitAnything = false;

    public void Move()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    public abstract void OnHit(EnemyUnit enemy);
    

    // Update is called once per frame
    void Update()
    {
        Move();
    }


    void OnTriggerEnter(Collider col)
    {
        if (alreadyHitAnything) return;

        if (col.tag == "Enemy")
        {
            EnemyUnit enemy = col.gameObject.GetComponentInParent<EnemyUnit>();
            OnHit(enemy);
            Destroy(this.gameObject);
        }
        
    }
}
