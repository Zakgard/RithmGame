using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _goldText;
    [SerializeField] private TMP_Text _crownText;
    [SerializeField] private bool _isCleanup;
    [SerializeField] private List<StarList> _starList;

    public static CurrencyManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        int gold = 0;
        int stars = 0;

        if (_isCleanup)
        {            
            Clear();
        }

        gold = Game.PreferencesService.GetInt("coins");
        _goldText.text = $"{gold}";
        for (int i = 0; i < _starList.Count; i++)
        {
            int tempStars = Game.PreferencesService.GetInt($"{_starList[i].Level}");
            stars += tempStars;
        }
        _crownText.text = $"{stars}";

        GetStars();
    }

    private void GetStars()
    {
        for (int i = 0; i < _starList.Count; i++)
        {
            int stars = Game.PreferencesService.GetInt($"{_starList[i].Level}");
            if (stars > 3)
                stars = 3;

            for (int j = 0; j < stars; j++)
            {
                _starList[i].StarsImage[j].SetActive(true);
            }
        }
    }

    private void Clear()
    {
        for(int i = 0; i < _starList.Count; i++)
        {
            Game.PreferencesService.SetInt($"{_starList[i].Level}", 0);
        }
        Game.PreferencesService.SetInt("coins", 0);
        Game.PreferencesService.SetInt("stars", 0);
    }

    public bool IsCleanup()
    {
        return _isCleanup;
    }

    public bool IsEnoughCurrencyToBuyLevel(int levelPrice)
    {
        return levelPrice <= Game.PreferencesService.GetInt("coins");
    }

    public void ChangeCoinsAmount(int amount)
    {
        var balance = Game.PreferencesService.GetInt("coins");
        Game.PreferencesService.SetInt("coins", balance + amount);
    }

    public int GetCoinsLack(int price)
    {
        return price - Game.PreferencesService.GetInt("coins");
    }
}
