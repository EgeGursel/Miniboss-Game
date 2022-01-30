using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPickUp : MonoBehaviour
{
    int coins;
    public GameObject infoBar;
    public GameObject boss;
    public Text coinText;
    public bool dashActive = false;
    public bool weaponActive = false;
    private void Start()
    {
        coins = PlayerPrefs.GetInt("Coins");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Collectable"))
        {
            return;
        }

        string collectableName = collision.gameObject.name;

        // NON-VISUAL COLLECTABLES
        if (collectableName == "Dash Ability")
        {
            dashActive = true;
        }
        else if (collectableName == "Candy")
        {
            boss.SetActive(true);
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
        Destroy(collision.gameObject);
        if (collectableName == "Coin")
        {
            PlayerPrefs.SetInt("Coins", coins += 1);
            coinText.text = coins.ToString();
            return;
        }
        infoBar.SetActive(true);
        InfoBarManager.instance.SendInfoBar(collectableName);
    }
}
