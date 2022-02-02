using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponShop : MonoBehaviour
{
    public Coins souls;
    PlayerAttack playerAttack;
    private void Start()
    {
        playerAttack = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>();
        souls.AddSouls(25);
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
        foreach (Button button in GetComponentsInChildren<Button>())
        {
            if (button.name != "CloseButton")
            {
                if (PlayerPrefs.GetInt("SoulsFragments") < int.Parse(button.GetComponentInChildren<Text>().text))
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
