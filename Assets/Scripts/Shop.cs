using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    Coins coins;
    public GameObject items;

    // Start is called before the first frame update
    void Start()
    {
        coins = GameObject.FindGameObjectWithTag("CoinCounter").GetComponent<Coins>();
        CheckAvailability();
    }
    public void BuySB()
    {
        coins.AddCoins(-15);
        PlayerPrefs.SetFloat("RunSpeed", PlayerPrefs.GetFloat("RunSpeed") * 1.2f);
        CheckAvailability();
    }
    public void BuyDMG()
    {
        coins.AddCoins(-20);
        PlayerPrefs.SetFloat("AttackDamage", PlayerPrefs.GetFloat("AttackDamage") * 1.2f);
        CheckAvailability();
    }
    public void BuyShield()
    {
        coins.AddCoins(-30);
        PlayerPrefs.SetInt("Shield", PlayerPrefs.GetInt("Shield") * 2);
        CheckAvailability();
    }
    public void CheckAvailability()
    {
        foreach (Button button in items.GetComponentsInChildren<Button>())
        {
            if (PlayerPrefs.GetInt("Coins") < int.Parse(button.GetComponentInChildren<Text>().text))
            {
                button.interactable = false;
            }
            else
            {
                button.interactable = true;
            }
        }
    }
}
