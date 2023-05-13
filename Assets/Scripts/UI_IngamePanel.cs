using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_IngamePanel : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI numberOfCoins;
    public TextMeshProUGUI NumberOfCoins => numberOfCoins;

    // Using Delegate to listen number coin Player collect
    private void OnEnable()
    {
        Coin.collectCoinDelegate += OnPlayerCollect;
    }
    private void OnDisable()
    {
        Coin.collectCoinDelegate -= OnPlayerCollect;
    }

    private void OnPlayerCollect(int value)
    {
        numberOfCoins.SetText(value.ToString());
    }
}
