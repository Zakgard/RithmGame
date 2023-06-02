using System.Collections.Generic;
using UnityEngine;

public class RewardSystem : MonoBehaviour
{
    [SerializeField] private List<string> _levels;

    private void Start()
    {
        SessionManager.Instance.OnGameWon += GetGoldCoins;
        SessionManager.Instance.OnGameWon += GetStars;
    }
    private void GetGoldCoins()
    {
        try
        {
            int coins = PlayerPrefs.GetInt("coins");
            coins += 150;
            PlayerPrefs.SetInt("coins", coins);
            PlayerPrefs.Save();
        }
        catch
        {

        }
        SessionManager.Instance.OnGameWon -= GetGoldCoins;
        
        
    }

    private void GetStars()
    {
        try
        {
            int stars = PlayerPrefs.GetInt($"{_levels[Conductor.LevelIndex]}");
            if(stars <= 3)
            {
                stars = SessionManager.StarsGot;
            }
            int currentStars = PlayerPrefs.GetInt("stars");
            int starss = SessionManager.StarsGot;

            if (stars + SessionManager.StarsGot > 3)
            {
                stars = 3;
                SessionManager.StarsGot = 0;
            }

            

            PlayerPrefs.SetInt($"{_levels[Conductor.LevelIndex]}", stars + SessionManager.StarsGot);
            PlayerPrefs.SetInt("stars", stars + currentStars);
            PlayerPrefs.Save();
        }
        catch
        {

        }
        SessionManager.Instance.OnGameWon -= GetStars;
    }
}
