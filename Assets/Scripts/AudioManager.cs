using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    

     private int _index;
    [SerializeField] private bool _isTest;
    [SerializeField] private int _groupsize;
    [SerializeField] private List<AudioClip> _audioClips;
    [SerializeField] private float _delay;

    private float _time;
    private bool _isStarted;
    private AudioSource _musicSource;

 
    private float _currentTime;
    private bool _isPlaying;

    public static bool IsSliderCompleted;

    private void Start()
    {
        _isPlaying = true;
        _currentTime= 0;
    }
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
        _musicSource = GetComponent<AudioSource>();        
    }


    private void Update()
    {
        if(!_musicSource.isPlaying && !SessionManager.IsLost && SessionManager.IsStarted && !_isPlaying)
        {
            SessionManager.IsWon = true;
        }

        if(_currentTime >= _delay && SessionManager.IsSpawn)
        {
            PlayMusic();
            _isPlaying = false;
            SessionManager.IsSpawn = false;           
        }
        else
        {
            _currentTime += Time.deltaTime;
        }
    }

    public void PlayMusic()
    {
        _musicSource.clip = _audioClips[Conductor.LevelIndex];
        _musicSource.Play();
        _isStarted= true;
        AlgorithmSpawner.Instance.SetAmplitudeBouns();
    }

    public void StopMusic()
    {
        _musicSource.Stop();
        _musicSource.clip=null;
        _isStarted= false;
    }   
}
