using UnityEngine;
using UnityEngine.UI;

public class SettingsSystem : MonoBehaviour
{
    public static float VolumeLevel;

    [SerializeField] private Toggle _musicToggle;

    private void Start()
    {
        VolumeLevel = .5f;
    }

    public void OnValueChange()
    {
        if(_musicToggle.isOn)
        {
            MenuAudioSystem.instance.SetVolume(VolumeLevel);
        }
        else
        {
            MenuAudioSystem.instance.SetVolume(0.0f);
        }
        
    }
}
