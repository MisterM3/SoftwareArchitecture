using System;
using UnityEngine;
using TMPro;

public class ManageInformationUI : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] TextMeshProUGUI moneyText;

    [SerializeField] TextMeshProUGUI waveText;

    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject gameWonScreen;
    
    // Start is called before the first frame update
    void Start()
    {
        GameStateManager.Instance.OnHealthChange += GameStateManager_OnHealthChange;
        MoneyManager.Instance.OnMoneyChange += MoneyManager_OnMoneyChange;

        GameStateManager.Instance.OnDuringWaveStart += GameStateManager_OnDuringWaveStart;

        GameStateManager.Instance.OnGameOverStart += GameStateManager_OnGameOverStart;

        GameStateManager.Instance.OnWonStart += GameStateManager_OnWonStart;

        ChangeMoneyText(MoneyManager.Instance.GetMoney());
        ChangeHealthText(GameStateManager.Instance.GetHealth());
    }

    private void GameStateManager_OnDuringWaveStart(object sender, int e)
    {
        //e++ because index starts at zero
        waveText.text = "Wave: " + (e+1).ToString();
    }

    private void GameStateManager_OnGameOverStart(object sender, EventArgs e)
    {
        gameOverScreen.SetActive(true);
    }

    private void GameStateManager_OnWonStart(object sender, EventArgs e)
    {
        gameWonScreen.SetActive(true);
    }

    private void MoneyManager_OnMoneyChange(object sender, int e)
    {
        ChangeMoneyText(e);
    }

    private void GameStateManager_OnHealthChange(object sender, int e)
    {
        ChangeHealthText(e);
    }

    private void ChangeMoneyText(int money)
    {
        moneyText.text = "Money: " + money.ToString();
    }
    private void ChangeHealthText(int health)
    {
        healthText.text = "Health: " + health.ToString();
    }

}
