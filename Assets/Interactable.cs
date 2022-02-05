using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    private Transform player;
    public GameObject infoBar;
    private bool interacted = false;
    private PlayerPickUp playerItems;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerItems = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPickUp>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && interacted == false)
        {
            infoBar.SetActive(true);
            InfoBarManager.instance.SendQuickSpecial("press e to interact", "");
        }
    }

    private void Update()
    {
        if (Vector3.Distance(player.position, transform.position) < 1)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (playerItems.hasKey == true)
                {
                    // PLAY DOOR OPEN SOUND
                    gameObject.SetActive(false);
                }
                else
                {
                    infoBar.SetActive(true);
                    InfoBarManager.instance.SendQuickSpecial("cell key missing!", "");
                }
            }
        }
        
    }
}
