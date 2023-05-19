using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


// Use this to serialize a struct
[System.Serializable]
public struct ColorToSell
{
    public Color Sellcolor;
    public int price;
}

public enum ColorType
{
    playerColor,
    platformColor
}

public class ShopPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI notifyText;
    [Header("Platform colors")]
    [SerializeField] private ColorToSell[] platformColors;

    [SerializeField] private GameObject platformColorButton;
    [SerializeField] private Transform platformColorParent;
    [SerializeField] private Image platformDisplay;

    [Header("Player colors")]
    [SerializeField] private ColorToSell[] playerColors;

    [SerializeField] private GameObject playerColorButton;
    [SerializeField] private Transform playerColorParent;
    [SerializeField] private Image playerDisplay;
    [SerializeField] private SpriteRenderer playerDisplaysr;


    private void Update()
    {
        coinText.text = PlayerPrefs.GetInt("Coins").ToString("#,#");
    }
    public void OnExitButtonClick()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySE(AUDIO.BGM_SFX_UI_CLICK);
        }
        UIManager.Instance.ActiveMenuPanel(true);
        UIManager.Instance.ActiveShopPanel(false);
    }
    private void Start()
    {
        for (int i = 0; i <platformColors.Length; i++)
        {
            Color color = platformColors[i].Sellcolor;
            int price = platformColors[i].price;
            GameObject newButton = Instantiate(platformColorButton, platformColorParent);
            newButton.transform.GetChild(0).GetComponent<Image>().color = platformColors[i].Sellcolor;
            newButton.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = price.ToString("#,#");
            newButton.GetComponent<Button>().onClick.AddListener(() => PurchaseColor(color, price, ColorType.platformColor));
        }

        for (int i = 0; i < playerColors.Length; i++)
        {
            Color color = playerColors[i].Sellcolor;
            int price = playerColors[i].price;
            GameObject newButton = Instantiate(playerColorButton, playerColorParent);
            newButton.transform.GetChild(0).GetComponent<Image>().color = playerColors[i].Sellcolor;
            newButton.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = price.ToString("#,#");
            newButton.GetComponent<Button>().onClick.AddListener(() => PurchaseColor(color, price, ColorType.playerColor));
        }
        playerDisplay.color = new Color(PlayerPrefs.GetFloat("ColorR"), PlayerPrefs.GetFloat("ColorG"), PlayerPrefs.GetFloat("ColorB"));
        platformDisplay.color = GameManager.Instance.platformColor;
    }

    public void PurchaseColor(Color Buycolor, int price, ColorType colorType)
    {
        if (EnoughMoney(price))
        {
            if (colorType == ColorType.platformColor)
            {
                GameManager.Instance.platformColor = Buycolor;
                GameManager.Instance.SavePlatformColor(Buycolor.r, Buycolor.g, Buycolor.b);
                platformDisplay.color = Buycolor;
            }
            else if (colorType == ColorType.playerColor)
            {
                GameManager.Instance.Player.GetComponent<SpriteRenderer>().color = Buycolor;
                GameManager.Instance.SaveColor(Buycolor.r, Buycolor.g, Buycolor.b);
                playerDisplay.color = Buycolor;
            }
        }
    }

    private bool EnoughMoney(int price)
    {
        int myCoins = PlayerPrefs.GetInt("Coins");
        if (myCoins > price)
        {
            int newAmountOfCoins = myCoins - price;
            PlayerPrefs.SetInt("Coins",newAmountOfCoins);
            MenuPanel.Instance.UpdateInfo();
            coinText.text = PlayerPrefs.GetInt("Coins").ToString("#,#");
            if (AudioManager.HasInstance)
            {
                AudioManager.Instance.PlaySE(AUDIO.BGM_SFX_UI_CLICK);
            }
            StartCoroutine(Notify("Buy Successful", 3f));
            return true;
        }
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySE(AUDIO.BGM_SFX_UI_CLICK);
        }
        StartCoroutine(Notify("Not Enought Coin", 3f));
        return false;
    }

    //Notify Buy Success

    IEnumerator Notify(string text, float seconds)
    {
        notifyText.text = text;
        yield return new WaitForSeconds(seconds);
        notifyText.text = "Click To Buy";
    }    
}
