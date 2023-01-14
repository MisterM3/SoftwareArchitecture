using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{

    public static GameStateManager Instance { get; private set; }
    [SerializeField] int playerHealh = 100;
    [SerializeField] int playerMoney = 500;

    [SerializeField] int waveNumber = -1;


    //Maybe change
    public event EventHandler<int> OnHealthChange;
    public event EventHandler<int> OnMoneyChange;



    //Rechange into eventhandler with info maybe?
    public event EventHandler OnBeforeWaveStart;
    public event EventHandler<int> OnDuringWaveStart;
    public event EventHandler OnAfterWaveStart;
    public event EventHandler OnGameOverStart;
    public event EventHandler OnWonStart;

    public enum GameState { StartGame, BeforeWave, DuringWave, AfterWaveSpawn, GameOver, Won };

    [SerializeField] GameState currentstate = GameState.StartGame;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Already a GameStateManager in scene destroying: " + gameObject);
            Destroy(this);
            return;
        }

        Instance = this;
    }

    public void Start()
    {

        EnemyUnit.OnAnyEnemyReachEnd += EnemyUnit_OnAnyEnemyReachEnd;
        EnemyUnit.OnAnyEnemyKilled += EnemyUnit_OnAnyEnemyKilled;

        EnemySpawnManager.Instance.OnWaveCompleted += Instance_OnWaveCompleted;
        ManageTimer.Instance.OnTimerComplete += ManageTimer_OnTimerComplete;


        StartGame();
    }

    private void ManageTimer_OnTimerComplete(object sender, EventArgs e)
    {
        if (currentstate != GameState.GameOver)
        StartWave();
    }

    private void Instance_OnWaveCompleted(object sender, EventArgs e)
    {
        if (currentstate != GameState.GameOver)
            EndWave();
    }

    public void StartGame() {

        currentstate = GameState.BeforeWave;
        SendEvents();
    }

    public void EndWave() {
        currentstate = GameState.BeforeWave;
        SendEvents();
    }

    public void StartWave() {
        waveNumber++;
        currentstate = GameState.DuringWave;
        SendEvents();
    }

    public void WaveSpawnOver() {
        currentstate = GameState.AfterWaveSpawn;
        SendEvents();
    }
    public void GameOver() {
        currentstate = GameState.GameOver;
        SendEvents();
    }

    private void SendEvents() { 
    
        switch (currentstate) {
            case GameState.BeforeWave:
                OnBeforeWaveStart?.Invoke(this, EventArgs.Empty);
                Debug.LogWarning("te");
                break;
            case GameState.DuringWave:
                Debug.LogWarning("sendEvent");
                OnDuringWaveStart?.Invoke(this, waveNumber);
                break;
            case GameState.AfterWaveSpawn:
                OnAfterWaveStart?.Invoke(this, EventArgs.Empty);
                break;
            case GameState.GameOver:
                OnGameOverStart?.Invoke(this, EventArgs.Empty);
                break;
            case GameState.Won:
                OnWonStart?.Invoke(this, EventArgs.Empty);
                break;
        }
    }










    //MOINEY WISE PUT IN DIFFERENT MANAGER

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

        if (playerHealh <= 0) GameOver();
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
