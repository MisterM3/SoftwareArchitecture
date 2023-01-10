using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{

    [SerializeField] int playerHealh = 100;
    [SerializeField] int playerMoney = 500;

    [SerializeField] int waveNumber = 1;

    public enum GameState { StartGame, BeforeWave, DuringWave, GameOver};

    [SerializeField] GameState state;



    public void Start()
    {
        EnemyUnit.OnAnyEnemyReachEnd += EnemyUnit_OnAnyEnemyReachEnd;
    }

    private void EnemyUnit_OnAnyEnemyReachEnd(object sender, System.EventArgs e)
    {
        PlaterHealthDown();
    }

   private void PlaterHealthDown()
    {
        playerHealh -= 1;
    }
}
