using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuPanel : BaseManager<MenuPanel>
{
    [SerializeField] private TextMeshProUGUI lastscoreText;
    [SerializeField] private TextMeshProUGUI highscoreText;
    [SerializeField] private TextMeshProUGUI coinText;

    [SerializeField]
    private GameObject howToPlayImage;
    [SerializeField]
    private GameObject howToPlayBtn;

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
        
        if(AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySE(AUDIO.BGM_SFX_UI_CLICK);
        }    
        if (UIManager.HasInstance)
        {
            UIManager.Instance.ActiveMenuPanel(false);
            UIManager.Instance.ActiveShopPanel(false);
            UIManager.Instance.ActiveSettingPanel(true);
        }
    }

    public void OnShopButtonClick()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySE(AUDIO.BGM_SFX_UI_CLICK);
        }
        if (UIManager.HasInstance)
        {
            UIManager.Instance.ActiveMenuPanel(false);
            UIManager.Instance.ActiveShopPanel(true);
            UIManager.Instance.ActiveSettingPanel(false);
        }    
    }    
    public void OnMusiceButtonClick()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySE(AUDIO.BGM_SFX_UI_CLICK);
        }
        if (UIManager.HasInstance)
        {
        }    
    }

    public void OnHowToPlayButtonClick()
    {
        howToPlayImage.SetActive(true);
        howToPlayBtn.SetActive(false);
    }

    public void OnHowToPlayButtonImgClick()
    {
        howToPlayImage.SetActive(false);
        howToPlayBtn.SetActive(true);
    }
}
