using System.Collections.Generic;
using UnityEngine;

public class Conductor : MonoBehaviour
{
    [SerializeField] private int _timesToLong;
    [SerializeField] private List<int[]> _array;
    [SerializeField] private int _levelIndex;
    [SerializeField] private bool _test;
    [SerializeField] private List<GameObject> _levelsList;
    [SerializeField] private List<float> _yOfftest;
     
    public static bool Test;
    [SerializeField] private float _delayAtTheEnd;
    [SerializeField] private List<float> _speedPerLevel;

    public static List<GameObject> _shorts;
    public static List<GameObject> _longs;

    public static int LevelIndex;
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
    }

    private void Start()
    {
        _shorts= new List<GameObject>();
        _longs= new List<GameObject>();

        Test = _test;

        if (Test)
        {
            LevelIndex = _levelIndex;
        }
        else
        {
            LevelIndex = _levelIndexFromPrefs;
        }

        MovementSpeed = _speedPerLevel[LevelIndex];
        CurrentGO = null;
    }

    private void Update()
    {
        if(_shorts.Count == 0 && _longs.Count == 0 && SessionManager.IsStarted) {
            StartCoroutine(SessionManager.Instance.GameWon());
        }
    }

    public void SpawnLevel()
    {
        Instantiate(_levelsList[LevelIndex], new Vector3(0, 10 + _yOfftest[LevelIndex], 0), Quaternion.identity);
        InsertObjectsInLists();

        if(!Test)
        {
            if(_shorts != null && _longs != null)
               UnTest();
        }
        else
        {
            EnableTest();
        }
    }

    private void InsertObjectsInLists()
    {
        _shorts.AddRange(GameObject.FindGameObjectsWithTag("short"));
        _longs.AddRange(GameObject.FindGameObjectsWithTag("long"));
    }

    private void UnTest()
    {
        for(int i = 0; i < _shorts.Count; i++)
        {
            _shorts[i].GetComponent<PianoKeySystem>().isTest = false;
        }

        for(int i = 0; i < _longs.Count; i++)
        {
            _longs[i].GetComponent<PianoKeySystem>().isTest = false;
        }
    }

    private void EnableTest()
    {
        for (int i = 0; i < _shorts.Count; i++)
        {
            _shorts[i].GetComponent<PianoKeySystem>().isTest = true;
        }

        for (int i = 0; i < _longs.Count; i++)
        {
            _longs[i].GetComponent<PianoKeySystem>().isTest = true;
        }
    }
}
