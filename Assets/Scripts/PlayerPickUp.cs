using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUp : MonoBehaviour
{
    Coins coins;
    Souls souls;
    Player player;
    public int levelSoulCount = 0;
    public int levelCoinCount = 0;
    public GameObject shopOneSymbol;
    public GameObject shopTwoSymbol;
    public bool hasKey = false;

    private void Start()
    {
        player = GetComponent<Player>();
        coins = GameObject.FindGameObjectWithTag("CoinCounter").GetComponent<Coins>();
        souls = GameObject.FindGameObjectWithTag("SoulCounter").GetComponent<Souls>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Collectable"))
        {
            string collectableName = collision.gameObject.name;
            Destroy(collision.gameObject);
            AudioManager.instance.Play("pickup");

            // NON-VISUAL COLLECTABLES
            if (collectableName == "Dash Ability")
            {
                PlayerPrefs.SetInt("DashActive", 1);
            }
            else if (collectableName == "Candy")
            {
                return;
            }
            else if (collectableName.StartsWith("Key"))
            {
                hasKey = true;
                InfoBarManager.instance.gameObject.SetActive(true);
                InfoBarManager.instance.SendInfoBar("cell key");
                return;
            }
            else if (collectableName.StartsWith("Coin"))
            {
                levelCoinCount += 1;
                coins.AddCoins(1);
                if (PlayerPrefs.GetInt("Coins") == 15)
                {
                    shopOneSymbol.GetComponent<Animator>().SetTrigger("Highlighted");
                }
                return;
            }
            else if (collectableName.StartsWith("Soul Fragment"))
            {
                levelSoulCount += 1;
                souls.AddSouls(1);
                if (PlayerPrefs.GetInt("SoulFragments") == 15)
                {
                    shopTwoSymbol.GetComponent<Animator>().SetTrigger("Highlighted");
                }
                return;
            }
            else if (collectableName.StartsWith("Health"))
            {
                player.Heal(35);
                return;
            }

            // VISUAL COLLECTABLES
            else
            {

                foreach (Transform child in transform)
                {
                    if (collectableName == child.name)
                    {
                        child.gameObject.SetActive(true);
                        PlayerPrefs.SetInt(collectableName, 1);
                    }
                    else
                    {
                        child.gameObject.SetActive(false);
                        PlayerPrefs.SetInt(child.name, 0);
                    }
                }
            }
            InfoBarManager.instance.gameObject.SetActive(true);
            InfoBarManager.instance.SendInfoBar(collectableName);
        }
    }
}
