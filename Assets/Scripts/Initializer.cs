using System.Collections;
using UnityEngine;
using Agava.YandexGames;
using UnityEngine.SceneManagement;

public class Initializer : MonoBehaviour, ICoroutineRunner
{
    public static ICoroutineRunner Instance;

    private void Awake()
    {
        YandexGamesSdk.CallbackLogging = true;
        Instance = this;
    }

    private IEnumerator Start()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        var Service = new PlayerPrefsService(Instance);
        Game.AdvertisingService = new YandexAdvertisingService(Instance, Service);
        Game.PreferencesService = Service;
        Debug.Log("not Authorised");
        SceneManager.LoadScene("MenuScene");
        yield break;
#else
        yield return YandexGamesSdk.Initialize();
        var Service = new YandexPrefsService(Instance);
        yield return new WaitUntil(() => Service.IsDataLoaded);
        Game.AdvertisingService = new YandexAdvertisingService(Instance, Service);
        Game.PreferencesService = Service;
        Debug.Log("Authorised");
        SceneManager.LoadScene("MenuScene");
        yield break;
#endif
    }
}
