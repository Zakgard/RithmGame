using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesignManager : MonoBehaviour
{
    [SerializeField] private List<Sprite> _firstBackGrounds;
    [SerializeField] private List<Sprite> _secondBackGrounds;
    [SerializeField] private List<Sprite> _thirdBackGrounds;
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
    private float _trackLength;

    private void Start()
    {
        _trackLength = AudioManager.Instance.GetTrackLength();
        _backR = _backGround.GetComponent<SpriteRenderer>();
        _shortR = _shortKey.GetComponent<SpriteRenderer>();
        _longR = _longKey.GetComponent<SpriteRenderer>();
        GetDesignIndex();      
    }

    private void GetDesignIndex()
    {
        int index = 1;
        index = PlayerPrefs.GetInt("designIndex");
        Debug.Log(index);
        if (_isTest)
            ChangeSprite(_testIndex);
        else
            ChangeSprite(index);
    }

    private void ChangeSprite(int index)
    {       
        if(Conductor._shorts != null)
        {
            for (int i = 0; i < Conductor._shorts.Count; i++)
            {
                Conductor._shorts[i].GetComponent<SpriteRenderer>().sprite = _shortKeys[index];
            }
            StartCoroutine(ChangeBackGroundToNextPart(index));
        }       
    }

    private IEnumerator ChangeBackGroundToNextPart(int index)
    {
        _backR.sprite = _firstBackGrounds[index];
        yield return new WaitForSeconds(_trackLength/ 3.0f);
        _backR.sprite = _secondBackGrounds[index];
        yield return new WaitForSeconds(_trackLength / 3.0f);
        _backR.sprite = _thirdBackGrounds[index];
    }
}
