using System;
using UnityEngine;

public class FakeAdService : IAdvertisingService
{
    private readonly ICoroutineRunner _coroutineRunner;
    private readonly IPrefsService _prefsService;

    public FakeAdService(ICoroutineRunner coroutineRunner, IPrefsService prefsService)
    {
        _coroutineRunner= coroutineRunner;
        _prefsService= prefsService;
    }

    public void ShowInterstitial(Action action = null)
    {
        Debug.Log("Ad shown!");      
    }

    public void ShowRewarded(Action action)
    {
        Debug.Log("Reward given!");
        AdManager.Instance.GoRewardGot();
    }
}
