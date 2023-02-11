using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Turret))]
public class UpgradePaths : MonoBehaviour
{

    [SerializeField] int upgradeIndexFirst = 0;
    [SerializeField] List<UpgradeStrategySO> FirstUpgradeStrategy;


    [SerializeField] int upgradeIndexSecond = 0;
    [SerializeField] List<UpgradeStrategySO> SecondUpgradeStrategy;

    private IUpgradable[] upgradables;


    // Start is called before the first frame update
    void Start()
    {
        upgradables = gameObject.GetComponents<IUpgradable>();
    }


    public void UpgradeFirst()
    {
        if (upgradeIndexFirst >= FirstUpgradeStrategy.Count) return;
        
        if (!MoneyManager.Instance.TrySpendMoney(FirstUpgradeStrategy[upgradeIndexFirst + 1].costUpgrade)) return;

        upgradeIndexFirst++;
        UpgradeComponenent(FirstUpgradeStrategy[upgradeIndexFirst]);
         
    }

    public void UpgradeSecond()
    {
        if (upgradeIndexSecond >= SecondUpgradeStrategy.Count) return;

        if (!MoneyManager.Instance.TrySpendMoney(SecondUpgradeStrategy[upgradeIndexSecond + 1].costUpgrade)) return;

        upgradeIndexSecond++;
        UpgradeComponenent(SecondUpgradeStrategy[upgradeIndexSecond]);
    }



    //The components themself check if they can be upgraded by the upgrade
    public void UpgradeComponenent(UpgradeStrategySO upgrade)
    {
        foreach(IUpgradable upgradable in upgradables)
        {
            upgradable.Upgrade(upgrade);
        }

    }


    //Returns name + cost
    public string GetFirstUpgradeNameAndCost()
    {
        if ((upgradeIndexFirst + 1) >= FirstUpgradeStrategy.Count) return "No Upgrades";


            string cost = FirstUpgradeStrategy[upgradeIndexFirst + 1].costUpgrade.ToString();
        string name = FirstUpgradeStrategy[upgradeIndexFirst + 1].nameUpgrade;

        return $"{name} \n ({cost})";
    }



    public int GetFirstUpgradeCost()
    {
        if ((upgradeIndexFirst + 1) >= FirstUpgradeStrategy.Count) return int.MaxValue;

        return FirstUpgradeStrategy[upgradeIndexFirst + 1].costUpgrade;
    }

    //Returns name + cost
    public string GetSecondUpgradeNameAndCost()
    {
        if ((upgradeIndexSecond + 1) >= SecondUpgradeStrategy.Count) return "No Upgrades";


        string cost = SecondUpgradeStrategy[upgradeIndexSecond + 1].costUpgrade.ToString();
        string name = SecondUpgradeStrategy[upgradeIndexSecond + 1].nameUpgrade;

        return $"{name} \n ({cost})";
    }



    public int GetSecondUpgradeCost()
    {
        if ((upgradeIndexSecond + 1) >= SecondUpgradeStrategy.Count) return int.MaxValue;

        return SecondUpgradeStrategy[upgradeIndexSecond + 1].costUpgrade;
    }
}
