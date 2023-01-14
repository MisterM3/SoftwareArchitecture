using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ManageUI : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] TextMeshProUGUI moneyText;
    // Start is called before the first frame update
    void Start()
    {
        GameStateManager.Instance.OnHealthChange += GameStateManager_OnHealthChange;
        GameStateManager.Instance.OnMoneyChange += GameStateManager_OnMoneyChange;
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
