using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPanel : MonoBehaviour
{
    public void OnStartButtonClick()
    {
        if (UIManager.HasInstance)
        {
            UIManager.Instance.ActiveMenuPanel(false);
            UIManager.Instance.ActiveUIIngamePanel(true);
            UIManager.Instance.ActivePausePanel(false);
            UIManager.Instance.ActiveShopPanel(false);
            UIManager.Instance.ActiveSettingPanel(false);
            UIManager.Instance.ActiveEndGamePanel(false);
            GameManager.Instance.StartGame();

        }
    }
    public void OnSettingButtonClick()
    {
        if (UIManager.HasInstance)
        {
            UIManager.Instance.ActiveMenuPanel(false);
            UIManager.Instance.ActiveShopPanel(false);
            UIManager.Instance.ActiveSettingPanel(true);
        }
    }

    public void OnShopButtonClick()
    {
        if(UIManager.HasInstance)
        {
            UIManager.Instance.ActiveMenuPanel(false);
            UIManager.Instance.ActiveShopPanel(true);
            UIManager.Instance.ActiveSettingPanel(false);
        }    
    }    
    public void OnMusiceButtonClick()
    {
        if(UIManager.HasInstance)
        {
        }    
    }    
}
