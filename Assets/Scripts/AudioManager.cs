using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;  

    private int _index;
    [SerializeField] private bool _isTest;
    [SerializeField] private int _groupsize;
    [SerializeField] private List<AudioClip> _audioClips;

    [SerializeField] private int index;
    [SerializeField] private List<float> _timeOffsets;
    [SerializeField] private float _testDelay;

    private AudioSource _musicSource;
    private float _currentTime;
    private bool _isPlaying;
    private float _delay;
    public static bool IsSliderCompleted;
    public static bool IsPlaying;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        _index = PlayerPrefs.GetInt("MusicIndex");
        _index = Mathf.RoundToInt(_index / 3.0f); 

        _musicSource = GetComponent<AudioSource>();        
    }

    private void Start()
    {
        Debug.Log(_index);
        if (_isTest)
        {
            _index = index;
            _delay = _testDelay;
        }
        else
        {
            _delay = _timeOffsets[index];
        }
        
        _isPlaying = true;
        _currentTime = 0;
    }

    private void Update()
    {
        if(!_musicSource.isPlaying && !SessionManager.IsLost && SessionManager.IsStarted && !_isPlaying)
        {
            SessionManager.IsWon = true;
        }

        if(SessionManager.IsSpawn &&_currentTime >= _delay)
        {
            PlayMusic();
            Debug.Log(_index);
            _isPlaying = false;
            SessionManager.IsSpawn = false;           
        }
        else if(SessionManager.IsSpawn)
        {
            _currentTime += Time.deltaTime;
        }
    }

    public void PlayMusic()
    {
        _musicSource.clip = _audioClips[_index];
        _musicSource.Play();
        IsPlaying = true;
    }

    public void StopMusic()
    {
        _musicSource.Stop();
        _musicSource.clip=null;
    }   
}
