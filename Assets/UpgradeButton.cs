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

    [SerializeField] Button destroyButton;

    // Start is called before the first frame update
    void Start()
    {
        textButtonOne = buttonFirstUpgrade.GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetButtons(Turret pTurret)
    {
        turret = pTurret;
      //  textButtonOne.text = "Turret ShotSpeed" + turret
        buttonFirstUpgrade.onClick.RemoveAllListeners();
        buttonFirstUpgrade.onClick.AddListener(turret.Upgrade);

        destroyButton.onClick.RemoveAllListeners();
        destroyButton.onClick.AddListener(turret.DestroyTurret);

    }
}
