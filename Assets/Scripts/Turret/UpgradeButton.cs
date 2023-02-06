using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{

    [SerializeField] Turret turret;

    [SerializeField] Button buttonFirstUpgrade;
    private TextMeshProUGUI textButtonOne;

    [SerializeField] Button buttonSecondUpgrade;
    private TextMeshProUGUI textButtonTwo;

    [SerializeField] Button destroyButton;

    

    // Start is called before the first frame update
    void Start()
    {
        textButtonOne = buttonFirstUpgrade.GetComponentInChildren<TextMeshProUGUI>();

        textButtonTwo = buttonSecondUpgrade.GetComponentInChildren<TextMeshProUGUI>();

        GameStateManager.Instance.OnMoneyChange += GameStateManager_OnMoneyChange;
    }

    private void GameStateManager_OnMoneyChange(object sender, int money)
    {
        CheckMoneyUpgrades(money);
    }


    public void ResetButtons(Turret pTurret)
    {
        turret = pTurret;

        Debug.LogWarning(turret);
        buttonFirstUpgrade.onClick.RemoveAllListeners();
        buttonFirstUpgrade.onClick.AddListener(turret.UpgradeFirst);

        buttonSecondUpgrade.onClick.RemoveAllListeners();
        buttonSecondUpgrade.onClick.AddListener(turret.UpgradeSecond);

        destroyButton.onClick.RemoveAllListeners();
        destroyButton.onClick.AddListener(turret.DestroyTurret);
        destroyButton.onClick.AddListener(DestroyButton);

        buttonFirstUpgrade.onClick.AddListener(ClickedButton);

        buttonSecondUpgrade.onClick.AddListener(ClickedButton);

        textButtonOne.text = turret.GetFirstUpgradeNameAndCost();

        textButtonTwo.text = turret.GetSecondUpgradeNameAndCost();

        CheckMoneyUpgrades(GameStateManager.Instance.GetMoney());
    }

    void CheckMoneyUpgrades(int money)
    {
        if (turret == null) return;

        if (turret.GetFirstCost() <= money) buttonFirstUpgrade.interactable = true;
        else buttonFirstUpgrade.interactable = false;

        if (turret.GetSecondCost() <= money) buttonSecondUpgrade.interactable = true;
        else buttonSecondUpgrade.interactable = false;

    }


    void ClickedButton()
    {
        if (turret == null)
        {
            gameObject.SetActive(false);
            return;
        }
        ResetButtons(turret);
        
    }

    void DestroyButton()
    {
        turret = null;
        gameObject.SetActive(false);

        buttonFirstUpgrade.onClick.RemoveAllListeners();
        buttonSecondUpgrade.onClick.RemoveAllListeners();
        destroyButton.onClick.RemoveAllListeners();
    }




}
