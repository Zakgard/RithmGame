using System;
using UnityEngine;
using UnityEngine.UI;

public class UiPaneButtonStateManager : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _commercialButton;
    [SerializeField] private Button _purchaseButton;
    [SerializeField] private string _levelName;
    [SerializeField] private int _levelPrice;
    [SerializeField] private GameObject _notificationPanel;

    private event Action _onCommercialSucces;

    private void Start()
    {
        if (CurrencyManager.Instance.IsCleanup())
        {
            Game.PreferencesService.SetInt(_levelName, 0);
        }   

        int activeStatus = Game.PreferencesService.GetInt(_levelName, 0);

        if (activeStatus == 0)
        {
            _playButton.gameObject.SetActive(false);

            if (_commercialButton != null)
            {
                _commercialButton.gameObject.SetActive(true);
            }

            if(_purchaseButton != null)
            {
                _purchaseButton.gameObject.SetActive(true);
            }
        }  
        else
        {
            _playButton.gameObject.SetActive(true);
             
            if(_commercialButton!= null)
            {
                _commercialButton.gameObject.SetActive(false);
            }

            if(_purchaseButton!= null)
            {
                _purchaseButton.gameObject.SetActive(false);
            }
        }                    
        _onCommercialSucces += ChangeButtonsState;
    }
        

    private void OnDestroy()
    {
        _onCommercialSucces-= ChangeButtonsState;
    }

    private void ChangeButtonsState()
    {
        if(_commercialButton != null)
        {
            _commercialButton.gameObject.SetActive(false);
        }

        if(_purchaseButton != null)
        {
            _purchaseButton.gameObject.SetActive(false);
        }
        
        _playButton.gameObject.SetActive(true);
        Game.PreferencesService.SetInt($"{_levelName}", 1);
    }

    public void OnCommercialButtonClick()
    {
        Game.AdvertisingService.ShowRewarded(_onCommercialSucces);
    }

    public void OnPurchaseButtonClick()
    {
        if (CurrencyManager.Instance.IsEnoughCurrencyToBuyLevel(_levelPrice))
        {
            CurrencyManager.Instance.ChangeCoinsAmount(-_levelPrice);
            ChangeButtonsState();
        }
        else
        {
            if(_notificationPanel!= null)
            {
                _notificationPanel.gameObject.SetActive(true);
            }          
        }
    }
}
