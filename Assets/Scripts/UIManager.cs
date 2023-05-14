using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : BaseManager<UIManager>
{
    [SerializeField] private EndGamePanel endGamePanel;
    public EndGamePanel EndGamePanel => endGamePanel;
    [SerializeField] private SettingPanel settingPanel;
    public SettingPanel SettingPanel => settingPanel;
    [SerializeField] private ShopPanel shopPanel;
    public ShopPanel ShopPanel => shopPanel;
    [SerializeField] private PausePanel pausePanel;
    public PausePanel PausePanel => pausePanel;
    [SerializeField] private UI_IngamePanel uiIngamePanel;
    public UI_IngamePanel UIIngamePanel => uiIngamePanel;
    [SerializeField] private MenuPanel menuPanel;
    public MenuPanel MenuPanel => menuPanel;

    private void Start()
    {
        menuPanel.gameObject.SetActive(true);
        uiIngamePanel.gameObject.SetActive(false);
        pausePanel.gameObject.SetActive(false);
        shopPanel.gameObject.SetActive(false);
        settingPanel.gameObject.SetActive(false);
        endGamePanel.gameObject.SetActive(false);
        MenuPanel.Instance.UpdateInfo();
    }

    public void ActiveMenuPanel(bool active)
    {
        menuPanel.gameObject.SetActive(active);
    }    
    public void ActiveUIIngamePanel(bool active)
    {
        uiIngamePanel.gameObject.SetActive(active);
    }    
    public void ActivePausePanel(bool active)
    {
        pausePanel.gameObject.SetActive(active);
    }    
    public void ActiveShopPanel(bool active)
    {
        shopPanel.gameObject.SetActive(active);
    }    
    public void ActiveSettingPanel(bool active)
    {
        settingPanel.gameObject.SetActive(active);
    }    
    public void ActiveEndGamePanel(bool active)
    {
        endGamePanel.gameObject.SetActive(active);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameManager.HasInstance && GameManager.Instance.IsPlaying == true)
            {
                GameManager.Instance.PauseGame();
                ActivePausePanel(true);
            }
        }
    }

}
