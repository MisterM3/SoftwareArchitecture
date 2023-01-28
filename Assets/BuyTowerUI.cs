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

    private int cost;

    // Start is called before the first frame update
    void Start()
    {

        GameStateManager.Instance.OnMoneyChange += Instance_OnMoneyChange;

        cost = GetComponent<BuyTower>().getCost();

        towerText.text = "Turret \n (" + cost.ToString() + ")";
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
