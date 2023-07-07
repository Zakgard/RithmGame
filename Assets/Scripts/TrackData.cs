using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrackData : MonoBehaviour
{  
    public void SetTrackData(int index)
    {
        PlayerPrefs.SetInt("MusicIndex", index);
        SceneManager.LoadSceneAsync("GameLevel");
    }

    public void SetAuthor(TMP_Text text)
    {
       // PlayerPrefs.SetString("Author", text.text);
    }

    public void SetTitle(TMP_Text title)
    { 
       // PlayerPrefs.SetString("Title", title.text);
    }
}
