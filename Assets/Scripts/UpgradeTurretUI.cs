using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(UpgradePaths))]
public class UpgradeTurretUI : MonoBehaviour
{


    [SerializeField] GameObject UpgradableUI;

    private UpgradePaths _paths;
    // Start is called before the first frame update
    void Start()
    {
        MoneyManager.Instance.OnMoneyChange += MoneyManager_OnMoneyChange;
        _paths = GetComponent<UpgradePaths>();
        CheckTurretUpgradable(MoneyManager.Instance.GetMoney());
    }

    private void MoneyManager_OnMoneyChange(object sender, int money)
    {
        CheckTurretUpgradable(money);
    }


    void CheckTurretUpgradable(int money)
    {
        if (_paths == null)
        {
            _paths = GetComponent<UpgradePaths>();
            return;
        }
        if (_paths.GetFirstUpgradeCost() <= money || _paths.GetSecondUpgradeCost() <= money) UpgradableUI.SetActive(true);
        else UpgradableUI.SetActive(false);
    }

    //DO THIS WITH EVERY EVENTS
    void OnDestroy()
    {
        MoneyManager.Instance.OnMoneyChange -= MoneyManager_OnMoneyChange;
    }


}
