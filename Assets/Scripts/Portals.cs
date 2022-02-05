using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portals : MonoBehaviour
{
    void Start()
    {
        InfoBarManager.instance.SendSpecial("Progress Saved", "");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            SceneLoader.instance.LoadNextScene();
        }
        else if (collision.CompareTag("Player") && GameObject.FindGameObjectsWithTag("Enemy").Length > 0)
        {
            InfoBarManager.instance.gameObject.SetActive(true);
            InfoBarManager.instance.SendSpecial("Kill all the enemies first!", "");
        }
    }
}

