using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPickUp : MonoBehaviour
{
    Coins coins;
    public GameObject shopSymbol;
    public GameObject infoBar;
    public GameObject boss;
    public Text coinText;
    public bool weaponActive = false;
    private void Start()
    {
        coins = GameObject.FindGameObjectWithTag("CoinCounter").GetComponent<Coins>();
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
            coins.AddCoins(1);
            if (PlayerPrefs.GetInt("Coins") == 3)
            {
                shopSymbol.GetComponent<Animator>().SetTrigger("Highlighted");
            }
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
                    weaponActive = true;
                }
                else
                {
                    child.gameObject.SetActive(false);
                }
            }
        }
        infoBar.SetActive(true);
        InfoBarManager.instance.SendInfoBar(collectableName);
    }
}
