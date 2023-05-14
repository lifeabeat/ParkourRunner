using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_IngamePanel : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private TextMeshProUGUI numberOfCoins;
    public TextMeshProUGUI NumberOfCoins => numberOfCoins;
    [SerializeField] private TextMeshProUGUI distance;
    public TextMeshProUGUI Distance => distance;
     
    private float mDistance;

    [SerializeField] private Image heartEmpty;
    [SerializeField] private Image heartFull;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        InvokeRepeating("UpdateDistance", 0, .15f);
    }
    // Update Distance   
    private void UpdateDistance()
    {
        ExtraLife();
        mDistance = GameManager.Instance.distance;

        if (mDistance > 0 )
        { 
            Distance.text = GameManager.Instance.distance.ToString("#,#") + " m"; 
        } 
    }

    private void ExtraLife()
    {
        heartEmpty.enabled = !player.extraLife;
        heartFull.enabled = player.extraLife;
    }    
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
        NumberOfCoins.SetText(value.ToString());
    }
    // Button
    public void OnPauseButtonClick()
    {
        if(GameManager.HasInstance && UIManager.HasInstance)
        {
            GameManager.Instance.PauseGame();
            UIManager.Instance.ActivePausePanel(true);
        }    
    }    

    public void OnMusicButtonClick()
    {

    }    
}
