using UnityEngine;
using UnityEngine.UI;

public class TrackPlayer : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _slider.maxValue = _audioSource.clip.length;
    }

    private void Update()
    {
        if(AudioManager.IsPlaying)
           _slider.value += Time.deltaTime;
    }
}
