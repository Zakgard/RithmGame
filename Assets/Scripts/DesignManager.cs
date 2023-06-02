using System.Collections.Generic;
using UnityEngine;

public class DesignManager : MonoBehaviour
{
    [SerializeField] private List<Sprite> _backGrounds;
    [SerializeField] private List<Sprite> _shortKeys;
    [SerializeField] private List<Sprite> _longKeys;
    [SerializeField] private GameObject _backGround;
    [SerializeField] private GameObject _shortKey;
    [SerializeField] private GameObject _longKey;
    [SerializeField] private bool _isTest;
    [SerializeField] private int _testIndex;

    private SpriteRenderer _backR;
    private SpriteRenderer _shortR;
    private SpriteRenderer _longR;

    private void Start()
    {
        _backR = _backGround.GetComponent<SpriteRenderer>();
        _shortR = _shortKey.GetComponent<SpriteRenderer>();
        _longR = _longKey.GetComponent<SpriteRenderer>();

        GetDesignIndex();
    }

    private void GetDesignIndex()
    {
        int index = PlayerPrefs.GetInt("designIndex");
        if (_isTest)
            ChangeSprite(_testIndex);
        else
            ChangeSprite(index);
    }

    private void ChangeSprite(int index)
    {
        _backR.sprite = _backGrounds[index];
        _shortR.sprite = _shortKeys[index];
        _longR.sprite = _longKeys[index];
    }
}
