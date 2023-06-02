using UnityEngine;

public class DesignStoreSystem : MonoBehaviour
{
    private int _index;

    private void Start()
    {
        _index = 0;
    }

    public void ChangeDesignIndex(int designIndex)
    {
        _index= designIndex;
    }

    public void SetIndex()
    {
        PlayerPrefs.SetInt("designIndex", _index);
    }
}
