using UnityEngine;

public class AlgorithmSpawner : MonoBehaviour
{
    private int _lastIndex;
    private int _lastBigCounter;
    private float[] _bounds = new float[4];
    private int _currentPrefabIndex;

    public static AlgorithmSpawner Instance;

    private void Awake()
    {
        Instance = this; 
    }

    public void SetAmplitudeBouns()
    {
        _bounds = TrackDataCollector.Instance.GetBounds(_bounds.Length);
    }

    public int SpawnWIthAlgorithm()
    {       
        float wave = TrackDataCollector.Instance.GetMusicData();
        int index = 0;

        if (wave > _bounds[1])
        {
            if (wave > _bounds[2])
                index = 3;
            if (wave <= _bounds[2])
                index = 2;
        }
        else
        {
            if (wave < _bounds[0])
                index = 0;
            else
                index = 1;
        }

        if (index == _lastIndex)
        {
            index = ChangeSamePosition(_lastIndex);
            _lastIndex = index;
        }

        int tempIndex = Random.Range(0, _lastBigCounter);

        if (tempIndex < 2)
        {
            _currentPrefabIndex = 1;
            _lastBigCounter = 10;
        }
        else
        {
            _currentPrefabIndex = 0;
            _lastBigCounter--;
        }
        return index;
    }

    private int ChangeSamePosition(int lastIndex)
    {
        int newIndex = Random.Range(0, 4);
        if(newIndex == lastIndex)
            ChangeSamePosition(newIndex);
        return newIndex;
    }

}
