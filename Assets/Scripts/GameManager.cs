using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : BaseManager<GameManager>
{
    // Using Delegate
    private int coins = 0;
    public int Coins => coins;

    public void UpdateCoins(int v)
    {
        coins = v;
    }

    //
    public Color platformColor;

    public void RestartLevel()
    {
        coins = 0;
        if (UIManager.HasInstance)
        {
            UIManager.Instance.UIIngamePanel.NumberOfCoins.SetText("0");
        }
        SceneManager.LoadScene(0);
    }

    //Pause Menu

    private bool isPlaying = false;
    public bool IsPlaying => isPlaying;

    public void StartGame()
    {
        isPlaying = true;
        Time.timeScale = 1f;
    }
    public void PauseGame()
    {
        if (isPlaying)
        {
            isPlaying = false;
            Time.timeScale = 0f;
        }

    }
    public void ResumeGame()
    {
        isPlaying = true;
        Time.timeScale = 1f;
    }

    public void RestartGame()
    {
        coins = 0;
        if (UIManager.HasInstance)
        {
            UIManager.Instance.ActiveUIIngamePanel(false);
            UIManager.Instance.ActiveMenuPanel(true);
            // Old
            /*UIManager.Instance.UIIngamePanel.GetComponent<GamePanel>.NumberOfCoins.SetText("0");*/
            UIManager.Instance.UIIngamePanel.NumberOfCoins.SetText("0");
            SceneManager.LoadScene(0);
        }
    }

}
