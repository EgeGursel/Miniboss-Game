using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // HEALTH
    public int maxHealth = 100;
    public int currentHealth;

    // VISUALS
    public ParticleSystem hurtPS;
    SpriteRenderer spriteRenderer;

    // ATTACK
    public int attackDamage = 20;
    Player player;
    PatrolAI patrolAI;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = FindObjectOfType<Player>().GetComponent<Player>();
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
        StartCoroutine(HurtVisuals());
    }
    public void Die()
    {
        hurtPS.transform.position = gameObject.transform.position;
        hurtPS.Play();
        gameObject.SetActive(false);
    }
    IEnumerator HurtVisuals()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.15f);
        spriteRenderer.color = Color.white;
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
