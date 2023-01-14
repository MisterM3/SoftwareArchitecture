using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour, IGridObject, IUpgradable
{

  //  [SerializeField] GridSystem system;

    [SerializeField] GameObject bullet;

    public GridObjectVisual gridObjectVisual { get; set; }

    public GridPosition gridPosition { get; set; }



    private EnemyUnit targetedEnemy = null;

    public event EventHandler OnTurretUpgrade;


    [SerializeField] private TurretRangeStragetySO turretRangeStragety;

    [SerializeField] private TurretShootSpeedStrategySO shootingSpeedStragety;

    public GameObject GetGameObject()
    {
        return this.gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        gridPosition = GridSystem.Instance.WorldToGridPosition(this.transform.position);
        StartCoroutine("ShootingCycle");
    }

    // Update is called once per frame

    void Update()
    {
    }






    IEnumerator ShootingCycle()
    {
        while (true)
        {
            Aiming();

            if (targetedEnemy != null)
            {
                Shoot(targetedEnemy.transform.position);
                Debug.Log("Shooting");
                yield return new WaitForSeconds(shootingSpeedStragety.shootSpeed);
            }
            else yield return new WaitForSeconds(0.1f);


        }
    }

    //Rewrite this dogshit mess for better detection, maybe a manager (but hard if something breaks)
    void Aiming()
    {
        EnemyUnit[] list = GameObject.FindObjectsOfType<EnemyUnit>();

        List<EnemyUnit> enemies = new List<EnemyUnit>(list);



        float firstEnemy = float.MaxValue;

        foreach (EnemyUnit enemy in enemies)
        {
            Debug.Log(GridSystem.Instance.MiddleGridToWorldPosition(gridPosition));
            Debug.Log(Vector3.Distance(enemy.transform.position, GridSystem.Instance.MiddleGridToWorldPosition(gridPosition)));

            if (Vector3.Distance(enemy.transform.position, GridSystem.Instance.GridToWorldPosition(gridPosition)) <= turretRangeStragety.turretRange)
            {

                if (enemy.GetDistanceToEnd() < firstEnemy)
                {
                    firstEnemy = enemy.GetDistanceToEnd();
                    targetedEnemy = enemy;
                }
            }
        }
    }

    void Shoot(Vector3 shootToLocation)
    {
        Vector3 direction = shootToLocation - GridSystem.Instance.MiddleGridToWorldPosition(gridPosition);
        direction.Normalize();

        Vector3 spawnPosition = GridSystem.Instance.MiddleGridToWorldPosition(gridPosition);
        spawnPosition += new Vector3(0, 0.5f, 0);
        GameObject bulletObject = Instantiate(bullet, spawnPosition, Quaternion.identity);
        IBullet bul = bulletObject.GetComponent<IBullet>();
        bul.direction = direction;


    }


    public void ChangeShootSpeedStragety(TurretShootSpeedStrategySO shootSpeedSO)
    {
        shootingSpeedStragety = shootSpeedSO;
    }

    public void Upgrade()
    {
        OnTurretUpgrade?.Invoke(this, EventArgs.Empty);
    }


}
