using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrackData : MonoBehaviour
{  
    public void SetTrackData(int index)
    {
        PlayerPrefs.SetInt("MusicIndex", index);
        PlayerPrefs.Save();
        SceneManager.LoadScene("GameLevel");
    }

    public void SetAuthor(TMP_Text text)
    {
        PlayerPrefs.SetString("Author", text.text);
        Debug.Log(text.text);
        PlayerPrefs.Save();
    }

    public void SetTitle(TMP_Text title)
    { 
        PlayerPrefs.SetString("Title", title.text);
        Debug.Log(title.text);
        PlayerPrefs.Save();
    }
}
