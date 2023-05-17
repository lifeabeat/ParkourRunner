using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanel : MonoBehaviour
{
    public void OnResumeButtonClick()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySE(AUDIO.BGM_SFX_UI_CLICK);
        }
        if (GameManager.HasInstance && UIManager.HasInstance)
        {
            GameManager.Instance.ResumeGame();
            UIManager.Instance.ActivePausePanel(false);
        }
    }

    public void OnMenuButtonClick()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySE(AUDIO.BGM_SFX_UI_CLICK);
        }
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
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySE(AUDIO.BGM_SFX_UI_CLICK);
        }
        if (UIManager.HasInstance)
        {
        }
    }
}
