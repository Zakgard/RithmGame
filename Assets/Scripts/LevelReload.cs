using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelReload : MonoBehaviour
{
    public void OnRestartButtonClick()
    {
        int currentLEvelIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentLEvelIndex);
    }
}
