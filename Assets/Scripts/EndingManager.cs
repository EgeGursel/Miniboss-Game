using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingManager : MonoBehaviour
{
    public Transform boss;
    private GameObject player;
    private PlayerAttack playerAttack;
    public ParticleSystem explosionPS;
    public static EndingManager instance;
    public DialogueTrigger dialogueTrigger;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").gameObject;
        playerAttack = player.GetComponent<PlayerAttack>();
    }
    public void BossDead()
    {
        Instantiate(explosionPS, boss.position, boss.rotation);
        PlayerPrefs.SetInt("Katana", 0);
        PlayerPrefs.SetInt("Bow", 0);
        playerAttack.CheckWeapons();
        AudioManager.instance.Stop("theme");
        StartCoroutine(Wait());
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
        dialogueTrigger.StartDialogue();
    }
}
