using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{

    public static GameStateManager Instance { get; private set; }
    [SerializeField] int playerHealh = 100;

    [SerializeField] int waveIndex = -1;

    [SerializeField] int maxWaveIndex = 4;


    //Maybe change
    public event EventHandler<int> OnHealthChange;



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

        if (waveIndex >= maxWaveIndex)
        {
            currentstate = GameState.Won;
            SendEvents();
            return;
        }


        currentstate = GameState.BeforeWave;
        SendEvents();
    }

    public void StartWave() {
        waveIndex++;
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

    private void EnemyUnit_OnAnyEnemyReachEnd(object sender, System.EventArgs e)
    {
        PlaterHealthDown();
    }

    private void PlaterHealthDown()
    {
        playerHealh -= 1;
        if (playerHealh <= 0)
        {
            GameOver();
            playerHealh = 0;
        }

        OnHealthChange?.Invoke(this, playerHealh);

    }
}
