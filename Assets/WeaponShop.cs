using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponShop : MonoBehaviour
{
    Coins souls;
    // Start is called before the first frame update
    void Start()
    {
        souls = GameObject.FindGameObjectWithTag("SoulCounter").GetComponent<Coins>();
        CheckAvailability();
    }
    public void BuyBow()
    {
        souls.AddSouls(-15);
        PlayerPrefs.SetInt("Bow", 1);
        PlayerPrefs.SetInt("Katana", 0);
        CheckAvailability();
    }
    public void CheckAvailability()
    {
        foreach (Button button in GetComponentsInChildren<Button>())
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
