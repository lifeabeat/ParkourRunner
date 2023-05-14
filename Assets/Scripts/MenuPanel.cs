using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuPanel : BaseManager<MenuPanel>
{
    [SerializeField] private TextMeshProUGUI lastscoreText;
    [SerializeField] private TextMeshProUGUI highscoreText;
    [SerializeField] private TextMeshProUGUI coinText;

    public void UpdateInfo()
    {
        lastscoreText.text = "Lastest Score: " + PlayerPrefs.GetFloat("LastScore").ToString();
        highscoreText.text = "Highest Score: " + PlayerPrefs.GetFloat("HighScore").ToString();
        coinText.text = PlayerPrefs.GetInt("Coins").ToString();  
    }

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
