using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    Player player;
    Button[] shopButtons;
    Coins coins;

    // Start is called before the first frame update
    void Start()
    {
        coins = GameObject.FindGameObjectWithTag("CoinCounter").GetComponent<Coins>();
        player = FindObjectOfType<Player>().GetComponent<Player>();
        shopButtons = gameObject.GetComponentsInChildren<Button>();

       foreach (Button button in shopButtons)
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

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BuySB()
    {
        coins.AddCoins(-15);
        PlayerPrefs.SetFloat("RunSpeed", PlayerPrefs.GetFloat("RunSpeed") * 1.2f);
    }
    public void BuyDMG()
    {
        coins.AddCoins(-20);
        PlayerPrefs.SetFloat("AttackDamage", PlayerPrefs.GetFloat("AttackDamage") * 1.2f);
    }
    public void BuyShield()
    {
        coins.AddCoins(-30);
        PlayerPrefs.SetInt("Shield", PlayerPrefs.GetInt("Shield") * 2);
    }
}
