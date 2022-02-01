using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPickUp : MonoBehaviour
{
    Coins coins;
    Coins souls;
    Player player;
    public int levelSoulCount = 0;
    public int levelCoinCount = 0;
    public GameObject shopOneSymbol;
    public GameObject shopTwoSymbol;
    public GameObject infoBar;
    public GameObject boss;
    private void Start()
    {
        player = GetComponent<Player>();
        coins = GameObject.FindGameObjectWithTag("CoinCounter").GetComponent<Coins>();
        souls = GameObject.FindGameObjectWithTag("SoulCounter").GetComponent<Coins>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Collectable"))
        {
            return;
        }

        string collectableName = collision.gameObject.name;
        Destroy(collision.gameObject);

        // NON-VISUAL COLLECTABLES
        if (collectableName == "Dash Ability")
        {
            PlayerPrefs.SetInt("DashActive", 1);
        }
        else if (collectableName == "Candy")
        {
            boss.SetActive(true);
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
            player.Heal(25);
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
        infoBar.SetActive(true);
        InfoBarManager.instance.SendInfoBar(collectableName);
    }
}
