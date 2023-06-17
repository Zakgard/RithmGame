using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SongDataUISystem : MonoBehaviour
{
    [SerializeField] private TMP_Text _authorText;
    [SerializeField] private TMP_Text _titleText;
    [SerializeField] private TMP_Text _difficultyText;
    [SerializeField] private List<TrakList> _traksDdescriptionList;
    [SerializeField] private List<string> _diffucltList;
    [SerializeField] private List<string> _difficultyColors; 

    private void Start()
    {
        int songInforamtionIndex = Mathf.RoundToInt(Conductor.LevelIndex / 3.0f);
        int difficultIndex = Conductor.LevelIndex % 3;
        _authorText.text = _traksDdescriptionList[songInforamtionIndex].Author;
        _titleText.text = _traksDdescriptionList[songInforamtionIndex].Name;
        SetDifficultyColor(difficultIndex);
        _difficultyText.text = _diffucltList[difficultIndex];       
    }

    private void SetDifficultyColor(int index)
    {
        switch (index)
        {
            case 0:
                _difficultyText.color = Color.green;
            break;
            case 1:
                _difficultyText.color = Color.yellow;
            break;
            case 2:
                _difficultyText.color = Color.red;
            break;
        }
    }
}
