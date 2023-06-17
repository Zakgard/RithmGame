using UnityEngine;
using System.Collections.Generic;

public class MenuAudioSystem : MonoBehaviour
{
    public static MenuAudioSystem instance;

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioSource _bubleSource;
    [SerializeField] private List<AudioClip> _clips;
    [SerializeField] private GameObject _effect;

    private Transform _buttonTransform;
    private GameObject _lastBUtton;
    private GameObject _currentButton;
    private GameObject _effectHandler;

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
        if(_lastBUtton != null)
        {
            _lastBUtton.SetActive(true);
        }
        _effect.SetActive(false);
        _audioSource.Stop();
        _audioSource.PlayOneShot(_clips[index]);
        SetButtonTransform();
        SetEffectPosition();
    }

    public void SetVolume(float volume)
    {
        _audioSource.volume = volume;
    }

    private void PlayEffectSound()
    {
        _bubleSource.Play();
    }

    private void SetEffectPosition()
    {
        _effect.transform.position = _buttonTransform.position;
        _effect.transform.SetParent(_effectHandler.transform);
        _effect.SetActive(true);
        _currentButton.SetActive(false);
    }

    private void SetButtonTransform()
    {       
        _buttonTransform = _currentButton.GetComponent<Transform>();
    }

    public void SetLatButtonGO(GameObject gameObject)
    {
        _lastBUtton = _currentButton;
        _currentButton = gameObject;
    }

    public void SetEffectHandler(GameObject effector)
    {
        _effectHandler = effector;
    }
   
}
