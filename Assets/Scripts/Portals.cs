using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portals : MonoBehaviour
{
    Player player;
    public GameObject infoBar;
    public GameObject sceneLoader;

    void Start()
    {
        player = FindObjectOfType<Player>().GetComponent<Player>();
        infoBar.SetActive(true);
        InfoBarManager.instance.SendSpecial("Progress Saved", "");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            sceneLoader.GetComponent<SceneLoader>().LoadNextScene();
        }
        else if (collision.CompareTag("Player") && GameObject.FindGameObjectsWithTag("Enemy").Length > 0)
        {
            infoBar.SetActive(true);    
            InfoBarManager.instance.SendSpecial("Kill all the enemies first!", "");
        }
    }
}

