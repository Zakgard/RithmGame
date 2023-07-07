using UnityEngine;
using System;
using Agava.YandexGames;
using UnityEngine.SceneManagement;

public class YandexAdvertisingService : IAdvertisingService
{
    private readonly ICoroutineRunner _coroutineRunner;
    private readonly IPrefsService _prefsService;

    private bool _previousAudioState;
    private Action _onRewarded;
    private bool _rewarded;

    public event Action AdvertisingStarted;
    public event Action AdvertisingEnded;

    private static bool SdkNotWorking => Application.isEditor || YandexGamesSdk.IsInitialized == false;

    public bool IsInterstitialPurchased
    {
        get => _prefsService.GetInt("IsInterstitialPurchased", 0) == 1;
        set => _prefsService.SetInt("IsInterstitialPurchased", 1);
    }

    public YandexAdvertisingService(ICoroutineRunner coroutineRunner, IPrefsService prefsService)
    {
        _prefsService = prefsService;
        _coroutineRunner = coroutineRunner;
    }

    public void ShowInterstitial(Action action)
    {
        if (SdkNotWorking)
        {
            Debug.Log("Interstitial shown. But we're not in build");
            SceneManager.LoadScene("MenuScene");
            return;
        }
        if (IsInterstitialPurchased)
        {
            Debug.Log("Ad block bought");
            SceneManager.LoadScene("MenuScene");
            return;
        }

        
        InterstitialAd.Show(
            onOpenCallback: OnInterstitialOpen,
            onCloseCallback: _ => OnInterstitialClose(),
            onErrorCallback: _ => OnInterstitialErrorClose(),
            onOfflineCallback: OnInterstitialClose);
    }

    public void ShowRewarded(Action onRewarded)
    {
        if (SdkNotWorking)
        {
            Debug.Log("Rewarded shown");
            onRewarded?.Invoke();
            return;
        }

        _onRewarded = onRewarded;       

        VideoAd.Show(
            onOpenCallback: OnVideoOpen,
            onRewardedCallback: OnRewarded,
            onCloseCallback: OnClosed, 
            onErrorCallback: _ => OnClosed());
    }

    private void OnInterstitialClose()
    {
        Debug.Log("Interstitial shown");
        SceneManager.LoadScene("MenuScene");
        AdvertisingEnded?.Invoke();
    }

    private void OnInterstitialErrorClose()
    {
        SceneManager.LoadScene("MenuScene");
        Debug.Log("Error");
    }

    private void OnClosed()
    {
        if (_rewarded)
        {
            _onRewarded?.Invoke();
            AdManager.Instance.GoRewardGot();
        }
        _rewarded = false;
        AdvertisingEnded?.Invoke();
        AdManager.Instance.GoRewardMissed();
    }

    private void OnRewarded() => _rewarded = true;

    private void OnVideoOpen()
    {  
        AdvertisingStarted?.Invoke();
    }

    private void OnInterstitialOpen()
    {
        Debug.Log("Before invoking");
        AdvertisingStarted?.Invoke();
        Debug.Log("After Invoking");
    }
}
