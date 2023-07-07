using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agava.YandexGames;
using Newtonsoft.Json;

public class YandexPrefsService : IPrefsService
{
    private readonly ICoroutineRunner _coroutineRunner;

    private Dictionary<string, string> _data = new();
    private int _version;

    public bool IsDataLoaded { get; private set; }

    public event Action DataJustLoaded;

    private void Initialize () => PlayerAccount.GetCloudSaveData(InitilizeService);

    private void InitilizeService(string json)
    {
        if (!string.IsNullOrEmpty(json))
        {
            _data = JsonConvert.DeserializeObject<Dictionary<string, string>>(json, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Include,
            });
        }
        IsDataLoaded= true;
        _coroutineRunner.StartCoroutine(Worker());
        DataJustLoaded?.Invoke();
    }

    private IEnumerator Worker()
    {
        int version = _version;

        while (true)
        {
            if(_version == version)
            {
                yield return null;
                continue;
            }

            version = _version;
            Debug.Log($"Saving {_version} into yandex cloud");
            string serialized = JsonConvert.SerializeObject(_data);
            PlayerAccount.GetCloudSaveData(InitilizeService);
            yield return null;
        }
    }

    public YandexPrefsService(ICoroutineRunner coroutineRunner)
    {
        _coroutineRunner = coroutineRunner;        
        Initialize();
    }

    private void ThrowIfDataNotLoaded()
    {
        if (!IsDataLoaded)
        {
            throw new InvalidOperationException();
        }
    }

    public string GetString(string key)
    {
        ThrowIfDataNotLoaded();
        if (HasKey(key) == false)
            return String.Empty;
        return _data[key];
    }

    public void SetString(string key, string value)
    {
        ThrowIfDataNotLoaded();
        if (HasKey(key) == false)
        {
            _data.Add(key, value);
            return;
        }
        if (_data[key] == value)
            return;
        _data[key] = value;
        _version++;
    }

    public void SetInt(string key, int value)
    {
        ThrowIfDataNotLoaded();
        if (HasKey(key) == false)
        {
            _data.Add(key, value.ToString());
            return;
        }
        if (_data[key] == value.ToString())
            return;
        _data[key] = value.ToString();
        _version++;
    }

    public int GetInt(string key, int defaultValue = 0)
    {
        ThrowIfDataNotLoaded();
        if (_data.ContainsKey(key) == false)
            return defaultValue;
        return int.Parse(_data[key]);
    }

    public bool HasKey(string key)
    {
        ThrowIfDataNotLoaded();
        return _data.ContainsKey(key);
    }
}
