using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackGrounStoreController : MonoBehaviour
{
    [SerializeField] private Image _image;

    [SerializeField] private List<Sprite> sprites;

    public void SetBackGroundOnImage(int index)
    {
        _image.sprite = sprites[index];
    }
}
