using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : BaseManager<GameManager>
{
    // Using Delegate
    private int coins = 0;
    public int Coins => coins;

    public void UpdateCoins(int v)
    {
        coins = v;
    }

    public Color platformColor;

    public void RestartLevel() => SceneManager.LoadScene(0);
    
}
