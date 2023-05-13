using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanel : MonoBehaviour
{
    public void OnResumeButtonClick()
    {
        if (GameManager.HasInstance && UIManager.HasInstance)
        {
            GameManager.Instance.ResumeGame();
            UIManager.Instance.ActivePausePanel(false);
        }
    }

    public void OnMenuButtonClick()
    {
        if (UIManager.HasInstance && GameManager.HasInstance)
        {
            UIManager.Instance.ActiveMenuPanel(true);
            UIManager.Instance.ActiveUIIngamePanel(false);
            UIManager.Instance.ActivePausePanel(false);
            GameManager.Instance.ResumeGame();
            GameManager.Instance.RestartGame();
        }
    }
    public void OnMusiceButtonClick()
    {
        if (UIManager.HasInstance)
        {
        }
    }
}
