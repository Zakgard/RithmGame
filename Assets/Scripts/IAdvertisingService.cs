using System;

public interface IAdvertisingService
{
    public void ShowInterstitial();
    public void ShowRewarded(Action action);
}