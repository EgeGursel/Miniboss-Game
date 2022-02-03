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
    private Button[] shopButtons;

    void Start()
    {
        shopButtons = GetComponentsInChildren<Button>();
        sbLogo = uIUpgrades.transform.GetChild(0).gameObject;
        dmgLogo = uIUpgrades.transform.GetChild(1).gameObject;
        sLogo = uIUpgrades.transform.GetChild(2).gameObject;
        UpdateVisuals();
    }
    public void BuySB()
    {
        coins.AddCoins(-15);
        PlayerPrefs.SetFloat("RunSpeed", PlayerPrefs.GetFloat("RunSpeed") + 0.2f);
        CheckAvailability();
    }
    public void BuyDMG()
    {
        coins.AddCoins(-20);
        PlayerPrefs.SetFloat("AttackDamage", PlayerPrefs.GetFloat("AttackDamage") + 0.2f);
        CheckAvailability();
    }
    public void BuyShield()
    {
        coins.AddCoins(-30);
        PlayerPrefs.SetFloat("Shield", PlayerPrefs.GetFloat("Shield") + 0.2f);
        CheckAvailability();
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
        UpdateVisuals();
    }
    public void UpdateVisuals()
    {
        uIUpgrades.transform.GetChild(0).gameObject.GetComponentInChildren<Text>().text = "X" + PlayerPrefs.GetFloat("RunSpeed");
        uIUpgrades.transform.GetChild(1).gameObject.GetComponentInChildren<Text>().text = "X" + PlayerPrefs.GetFloat("AttackDamage");
        uIUpgrades.transform.GetChild(2).gameObject.GetComponentInChildren<Text>().text = "X" + PlayerPrefs.GetFloat("Shield");
    }
    private void OnEnable()
    {
        CheckAvailability();
    }
}
