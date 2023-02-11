using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(BuyTower))]
public class BuyTowerUI : MonoBehaviour
{

    [SerializeField] Button button;
    [SerializeField] TextMeshProUGUI towerText;

    //CHANGE THIS TO HAVE INFO ON SO OR TURRET ITSELF
    [SerializeField] string nameTurret;

    private int cost;

    // Start is called before the first frame update
    void Start()
    {

        MoneyManager.Instance.OnMoneyChange += Instance_OnMoneyChange;

        cost = GetComponent<BuyTower>().GetCost();

        towerText.text = nameTurret + " \n (" + cost.ToString() + ")";
    }

    private void Instance_OnMoneyChange(object sender, int e)
    {
        CheckButton(e);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CheckButton(int money)
    {
        if (money >= cost) button.interactable= true;
        else button.interactable= false;
    }
}
