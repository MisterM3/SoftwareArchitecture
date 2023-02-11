using System;
using UnityEngine;

public class DisableUI : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        GameStateManager.Instance.OnGameOverStart += GameStateManager_OnGameOverStart;
        GameStateManager.Instance.OnWonStart += GameStateManager_OnWonStart;
    }

    private void GameStateManager_OnWonStart(object sender, EventArgs e)
    {
        DisableButtonUI();
    }

    private void GameStateManager_OnGameOverStart(object sender, EventArgs e)
    {
        DisableButtonUI();
    }

    private void DisableButtonUI()
    {
        this.gameObject.SetActive(false);
    }

}
