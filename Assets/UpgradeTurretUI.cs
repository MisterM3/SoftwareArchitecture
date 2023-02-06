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
        GameStateManager.Instance.OnMoneyChange += GameStateManager_OnMoneyChange;
        _paths = GetComponent<UpgradePaths>();
        CheckTurretUpgradable(GameStateManager.Instance.GetMoney());
    }

    private void GameStateManager_OnMoneyChange(object sender, int money)
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
        GameStateManager.Instance.OnMoneyChange -= GameStateManager_OnMoneyChange;
    }


}
