using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ManageRoundTimer : MonoBehaviour
{

    public static ManageRoundTimer Instance { get; private set; }

    [SerializeField] int timeBetweenWaves;
    [SerializeField] TextMeshProUGUI timerText;

    public event EventHandler OnTimerComplete;

    private IEnumerator coroutine;
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Already a ManageTimer in scene destroying: " + gameObject);
            Destroy(this);
            return;
        }

        Instance = this;

    }

    private void Start()
    {
        GameStateManager.Instance.OnBeforeWaveStart += GameStateManager_OnBeforeWaveStart;
        coroutine = Timer();
        StartTimer();
    }

    private void GameStateManager_OnBeforeWaveStart(object sender, EventArgs e)
    {
        StartTimer();
    }



    private void StartTimer()
    {
        coroutine = Timer();
        StartCoroutine(coroutine);
    }



    private IEnumerator Timer() {

        Debug.Log("test");
        timerText.gameObject.SetActive(true);

        int timer = timeBetweenWaves;

        while (timer > 0)
        {
            Debug.Log("teste");
            timerText.text = timer.ToString();
            yield return new WaitForSeconds(1);
            timer--;
        }

        timerText.gameObject.SetActive(false);
        OnTimerComplete?.Invoke(this, EventArgs.Empty);
    
    }

    public void OnDestroy()
    {
        GameStateManager.Instance.OnBeforeWaveStart -= GameStateManager_OnBeforeWaveStart;
    }

}
