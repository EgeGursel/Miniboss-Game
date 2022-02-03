using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coins : MonoBehaviour
{
    private Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        UpdateVisuals();
    }
    public void AddCoins(int addedCoins)
    {
        PlayerPrefs.SetInt("Coins", ((PlayerPrefs.GetInt("Coins") + addedCoins)));
        UpdateVisuals();        
    }
    private void UpdateVisuals()
    {
        text.text = PlayerPrefs.GetInt("Coins").ToString();
    }
}
