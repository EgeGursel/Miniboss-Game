using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponShop : MonoBehaviour
{
    public Souls souls;
    PlayerAttack playerAttack;
    public Button buyButton;

    private void Start()
    {
        PlayerPrefs.SetInt("SoulsFragments", 30);
        playerAttack = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>();
    }
    public void BuyBow()
    {
        souls.AddSouls(-15);
        PlayerPrefs.SetInt("Bow", 1);
        PlayerPrefs.SetInt("Katana", 0);
        playerAttack.CheckWeapons();
        CheckAvailability();
    }
    public void CheckAvailability()
    {
        if (PlayerPrefs.GetInt("SoulsFragments") < int.Parse(buyButton.GetComponentInChildren<Text>().text))
        {
            buyButton.interactable = false;
        }
        else
        {
            buyButton.interactable = true;
        }
    }
    private void OnEnable()
    {
        CheckAvailability();
    }
}
