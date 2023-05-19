using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : BaseManager<GameManager>
{
    [SerializeField] private Player player;
    public Player Player => player;
    [SerializeField] private SpriteRenderer theSR;
    public SpriteRenderer TheSR => theSR;
    // Using Delegate for tracking coin
    private int coins = 0;
    public int Coins => coins;
    public void UpdateCoins(int v)
    {
        coins = v;
    }
    //Score
    public float score;
    [Header("Sky Box")]
    [SerializeField] private Material[] SkyBoxMat;


    [Header("Distance Info")]
    public float distance;

    [Header("Color Purchased Info")]
    public Color platformColor;

    protected override void Awake()
    {
        SetupSkyBox(PlayerPrefs.GetInt("SkyBoxSetting"));
    }
    private void Start()
    {
        player = GetComponent<Player>();
        theSR = GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>();
        //Using PlayerRef to Update Player Skin every first time to start the game
        GetSavedColor(theSR);
        GetPlatformSavedColor();
    }

    public void UnlockPlayer()
    {
        player.PlayerUnlocked = true;
    }
    //Pause Menu

    private bool isPlaying = false;
    public bool IsPlaying => isPlaying;

    public void StartGame()
    {
        isPlaying = true;
        Time.timeScale = 1f;
        UnlockPlayer();
    }
    public void PauseGame()
    {
        if (isPlaying)
        {
            isPlaying = false;
            Time.timeScale = 0f;
        }

    }
    public void ResumeGame()
    {
        isPlaying = true;
        Time.timeScale = 1f;
    }

    public void EndGame()
    {
        SaveInfo();
        isPlaying = false;
        Time.timeScale = 0f;
        if (UIManager.HasInstance)
        {
            UIManager.Instance.ActiveMenuPanel(false);
            UIManager.Instance.ActiveUIIngamePanel(false);
            UIManager.Instance.ActiveEndGamePanel(true);
        }

    }
    public void RestartGame()
    {
        isPlaying = true;
        Time.timeScale = 1f;
        coins = 0;
        
        if (UIManager.HasInstance && MenuPanel.HasInstance)
        {
            UIManager.Instance.ActiveMenuPanel(true);
            UIManager.Instance.ActiveUIIngamePanel(false);
            UIManager.Instance.ActiveEndGamePanel(false);
            MenuPanel.Instance.UpdateInfo();
            // Old
            /*UIManager.Instance.UIIngamePanel.GetComponent<GamePanel>.NumberOfCoins.SetText("0");*/
            UIManager.Instance.UIIngamePanel.NumberOfCoins.SetText("0");
            SceneManager.LoadScene(0);
        }
    
    }

    public void SetupSkyBox(int i)
    {
        if (i <= 1)
        {
            RenderSettings.skybox = SkyBoxMat[i];
        }
        else
        {
            RenderSettings.skybox = SkyBoxMat[Random.Range(0, SkyBoxMat.Length)];
        }

        PlayerPrefs.SetInt("SkyBoxSetting", i);
    }

    public void SaveColor(float r, float g, float b)
    {
        PlayerPrefs.SetFloat("ColorR", r);
        PlayerPrefs.SetFloat("ColorG", g);
        PlayerPrefs.SetFloat("ColorB", b);
    }
    public void SavePlatformColor(float pr, float pg, float pb)
    {
        PlayerPrefs.SetFloat("ColorpR", pr);
        PlayerPrefs.SetFloat("ColorpG", pg);
        PlayerPrefs.SetFloat("ColorpB", pb);
    }

    public void GetPlatformSavedColor()
    {
        float colorpR = PlayerPrefs.GetFloat("ColorpR");
        float colorpG = PlayerPrefs.GetFloat("ColorpG");
        float colorpB = PlayerPrefs.GetFloat("ColorpB");

        platformColor = new Color(colorpR, colorpG, colorpB, 1);
    }
    public void GetSavedColor(SpriteRenderer theSR)
    {
        float colorR = PlayerPrefs.GetFloat("ColorR");
        float colorG = PlayerPrefs.GetFloat("ColorG");
        float colorB = PlayerPrefs.GetFloat("ColorB");

        theSR.color = new Color(colorR, colorG, colorB, 1);
    }

    private void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            distance = player.transform.position.x;
        }
        if (theSR == null)
        {
            theSR = GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>();
        }
        if (player.transform.position.x >= distance)
        {
            distance = player.transform.position.x;
        }
    }

    public void SaveInfo()
    {
        int savedCoins = PlayerPrefs.GetInt("Coins");
        PlayerPrefs.SetInt("Coins", savedCoins + coins);

        score = distance * coins;
        
        PlayerPrefs.SetFloat("LastScore", score);

        if (PlayerPrefs.GetFloat("HighScore") < score)
        {
            PlayerPrefs.SetFloat("HighScore", score);
        }
    }

}
