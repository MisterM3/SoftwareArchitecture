using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{

    [SerializeField] Turret turret;

    [SerializeField] Button buttonFirstUpgrade;

    [SerializeField] Button buttonSecondUpgrade;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetButtons(Turret pTurret)
    {
        turret = pTurret;
        buttonFirstUpgrade.onClick.RemoveAllListeners();
        buttonFirstUpgrade.onClick.AddListener(turret.Upgrade);
        
    }
}
