using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGenerator : MonoBehaviour
{
    [SerializeField] private int amountOfCoins;
    [SerializeField] private GameObject coinPrefab;
    //Way 1
    [SerializeField] private int minCoins;
    [SerializeField] private int maxCoins;

    [SerializeField] private SpriteRenderer[] coinImg;
    //Way 2
    /*[SerializeField] private float chanceOfSpawn;*/

    private void Start()
    {
        for (int i = 0; i < coinImg.Length; i++)
        {
            coinImg[i].sprite = null;
        }
        amountOfCoins = Random.Range(minCoins, maxCoins);

        int additionalOffset = amountOfCoins / 2;
        for (int i = 0; i < amountOfCoins; i++)
        {
            Vector3 offset = new Vector2(i - additionalOffset, 0);
            //Way 2
            /*bool canSpawn = chanceOfSpawn > Random.Range(0, 100);*/
            /*if(canSpawn)*/
            
                Instantiate(coinPrefab, transform.position + offset, Quaternion.identity, transform);
        }    


    }
}
