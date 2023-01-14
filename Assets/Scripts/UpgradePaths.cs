using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Turret))]
public class UpgradePaths : MonoBehaviour
{

    [SerializeField] int upgradeIndex = 0;
    [SerializeField] List<TurretShootSpeedStrategySO> ShootingStragety;

    private Turret turret;

    // Start is called before the first frame update
    void Start()
    {
        turret = gameObject.GetComponent<Turret>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Upgrade()
    {
        if (upgradeIndex >= ShootingStragety.Count) return;
        
        if (!GameStateManager.Instance.TrySpendMoney(ShootingStragety[upgradeIndex + 1].costUpgrade)) return;
        
        upgradeIndex++;
        turret.ChangeShootSpeedStragety(ShootingStragety[upgradeIndex]);
        
        
    }
}
