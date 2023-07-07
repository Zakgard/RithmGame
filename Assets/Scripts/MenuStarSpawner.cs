using System.Collections.Generic;
using UnityEngine;

public class MenuStarSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _starPrefab;
    [SerializeField] private int _starsExistAtOnce;
    [SerializeField] private float _xTopBound;
    [SerializeField] private float _yTopBound;
    [SerializeField] private float _xBottomBound;
    [SerializeField] private float _yBottomBound;
    [SerializeField] private float _minStarsDistance;
    [SerializeField] private float _timeBetweenSpawns;

    private Vector2 _currnetSpawnPosition;
    private float _currnetTimeSinceLastSpawn;

    public static List<GameObject> _activeStarsPool;
    public static List<Vector3> _activeStarsPositions;

    public static MenuStarSpawner Instance;
    

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _activeStarsPool= new List<GameObject>();
        _activeStarsPositions= new List<Vector3>();

        _activeStarsPool.Add(Instantiate(_starPrefab));
        _activeStarsPositions.Add(_activeStarsPool[0].transform.position);
        _currnetTimeSinceLastSpawn = 0.0f;
    }

    private void Update()
    {
        if(_activeStarsPool.Count <= _starsExistAtOnce && _currnetTimeSinceLastSpawn >= _timeBetweenSpawns)
        {
            GetSpawnCoordinates();
            _activeStarsPool.Add(Instantiate(_starPrefab, _currnetSpawnPosition, Quaternion.identity));
            _activeStarsPositions.Add(_currnetSpawnPosition);
            _currnetTimeSinceLastSpawn = 0.0f;
        }
        else
        {
            _currnetTimeSinceLastSpawn += Time.deltaTime;
        }
    }

    private void GetSpawnCoordinates()
    {
        _currnetSpawnPosition = new Vector2(Random.Range(_xBottomBound, _xTopBound), Random.Range(_yBottomBound, _yTopBound));

        for(int i = 0; i < _activeStarsPositions.Count; i++)
        {
            if(Vector3.Distance(_currnetSpawnPosition, _activeStarsPositions[i]) < _minStarsDistance)
            {
                GetSpawnCoordinates();
            }
        }
    }

    public void ClearListPosition(GameObject starToRemove)
    {
        int index = _activeStarsPool.IndexOf(starToRemove);
        _activeStarsPool.RemoveAt(index);
        _activeStarsPositions.RemoveAt(index);
    }
}
