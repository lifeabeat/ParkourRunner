using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndGamePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI distance;
    [SerializeField] private TextMeshProUGUI score;
    private void Start()
    {
        if (GameManager.Instance.distance < 0)
        {
            return;
        }
        if (GameManager.Instance.Coins < 0)
        {
            return;
        }
        distance.text = "Distance: " +GameManager.Instance.distance.ToString("#,#") +" m";
        score.text = "Score: " +GameManager.Instance.score.ToString("#,#");
        coinText.text = "Coins: " +GameManager.Instance.Coins.ToString("#,#");
        
    }
    
    public void OnMenuButtonClick()
    {
        GameManager.Instance.RestartGame();
    }    
}
