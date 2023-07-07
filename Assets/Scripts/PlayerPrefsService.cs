using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerPrefsService : IPrefsService
{
    private readonly ICoroutineRunner _coroutineRunner;
    public PlayerPrefsService(ICoroutineRunner coroutineRunner)
    {
        _coroutineRunner = coroutineRunner;
        _coroutineRunner.StartCoroutine(SaveOverTime());
    }

    private static IEnumerator SaveOverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(.5f);
            PlayerPrefs.Save();
        }
    }

    public bool IsDataLoaded => true;

    public event Action DataJustLoaded;
    public string GetString(string key) => PlayerPrefs.GetString(key);

    public void SetString(string key, string value) => PlayerPrefs.SetString(key, value);

    public void SetInt(string key, int value) => PlayerPrefs.SetInt(key, value);

    public int GetInt(string key, int defaultValue = 0) => PlayerPrefs.GetInt(key, defaultValue);

    public bool HasKey(string key) => PlayerPrefs.HasKey(key);
}
