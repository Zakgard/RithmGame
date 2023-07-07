using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class SessionManager : MonoBehaviour
{
    [SerializeField] private Slider _pointsSlider;
    [SerializeField] private TMP_Text _finalScoreText;
    [SerializeField] private List<GameObject> _stars;
    [SerializeField] private GameObject _gameOverUI;
    [SerializeField] private GameObject _gameWinUI;
    [SerializeField] private GameObject _progressionBar;
    [SerializeField] private List<GameObject> _starsGO;
    [SerializeField] private GameObject _startGO;
    [SerializeField] private GameObject _congratsText;
    [SerializeField] private float _sliderMultiplier;
    [SerializeField] private TMP_Text _streakText;
    [SerializeField] private Slider _slider;
    [SerializeField] private float _endDelay;

    private int _maxPoints;
    private int _currentStar;
    private float _currentScore;
    private int _index;
    private float[]_pointsArray;

    public Action OnGameLost;
    public Action OnGameWon;
    public Action OnGetPoints;
    public Action OnGameStart;

    public static bool IsStarted;
    private int _streak;
    public static bool IsLost;
    public static bool IsWon;
    public static int StarsGot;
    public static bool IsClicked;
    public static bool IsSpawn;
    public static float Points;
    public static SessionManager Instance;
    public static List<GameObject> ObjectsOnscene;

    public void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        Points= 0;
        _streak = 0;
        _currentScore = 0;
        _currentStar = 0;
        _pointsSlider.value = 0;
        IsWon = false;
        IsStarted = false;
        ObjectsOnscene = new List<GameObject>();
        _pointsArray = new float[3];

        OnGetPoints += SetPointsValue;
        OnGameLost += ShowGameOver;
        OnGameLost += ShowGameOverPanel;
        OnGameLost += RemoveObjectsFromScene;
        OnGameLost += DisableStarsProgress;
        OnGameWon += RemoveObjectsFromScene;
       // OnGameWon += DisableStarsProgress;
        OnGameWon += ShowGameWon;

        if(Conductor.Instance != null)
        {
            _index = Conductor.LevelIndex;
            Conductor.Instance.SpawnLevel();
            _maxPoints = Conductor._shorts.Count + Conductor._longs.Count * 3;
            _pointsSlider.maxValue = _maxPoints *.995f;

            for (int i = 0; i < 3; i++)
            {
                _pointsArray[i] = _pointsSlider.maxValue * ((i + 1.0f) / 3.0f);
            }
        }      
    }

    private void OnDestroy()
    {
        OnGetPoints -= SetPointsValue;
        OnGameLost -= ShowGameOver;
        OnGameLost -= ShowGameOverPanel;
        OnGameLost -= RemoveObjectsFromScene;
        OnGameLost -= DisableStarsProgress;
        OnGameWon -= ShowGameWon;
        OnGameWon -= RemoveObjectsFromScene;
       // OnGameWon -= DisableStarsProgress;
    }

    private void Update()
    {
        if(Points >= _maxPoints)
            Points = _maxPoints;

        if (_currentStar < _stars.Count && Points >= _pointsArray[_currentStar] && _slider.value >= _pointsArray[_currentStar])
        {           
            SpawnStar();
            _currentStar++;
            StarsGot++;            
        }

        if(_pointsSlider.value < _currentScore)
        {
            _pointsSlider.value += _sliderMultiplier * Time.deltaTime * (1+_pointsSlider.value/_pointsSlider.maxValue);
        }
        
        if(IsWon && _pointsSlider.value <= _currentScore && !AudioManager.IsSliderCompleted)
        {
            _pointsSlider.value += _sliderMultiplier * Time.deltaTime;
        }

        if (_pointsSlider.value >= _currentScore - .5f)
            AudioManager.IsSliderCompleted = true;
        
        if(IsWon && AudioManager.IsSliderCompleted)
        {
            
        }
    }

    public void DestroyPianoKey(bool isCliked, GameObject gameObject)
    {      
        IsClicked = isCliked;
        Destroy(gameObject);
    }

    public IEnumerator GameWon()
    {
       // DisableStarsProgress();
        RemoveObjectsFromScene();
        yield return new WaitForSeconds(_endDelay + 1.0f);
        AudioManager.Instance.StopMusic();
        IsWon = true;        
        OnGameWon?.Invoke();
    }

    private void GoToPause()
    {
        AudioManager.Instance.OnPauseMusic();
        Time.timeScale = 0.0f;      
    }

    public void ShowGameOver()
    {
        AudioManager.Instance.OnPauseMusic();
        GoToPause();               
    }

    public void ShowGameWon()
    {      
        _progressionBar.SetActive(false);
        DisableStarsProgress();
        _gameOverUI.SetActive(false);
        _gameWinUI.SetActive(true);

        for(int i = 0; i < _pointsArray.Length; i++)
        {
            if(Points >= _pointsArray[i])
            {
                _starsGO[i].SetActive(true);               
            }
        }
        GoToPause();
        StartCoroutine(MenuReturnDelay());
    }

    private IEnumerator MenuReturnDelay()
    {
        yield return new WaitForSecondsRealtime(3.0f);
        ShowInterstitialBeforeMenu();
    }

    public void SetPointsValue()
    {      
        _streak++;
        _streakText.text = $"X {_streak} !";
        AnimationSystem.Instance.ActiveStreak();
        _currentScore = Points;
    }

    private void SpawnStar()
    {
        EffectsSystem.Instance.SpawnStartEffets(_stars[_currentStar]);
    }

    private void ShowGameOverPanel()
    {
        if (_progressionBar != null && _gameOverUI != null && _gameOverUI != null)
        {
            _progressionBar.SetActive(false);
            _gameWinUI.SetActive(false);
            _gameOverUI.SetActive(true);
        }
        //_finalScoreText.text = "Ваши очки: " + Points;
    }

    private void RemoveObjectsFromScene()
    {
        if(_progressionBar!= null)
          //  _progressionBar.SetActive(false);
        Conductor._longs.Clear();
        Conductor._shorts.Clear();
    }

    private void DisableStarsProgress()
    {
        for (int i = 0; i < _stars.Count; i++)
        {
            if (_stars[i] != null)
                 _stars[i].SetActive(false);
        }
    }

    public void StartTheGame()
    {
        Time.timeScale = 1.0f;
        IsSpawn = true;
        Destroy(_startGO);
        IsStarted = true;
        IsWon = false;
    }


    public void OnMenuReturnButtonCLick()
    {
        ShowInterstitialBeforeMenu();
    }

    private void ShowInterstitialBeforeMenu()
    {
        Debug.Log("beforeShowing");
        AdManager.Instance.RunInterstitial();       
    }

    public void GoToTheMenu()
    {
        SceneManager.LoadSceneAsync("MenuScene");
    }

    public void CloseLoseTab()
    {
        _gameOverUI.SetActive(false);
    }
}
