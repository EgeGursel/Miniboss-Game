using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingManager : MonoBehaviour
{
    public Transform boss;
    private GameObject player;
    private PlayerPickUp playerPickUp;
    public ParticleSystem explosionPS;
    public static EndingManager instance;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        PlayerPrefs.SetInt("Bow", 1);
        player = GameObject.FindGameObjectWithTag("Player").gameObject;
        playerPickUp = player.GetComponent<PlayerPickUp>();
    }
    public void BossDead()
    {
        Instantiate(explosionPS, boss.position, boss.rotation);
        AudioManager.instance.Stop("theme");
    }
    public void GameEnd()
    {

    }
}
