using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coins : MonoBehaviour
{
    Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        text.text = PlayerPrefs.GetInt("Coins").ToString();
    }
    public void AddCoins(int addedCoins)
    {
        PlayerPrefs.SetInt("Coins", (PlayerPrefs.GetInt("Coins") + addedCoins));
        text.text = PlayerPrefs.GetInt("Coins").ToString();
    }
}
