using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableUI : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        GameStateManager.Instance.OnGameOverStart += Instance_OnGameOverStart;
        GameStateManager.Instance.OnWonStart += Instance_OnWonStart;
    }

    private void Instance_OnWonStart(object sender, System.EventArgs e)
    {
        this.gameObject.SetActive(false);
    }

    private void Instance_OnGameOverStart(object sender, System.EventArgs e)
    {
        this.gameObject.SetActive(false);
    }

}
