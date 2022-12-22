using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour, IGridObject
{

    [SerializeField] GridSystem system;

    [SerializeField] GameObject bullet;

    public GridObjectVisual gridObjectVisual { get; set; }

    public GridPosition gridPosition { get; private set; }

    private EnemyUnit targetedEnemy = null;

    private float radius = 4;

    private float cooldown = 1f;

    public GameObject GetGameObject()
    {
        return this.gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        gridPosition = system.WorldToGridPosition(this.transform.position);
        InvokeRepeating("ShootingCycle", 0f, cooldown);
    }

    // Update is called once per frame

    void Update()
    {
    }



    void ShootingCycle()
    {
        Aiming();

        if (targetedEnemy == null) return;

        Shoot(targetedEnemy.transform.position);
        Debug.Log("Shooting");
    }

    //Rewrite this dogshit mess for better detection, maybe a manager (but hard if something breaks)
    void Aiming()
    {
        GameObject[] list = GameObject.FindGameObjectsWithTag("Enemy");

        List<GameObject> enemies = new List<GameObject>(list);



        float firstEnemy = float.MaxValue;

        foreach (GameObject enemy in enemies)
        {
            Debug.Log(system.MiddleGridToWorldPosition(gridPosition));
            Debug.Log(Vector3.Distance(enemy.transform.position, system.MiddleGridToWorldPosition(gridPosition)));

            if (Vector3.Distance(enemy.transform.position, system.GridToWorldPosition(gridPosition)) < radius)
            {
                EnemyUnit enemyUnit = enemy.GetComponent<EnemyUnit>();

                if (enemyUnit.GetDistanceToEnd() < firstEnemy)
                {
                    firstEnemy = enemyUnit.GetDistanceToEnd();
                    targetedEnemy = enemyUnit;
                }
            }
        }
    }

    void Shoot(Vector3 shootToLocation)
    {
        Vector3 direction = shootToLocation - system.MiddleGridToWorldPosition(gridPosition);
        direction.Normalize();

        Vector3 spawnPosition = system.MiddleGridToWorldPosition(gridPosition);
        spawnPosition += new Vector3(0, 0.5f, 0);
        GameObject bulletObject = Instantiate(bullet, spawnPosition, Quaternion.identity);
        IBullet bul = bulletObject.GetComponent<IBullet>();
        bul.direction = direction;


    }


}
