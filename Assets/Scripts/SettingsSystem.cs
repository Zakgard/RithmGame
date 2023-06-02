using UnityEngine;
using UnityEngine.UI;

public class SettingsSystem : MonoBehaviour
{
    public static float VolumeLevel;

    [SerializeField] private Slider _volumeSlider;

    private void Start()
    {
        VolumeLevel = .5f;
        _volumeSlider.value = VolumeLevel;
    }

    public void OnValueChange()
    {
        VolumeLevel = _volumeSlider.value;
        MenuAudioSystem.instance.SetVolume(VolumeLevel);
    }
}
