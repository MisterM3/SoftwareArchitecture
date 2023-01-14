using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{

    public static GameStateManager Instance { get; private set; }
    [SerializeField] int playerHealh = 100;
    [SerializeField] int playerMoney = 500;

    [SerializeField] int waveNumber = 1;


    //Maybe change
    public event EventHandler<int> OnHealthChange;
    public event EventHandler<int> OnMoneyChange;

    public enum GameState { StartGame, BeforeWave, DuringWave, AfterWaveSpawn, GameOver};

    [SerializeField] GameState state;



    public void Start()
    {

        if (Instance != null)
        {
            Debug.LogError("Already a GameStateManager in scene destroying: " + gameObject);
            Destroy(this);
            return;
        }

        Instance = this;
        EnemyUnit.OnAnyEnemyReachEnd += EnemyUnit_OnAnyEnemyReachEnd;
        EnemyUnit.OnAnyEnemyKilled += EnemyUnit_OnAnyEnemyKilled;
    }




    private void EnemyUnit_OnAnyEnemyKilled(object sender, int e)
    {
        playerMoney += e;
        OnMoneyChange?.Invoke(this, playerMoney);
    }

    private void EnemyUnit_OnAnyEnemyReachEnd(object sender, System.EventArgs e)
    {
        PlaterHealthDown();
    }

   private void PlaterHealthDown()
    {
        playerHealh -= 1;
        OnHealthChange?.Invoke(this, playerHealh);
    }

    public int GetMoney()
    {
        return playerMoney;
    }

    public bool HasEnoughMoney(int amount)
    {
        return playerMoney >= amount;
    }

    public bool TrySpendMoney(int amount)
    {
        if (HasEnoughMoney(amount)) {

            SpendMoney(amount);
            return true;
        }

        return false;
        
    }

    public void SpendMoney(int amount)
    {
        playerMoney -= amount;
        OnMoneyChange?.Invoke(this, playerMoney);
    }
}
