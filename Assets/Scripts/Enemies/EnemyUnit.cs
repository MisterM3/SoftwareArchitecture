using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(IEnemyMovement))]
public class EnemyUnit : MonoBehaviour
{
    [SerializeField] int health = 100;
    [SerializeField] int money = 10;

    public event EventHandler OnHealthChanged;

    public static event EventHandler OnAnyEnemyReachEnd;

    public static event EventHandler<int> OnAnyEnemyKilled;

    private IEnemyMovement _movement;

    public void Start()
    {
        _movement = GetComponent<IEnemyMovement>();
    }


    public void Update()
    {
        _movement.UpdateMovement();
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        OnHealthChanged?.Invoke(this, EventArgs.Empty);
        if (health <= 0)
        {
            OnAnyEnemyKilled?.Invoke(this, money);
            Destroy(this.gameObject);
        }
    }

    public void EnemyReachedEnd()
    {
        OnAnyEnemyReachEnd?.Invoke(this, EventArgs.Empty);
    }

    public float GetDistanceToEnd()
    {
        if (_movement == null) return float.MaxValue;
        return _movement.GetDistanceToEnd();
    }


    public int GetHealth()
    {
        return health;
    }    
}
