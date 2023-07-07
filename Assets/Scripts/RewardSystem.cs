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
            int coins = Game.PreferencesService.GetInt("coins");
            coins += 150;
            Game.PreferencesService.SetInt("coins", coins);
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
            int stars = Game.PreferencesService.GetInt($"{_levels[Conductor.LevelIndex]}");
            if(stars <= 3)
            {
                stars = SessionManager.StarsGot;
            }
            int currentStars = Game.PreferencesService.GetInt("stars");
            int starss = SessionManager.StarsGot;

            if (stars + SessionManager.StarsGot > 3)
            {
                stars = 3;
                SessionManager.StarsGot = 0;
            }
            Game.PreferencesService.SetInt($"{_levels[Conductor.LevelIndex]}", stars + SessionManager.StarsGot);
            Game.PreferencesService.SetInt("stars", stars + currentStars);
        }
        catch
        {

        }
        SessionManager.Instance.OnGameWon -= GetStars;
    }
}
