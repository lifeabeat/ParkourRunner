using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPanel : MonoBehaviour
{
    public void OnExitButtonClick()
    {
        UIManager.Instance.ActiveMenuPanel(true);
        UIManager.Instance.ActiveShopPanel(false);
    }
}
