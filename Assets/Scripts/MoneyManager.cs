using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{

    public static MoneyManager Instance { get; private set; }

    [SerializeField] int playerMoney = 500;

    public event EventHandler<int> OnMoneyChange;


    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Already a MoneyManager in scene destroying: " + gameObject);
            Destroy(this);
            return;
        }

        Instance = this;
    }

    void Start()
    {
        EnemyUnit.OnAnyEnemyKilled += EnemyUnit_OnAnyEnemyKilled;
    }


    private void EnemyUnit_OnAnyEnemyKilled(object sender, int e)
    {
        playerMoney += e;
        OnMoneyChange?.Invoke(this, playerMoney);
    }


    public int GetMoney()
    {
        return playerMoney;
    }

    public bool HasEnoughMoney(int amount)
    {
        return playerMoney >= amount;
    }
    public void SpendMoney(int amount)
    {
        playerMoney -= amount;
        OnMoneyChange?.Invoke(this, playerMoney);
    }

    public bool TrySpendMoney(int amount)
    {
        if (HasEnoughMoney(amount))
        {

            SpendMoney(amount);
            return true;
        }

        return false;

    }


    private void OnDestroy()
    {
        EnemyUnit.OnAnyEnemyKilled -= EnemyUnit_OnAnyEnemyKilled;
    }
}
