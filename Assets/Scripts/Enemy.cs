using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // HEALTH
    public int maxHealth = 120;
    public int currentHealth;

    // VISUALS
    public ParticleSystem hurtPS;
    Animator enemyAnimator;

    // ATTACK
    public int attackDamage = 20;
    GameObject playerObject;
    Player player;
    PatrolAI patrolAI;

    // DROP
    public Transform soulPrefab;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        enemyAnimator = GetComponent<Animator>();
        playerObject = GameObject.FindGameObjectWithTag("Player");
        player = playerObject.GetComponent<Player>();
        patrolAI = GetComponent<PatrolAI>();
    }
    public void Damage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
            return;
        }
        enemyAnimator.SetTrigger("Damaged");
    }
    public void Die()
    {
        hurtPS.transform.position = transform.position;
        hurtPS.Play();
        Instantiate(soulPrefab, transform.position, transform.rotation);
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.TakeDamage(attackDamage);
            StartCoroutine(patrolAI.PlayerBumped());
        }
    }
}
