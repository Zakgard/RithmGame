using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _goldText;
    [SerializeField] private TMP_Text _crownText;
    [SerializeField] private bool _isCleanup;
    [SerializeField] private List<StarList> _starList;

    private void Start()
    {
        int gold = 0;
        int stars = 0;
        if (_isCleanup)
        {            
            Clear();
        }
            
        try
        {
            gold = PlayerPrefs.GetInt("coins");
            _goldText.text =  $"{gold}";
            for(int i = 0; i < _starList.Count; i++)
            {
                int tempStars = PlayerPrefs.GetInt($"{_starList[i].Level}");
                stars += tempStars;
            }
            _crownText.text = $"{stars}";
        }
        catch
        {

        }
        GetStars();
    }

    private void GetStars()
    {
        for(int i = 0; i < _starList.Count; i++)
        {
            int stars = PlayerPrefs.GetInt($"{_starList[i].Level}");
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
            PlayerPrefs.SetInt($"{_starList[i].Level}", 0);
        }
        PlayerPrefs.SetInt("coins", 0);
        PlayerPrefs.SetInt("stars", 0);
        PlayerPrefs.Save();
    }
}
