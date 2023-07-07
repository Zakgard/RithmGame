using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackGrounStoreController : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private List<Button> _buttons;
    [SerializeField] private List<Sprite> sprites;

    private int _lastIndex;

    private void Start()
    {
        _lastIndex = 0;
    }

    public void SetBackGroundOnImage(int index)
    {
        _buttons[_lastIndex].interactable = true;
        _image.sprite = sprites[index];
        _buttons[index].interactable = false;
        _lastIndex = index;
        DesignStoreSystem.Instance.SetButtonUnpressed();
    }
}
