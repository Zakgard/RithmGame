using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SongDataUISystem : MonoBehaviour
{
    [SerializeField] private TMP_Text _authorText;
    [SerializeField] private TMP_Text _titleText;
    [SerializeField] private List<TrakList> _traksDdescriptionList;


    private void Start()
    {
        _authorText.text = _traksDdescriptionList[Conductor.LevelIndex].Author;
        _titleText.text = _traksDdescriptionList[Conductor.LevelIndex].Name;
    }
}
