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
    private float _trackTime;
    private float _currentTime;
    private bool _isPlaying;
    private float _delay;
    private bool _isPaused;
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
        _isPaused = false;
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
        AudioListener.pause = false;
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
        if (_musicSource != null && !_isPaused)
        {
            _musicSource.Stop();
            _musicSource.clip = null;
            _isPaused = true;
        }
    }   

    public void OnPauseMusic()
    {
        if(_musicSource != null)
        {
            _musicSource.Pause();
            Debug.Log("paused");
        }      
    }

    public void OnContinuePlayMusic()
    {
        if (_musicSource != null && _isPaused)
        {
            _musicSource.UnPause();
            Debug.Log("continue");
            _isPaused = false;
        }
    }

    public float GetTrackLength()
    {
        return _musicSource.clip.length;
    }
}
