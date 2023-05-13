using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Coin : MonoBehaviour
{
    public delegate void CollectCoin(int coin); // Define Delegate
    public static CollectCoin collectCoinDelegate; // Announce Delegate
    private int coins = 0;
    private void Start()
    {
        if (GameManager.HasInstance)
        {
            coins = GameManager.Instance.Coins;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
            coins++;
            GameManager.Instance.UpdateCoins(coins);
            collectCoinDelegate(coins); //Broadcast event for IngamePanel

        }
    }

    
}
