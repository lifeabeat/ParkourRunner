using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : MonoBehaviour
{
    [SerializeField]
    private Slider bgmSlider;
    [SerializeField]
    private Slider seSlider;

    public float bgmValue;
    public float seValue;

    private void Awake()
    {
        if (AudioManager.HasInstance)
        {
            bgmValue = AudioManager.Instance.AttachBGMSource.volume;
            seValue = AudioManager.Instance.AttachSESource.volume;
            bgmSlider.value = bgmValue;
            seSlider.value = seValue;
        }
    }

    private void OnEnable()
    {
        if (AudioManager.HasInstance)
        {
            bgmValue = AudioManager.Instance.AttachBGMSource.volume;
            seValue = AudioManager.Instance.AttachSESource.volume;

            bgmSlider.value = bgmValue;
            seSlider.value = seValue;
        }
    }

    public void OnSliderChangeBGMValue(float v)
    {
        bgmValue = v;
    }

    public void OnSliderChangeSEValue(float v)
    {
        seValue = v;
    }


    public void OnSubmitButtonClick()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySE(AUDIO.BGM_SFX_UI_CLICK);

            AudioManager.Instance.ChangeBGMVolume(bgmValue);
            AudioManager.Instance.ChangeSEVolume(seValue);
        }
        if (UIManager.HasInstance)
        {
            UIManager.Instance.ActiveSettingPanel(false);
            UIManager.Instance.ActiveMenuPanel(true);
        }
    }
    public void OnExitButtonClick()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySE(AUDIO.BGM_SFX_UI_CLICK);
        }
        UIManager.Instance.ActiveMenuPanel(true);
        UIManager.Instance.ActiveSettingPanel(false);
    }    
}
