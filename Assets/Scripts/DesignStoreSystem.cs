using TMPro;
using UnityEngine;

public class DesignStoreSystem : MonoBehaviour
{
    private int _index;
    [SerializeField] private TMP_Text _buttonText;

    public static DesignStoreSystem Instance;

    private void Awake()
    {
        Instance = this;
    }

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
        SetButtonPressed();
    }

    public void SetButtonPressed()
    {
        _buttonText.text = "Выбрано!";
    }

    public void SetButtonUnpressed()
    {
        _buttonText.text = "Выбрать";
    }
}
