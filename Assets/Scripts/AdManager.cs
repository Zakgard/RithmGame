using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class AdManager : MonoBehaviour
{
    public static AdManager Instance;

    [SerializeField] private TMP_Text _timerTMP;

    private float _timer;
    private bool _isTimerRunning;
    private event Action _OnRewarded;
    private event Action _OnInterstitialShawn;

    private void Start()
    {
        _OnRewarded += GoRewardGot;
        _OnInterstitialShawn += SessionManager.Instance.GoToTheMenu;
    }

    private void Awake()
    {
        Instance = this;
    }

    public void RunInterstitial()
    {
        Game.AdvertisingService.ShowInterstitial(_OnInterstitialShawn);
    }

    public void OnRewardedlAdButtonClick()
    {
        Game.AdvertisingService.ShowRewarded(_OnRewarded);
    }

    private void Update()
    {
        if(_isTimerRunning && _timer < 3.0f)
        {
            _timer += Time.deltaTime;
        }

        if(_timer >= 3.0f)
        {
            _isTimerRunning = false;         
        }
    }

    public void GoRewardGot()
    {      
        SessionManager.Instance.CloseLoseTab();
        StartCoroutine(ContinueGame()); 
        _isTimerRunning = true;
    }

    public void GoRewardMissed()
    {
        Debug.Log("Ad Error, reward not got!");
    }

    private IEnumerator ContinueGame()
    {
        _timerTMP.gameObject.SetActive(true);
        _timerTMP.text = "3";
        yield return new WaitForSecondsRealtime(1);
        _timerTMP.text = "2";
        yield return new WaitForSecondsRealtime(1);
        _timerTMP.text = "1";
        yield return new WaitForSecondsRealtime(1);
        _timerTMP.gameObject.SetActive(false);
        AudioManager.Instance.OnContinuePlayMusic();
        Time.timeScale = 1.0f;
    }
}
