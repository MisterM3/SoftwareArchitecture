using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GridObjectVisual : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gridPositionText;

    public void SetText(string text)
    {
        gridPositionText.text = text;
    }


}
