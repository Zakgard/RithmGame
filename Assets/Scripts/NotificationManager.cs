using TMPro;
using UnityEngine;

public class NotificationManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _notificationText;

    private void Start()
    {
        int coinsLack = CurrencyManager.Instance.GetCoinsLack(200);
        _notificationText.text = $"Недостаточно валюты ({coinsLack})!";
    }
}
