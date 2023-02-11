using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButtons : MonoBehaviour
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

        MoneyManager.Instance.OnMoneyChange += GameStateManager_OnMoneyChange;
    }

    private void GameStateManager_OnMoneyChange(object sender, int money)
    {
        CheckMoneyUpgrades(money);
    }


    public void ResetButtons(Turret pTurret)
    {
        turret = pTurret;

        UpgradePaths turretUpgrade = turret.GetTurretUpgrade();

        ResetFirstUpgradeButton(turretUpgrade);
        ResetSecondUpgradeButton(turretUpgrade);
        ResetDestroyButton(turret);

        CheckMoneyUpgrades(MoneyManager.Instance.GetMoney());
    }


    private void ResetFirstUpgradeButton(UpgradePaths turretUpgrade)
    {
        buttonFirstUpgrade.onClick.RemoveAllListeners();
        buttonFirstUpgrade.onClick.AddListener(turretUpgrade.UpgradeFirst);

        buttonFirstUpgrade.onClick.AddListener(ClickedButton);

        textButtonOne.text = turretUpgrade.GetFirstUpgradeNameAndCost();
    }

    private void ResetSecondUpgradeButton(UpgradePaths turretUpgrade)
    {
        buttonSecondUpgrade.onClick.RemoveAllListeners();
        buttonSecondUpgrade.onClick.AddListener(turretUpgrade.UpgradeSecond);

        buttonSecondUpgrade.onClick.AddListener(ClickedButton);

        textButtonTwo.text = turretUpgrade.GetSecondUpgradeNameAndCost();
    }

    private void ResetDestroyButton(Turret turret)
    {
        destroyButton.onClick.RemoveAllListeners();
        destroyButton.onClick.AddListener(turret.DestroyTurret);
        destroyButton.onClick.AddListener(DestroyButton);
    }

    private void CheckMoneyUpgrades(int money)
    {
        if (turret == null) return;

        UpgradePaths turretUpgrade = turret.GetTurretUpgrade();

        if (turretUpgrade.GetFirstUpgradeCost() <= money) buttonFirstUpgrade.interactable = true;
        else buttonFirstUpgrade.interactable = false;

        if (turretUpgrade.GetSecondUpgradeCost() <= money) buttonSecondUpgrade.interactable = true;
        else buttonSecondUpgrade.interactable = false;

    }


    private void ClickedButton()
    {
        if (turret == null)
        {
            gameObject.SetActive(false);
            return;
        }
        ResetButtons(turret);
        
    }

    private void DestroyButton()
    {
        turret = null;
        gameObject.SetActive(false);

        buttonFirstUpgrade.onClick.RemoveAllListeners();
        buttonSecondUpgrade.onClick.RemoveAllListeners();
        destroyButton.onClick.RemoveAllListeners();
    }




}
