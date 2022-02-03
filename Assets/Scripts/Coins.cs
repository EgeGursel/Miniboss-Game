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
        UpdateVisuals();
    }
    public void AddCoins(int addedCoins)
    {
        if (gameObject.name == "CoinText")
        {
            PlayerPrefs.SetInt("Coins", ((PlayerPrefs.GetInt("Coins") + addedCoins)));
            UpdateVisuals();
        }
        
    }
    public void AddSouls(int addedSouls)
    {
        if(gameObject.name == "SoulText")
        {
            PlayerPrefs.SetInt("SoulFragments", (PlayerPrefs.GetInt("SoulFragments") + addedSouls));
            UpdateVisuals();
        }
    }
    private void UpdateVisuals()
    {
        if (gameObject.name == "CoinText")
        {
            text.text = PlayerPrefs.GetInt("Coins").ToString();
        }
        else if (gameObject.name == "SoulText")
        {
            text.text = PlayerPrefs.GetInt("SoulFragments").ToString();
        }
    }
}
