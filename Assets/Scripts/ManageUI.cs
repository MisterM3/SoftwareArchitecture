using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ManageUI : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] TextMeshProUGUI moneyText;

    [SerializeField] TextMeshProUGUI waveText;

    [SerializeField] GameObject gameOverScreen;
    // Start is called before the first frame update
    void Start()
    {
        GameStateManager.Instance.OnHealthChange += GameStateManager_OnHealthChange;
        GameStateManager.Instance.OnMoneyChange += GameStateManager_OnMoneyChange;

        GameStateManager.Instance.OnDuringWaveStart += Instance_OnDuringWaveStart;

        GameStateManager.Instance.OnGameOverStart += Instance_OnGameOverStart;
    }

    private void Instance_OnDuringWaveStart(object sender, int e)
    {
        //e++ because index starts at zero
        waveText.text = "Wave: " + e++.ToString();
    }

    private void Instance_OnGameOverStart(object sender, System.EventArgs e)
    {
        gameOverScreen.SetActive(true);
    }

    private void GameStateManager_OnMoneyChange(object sender, int e)
    {
        moneyText.text = "Money: " + e.ToString();
    }

    private void GameStateManager_OnHealthChange(object sender, int e)
    {
        healthText.text = "Health: " + e.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
