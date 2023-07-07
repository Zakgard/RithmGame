using System;

public interface IAdvertisingService
{
    public void ShowInterstitial(Action action);
    public void ShowRewarded(Action action);
}