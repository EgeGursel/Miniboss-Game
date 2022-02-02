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

    // DROP
    public Transform soulPrefab;
    public Transform coinPrefab;
    private Vector3 dropPos;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        enemyAnimator = GetComponent<Animator>();
        playerObject = GameObject.FindGameObjectWithTag("Player");
        player = playerObject.GetComponent<Player>();
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
        if (gameObject.name.StartsWith("Slime"))
        {
            Instantiate(soulPrefab, transform.position, transform.rotation);
        }
        else if (gameObject.name.StartsWith("Wizard"))
        {
            dropPos = new Vector3(transform.position.x - 2.5f, transform.position.y, transform.position.z);
            for (int i = 0; i < 6; i++)
            {
                Instantiate(coinPrefab, dropPos, transform.rotation);
                dropPos.x += 0.5f;
            }
        }
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.TakeDamage(attackDamage);
        }
        return;
    }
}
