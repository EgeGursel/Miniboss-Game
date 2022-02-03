using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponShop : MonoBehaviour
{
    public Souls souls;
    PlayerAttack playerAttack;
    private Button[] shopButtons;
    private void Start()
    {
        playerAttack = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>();
        shopButtons = GetComponentsInChildren<Button>();
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
    private void OnEnable()
    {
        CheckAvailability();
    }
}
