using System.Collections;
using UnityEngine;
using Agava.YandexGames;
using UnityEngine.SceneManagement;
using System;

public class Initializer : MonoBehaviour
{
    public static ICoroutineRunner coroutineRunner;
    private void Awake()
    {
        YandexGamesSdk.CallbackLogging = true;
    }

    private IEnumerator Start()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        var prefsP = new PlayerPrefsService(coroutineRunner);
        //  Game.AdvertisingService = new FakeService(coroutineRunner, prefsP);
        Game.PreferencesService = prefsP;
        SceneManager.LoadScene("MenuScene");
        yield break;
#endif       
        yield return YandexGamesSdk.Initialize();
        var prefsY = new YandexPrefsService(coroutineRunner);
        yield return new WaitUntil(() => prefsY.IsDataLoaded);
        Game.AdvertisingService = new YandexAdvertisingService(coroutineRunner, prefsY);
        Game.PreferencesService = prefsY;
        SceneManager.LoadScene("MenuScene");
    }
}
