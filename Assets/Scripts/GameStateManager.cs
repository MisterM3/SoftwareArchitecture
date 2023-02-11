using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{

    public static GameStateManager Instance { get; private set; }
    [SerializeField] int playerHealth = 100;

    [SerializeField] int waveIndex = -1;

    [SerializeField] int maxWaveIndex = 4;


    public event EventHandler<int> OnHealthChange;



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

        EnemySpawnManager.Instance.OnWaveCompleted += EnemySpawnManager_OnWaveCompleted;
        ManageRoundTimer.Instance.OnTimerComplete += ManageTimer_OnTimerComplete;


        StartGame();
    }

    private void ManageTimer_OnTimerComplete(object sender, EventArgs e)
    {
        if (currentstate != GameState.GameOver)
        StartWave();
    }

    private void EnemySpawnManager_OnWaveCompleted(object sender, EventArgs e)
    {
        if (currentstate != GameState.GameOver)
            EndWave();
    }

    public void StartGame() {

        currentstate = GameState.BeforeWave;
        SendCurrentStateEvents();
    }

    public void StartWave() {
        waveIndex++;
        currentstate = GameState.DuringWave;
        SendCurrentStateEvents();
    }

    public void EndWave() {

        if (waveIndex >= maxWaveIndex)
        {
            GameWon();
            return;
        }


        currentstate = GameState.BeforeWave;
        SendCurrentStateEvents();
    }

    public void WaveSpawnOver() {
        currentstate = GameState.AfterWaveSpawn;
        SendCurrentStateEvents();
    }
    public void GameOver() {
        currentstate = GameState.GameOver;
        SendCurrentStateEvents();
    }

    public void GameWon()
    {
        currentstate = GameState.Won;
        SendCurrentStateEvents();
    }

    private void SendCurrentStateEvents() { 
    
        switch (currentstate) {
            case GameState.BeforeWave:
                OnBeforeWaveStart?.Invoke(this, EventArgs.Empty);
                break;
            case GameState.DuringWave:
                Debug.LogWarning("sendEvent");
                OnDuringWaveStart?.Invoke(this, waveIndex);
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

    private void EnemyUnit_OnAnyEnemyReachEnd(object sender, EventArgs e)
    {
        PlaterHealthDown();
    }

    private void PlaterHealthDown()
    {
        playerHealth -= 1;
        if (playerHealth <= 0)
        {
            GameOver();
            playerHealth = 0;
        }

        OnHealthChange?.Invoke(this, playerHealth);

    }

    public int GetHealth()
    {
        return playerHealth;
    }

    public void OnDestroy()
    {
        EnemyUnit.OnAnyEnemyReachEnd -= EnemyUnit_OnAnyEnemyReachEnd;

        EnemySpawnManager.Instance.OnWaveCompleted -= EnemySpawnManager_OnWaveCompleted;
        ManageRoundTimer.Instance.OnTimerComplete -= ManageTimer_OnTimerComplete;
    }
}
