using UnityEngine;
using System.Collections.Generic;

public class MenuAudioSystem : MonoBehaviour
{
    public static MenuAudioSystem instance;

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioSource _bubleSource;
    [SerializeField] private List<AudioClip> _clips;

    public void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        _audioSource.volume = .5f;
    }

    private void Update()
    {
        if(Input.GetMouseButton(0))
        {
             PlayEffectSound();
        }

        if (Input.touchCount > 0)
        {
            PlayEffectSound();
        }
    }

    public void OnPlayMusicButtonClick(int index)
    {
        _audioSource.Stop();
        _audioSource.PlayOneShot(_clips[index]);
    }

    public void SetVolume(float volume)
    {
        _audioSource.volume = volume;
    }

    private void PlayEffectSound()
    {
        _bubleSource.Play();
    }
}
