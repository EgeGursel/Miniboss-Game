using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public Coins coins;
    public GameObject uIUpgrades;
    private GameObject sbLogo;
    private GameObject dmgLogo;
    private GameObject sLogo;

    void Start()
    {
        sbLogo = uIUpgrades.transform.GetChild(0).gameObject;
        dmgLogo = uIUpgrades.transform.GetChild(1).gameObject;
        sLogo = uIUpgrades.transform.GetChild(2).gameObject;
    }
    public void BuySB()
    {
        coins.AddCoins(-15);
        PlayerPrefs.SetFloat("RunSpeed", PlayerPrefs.GetFloat("RunSpeed") * 1.2f);
        CheckAvailability();
        sbLogo.GetComponentInChildren<Text>().text = "X" + PlayerPrefs.GetFloat("RunSpeed");
    }
    public void BuyDMG()
    {
        coins.AddCoins(-20);
        PlayerPrefs.SetFloat("AttackDamage", PlayerPrefs.GetFloat("AttackDamage") * 1.2f);
        CheckAvailability();
        dmgLogo.GetComponentInChildren<Text>().text = "X" + PlayerPrefs.GetFloat("AttackDamage");
    }
    public void BuyShield()
    {
        coins.AddCoins(-30);
        PlayerPrefs.SetFloat("Shield", PlayerPrefs.GetFloat("Shield") * 1.2f);
        CheckAvailability();
        sLogo.GetComponentInChildren<Text>().text = "X" + PlayerPrefs.GetFloat("Shield");
    }
    public void CheckAvailability()
    {
        foreach (Button button in GetComponentsInChildren<Button>())
        {
            if (button.name != "CloseButton")
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
}
