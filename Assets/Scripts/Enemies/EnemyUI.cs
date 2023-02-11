using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyUI : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI healthText;

    [SerializeField] EnemyUnit unit;
    // Start is called before the first frame update
    void Start()
    {
        if (unit == null)
        {
            unit = gameObject.GetComponentInParent<EnemyUnit>();
        }

        unit.OnHealthChanged += Unit_OnHealthChanged;
    }

    private void Unit_OnHealthChanged(object sender, System.EventArgs e)
    {
        UpdateHealthText();
    }


    void UpdateHealthText()
    {
        healthText.text = unit.GetHealth().ToString();
    }
}
