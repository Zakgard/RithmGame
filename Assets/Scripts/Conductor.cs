using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conductor : MonoBehaviour
{
    [SerializeField] private List<GameObject> _objectsToSpawn;
    [SerializeField] private float _topBound;
    [SerializeField] private int _timesToLong;
    [SerializeField] private List<Vector2> _spawnOffsets;
    [SerializeField] private List<int[]> _array;
    [SerializeField] private List<SubList> _list;
    [SerializeField] private List<OffsetsList> _offsets;
    [SerializeField] private int _levelIndex;
    [SerializeField] private List<float> _keysSpawnOffset;
    [SerializeField] private bool _isWithAlgorithm;
     
    [SerializeField] private bool _isTest;
    [SerializeField] private float _delayAtTheEnd;
    [SerializeField] private List<float> _speedPerLevel;

    private float _smallAfterSmall;
    private float _bigAfterBig;
    private float _bigAfterSmall;
    private float _smallAfterBig;

    public static int LevelIndex;
    private int _songIndex;   
    private int _currentPrefabIndex;
    private int _index;
    private float _positionY;
    private bool _isBig;
    private int _levelIndexFromPrefs;

    public static float MovementSpeed;
    public static bool IsCompleted;
    public Vector2[] spawnPoints;

    public static GameObject CurrentGO;
    [SerializeField] private int _boundAmount;
 
    public static Conductor Instance;

    private void Awake()
    {
        Instance = this;
        _levelIndexFromPrefs = PlayerPrefs.GetInt("MusicIndex");

        if (_isTest)
        {
            LevelIndex = _levelIndex;
            _songIndex = _levelIndex;
        }
        else
        {
            LevelIndex = _list[_levelIndexFromPrefs].Level;
            _songIndex = LevelIndex;
        }
        MovementSpeed = _speedPerLevel[LevelIndex];
    }

    private void Start()
    {
        CurrentGO = null;
        _index = 0;       
        _bigAfterSmall = _offsets[LevelIndex].BigAfterSmall;
        _bigAfterBig = _offsets[LevelIndex].BigAfterBig;
        _smallAfterBig = _offsets[LevelIndex].SmallAfterBig;
        _smallAfterSmall = _offsets[LevelIndex].SmallAfterSmall;

        _currentPrefabIndex = 0;
    }
    
    private void Update()
    {
        if (SessionManager.IsStarted) 
        {          
            if(CurrentGO != null)
            {
                _positionY = CurrentGO.transform.position.y;
                _isBig = CurrentGO.GetComponent<PianoKeySystem>().isBig;
            }
            DetecteSpawn();
        }
    }

    private void DetecteSpawn()
    {
        if (_index == 0)
            SpawnObject();
        else if (_index < _list[_songIndex].IsBig.Count && _isBig)
        {
            if (!_list[_songIndex].IsBig[_index] && _positionY + _bigAfterSmall <= _topBound - _spawnOffsets[0].y)
            {
                SpawnObject();
            }
            else if (_list[_songIndex].IsBig[_index] && _positionY + _bigAfterBig <= _topBound)
            {
                SpawnObject();
            }
        }
        else if (_index < _list[_songIndex].IsBig.Count && !_isBig)
        {
            if (!_list[_songIndex].IsBig[_index] && _positionY + _smallAfterBig <= _spawnOffsets[0].y)
            {
                SpawnObject();
            }
            else if (_list[_songIndex].IsBig[_index] && _positionY + _smallAfterSmall <= _topBound)
            {
                SpawnObject();
            }
        }
        else
        {
            IsCompleted = true;
            StartCoroutine(StopMusicAfterDelay());
        }
    }

    private IEnumerator StopMusicAfterDelay()
    {
        yield return new WaitForSeconds(_delayAtTheEnd);
        AudioManager.Instance.StopMusic();
    }


    private void SpawnObject()
    {
        int index;
        if (!_isWithAlgorithm)
        {
            index = _list[LevelIndex].SpawnIndex[_index];
            Spawn(index);
        }
        else
        {
            index = AlgorithmSpawner.Instance.SpawnWIthAlgorithm();
            Spawn(index);
            Debug.Log(index);
        }
    }

    private void Spawn(int index)
    {
        if (_list[LevelIndex].IsBig[_index])
        {
            _currentPrefabIndex = 1;
        }
        else
        {
            _currentPrefabIndex = 0;
        }

        Vector2 spawnPoint = spawnPoints[index] - _spawnOffsets[_currentPrefabIndex];
        CurrentGO = Instantiate(_objectsToSpawn[_currentPrefabIndex], spawnPoint, Quaternion.identity);
        SessionManager.ObjectsOnscene.Add(CurrentGO);
        _index++;
    }
}
