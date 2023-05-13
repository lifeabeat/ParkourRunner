using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingPanel : MonoBehaviour
{
    public void OnExitButtonClick()
    {
        UIManager.Instance.ActiveMenuPanel(true);
        UIManager.Instance.ActiveSettingPanel(false);
    }    
}
