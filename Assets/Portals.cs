using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portals : MonoBehaviour
{
    Player player;
    public GameObject sceneLoader;

    void Start()
    {
        player = FindObjectOfType<Player>().GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            sceneLoader.GetComponent<SceneLoader>().LoadNextScene();
        }
    }
}

