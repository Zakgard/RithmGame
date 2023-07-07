using UnityEngine;
using UnityEngine.UI;

public class SettingsSystem : MonoBehaviour
{
    public static float VolumeLevel;

    [SerializeField] private Toggle _musicToggle;

    private void Start()
    {
        VolumeLevel = .5f;
        MenuAudioSystem.instance.SetVolume(VolumeLevel);
    }

    public void OnValueChange()
    {
        if(_musicToggle.isOn)
        {
            MenuAudioSystem.instance.SetVolume(VolumeLevel);
            Debug.Log("On");
        }
        else
        {
            MenuAudioSystem.instance.SetVolume(0.0f);
            Debug.Log("Off");
        }
        
    }
}
