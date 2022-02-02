using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAgro : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    public float moveSpeed;
    public float agroRange;
    Transform player;

    // ATTACK
    Transform firePoint;
    public GameObject projectilePrefab;
    public float attackCooldown;
    private bool attackCD = false;

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed *= PlayerPrefs.GetFloat("RunSpeed");
        firePoint = transform.GetChild(0).transform;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));

        float playerDistance = Vector2.Distance(transform.position, player.position);
        
        if (playerDistance < agroRange && playerDistance > 3.7f)
        {
            StartAgro();
        }
        else if (playerDistance > agroRange)
        {
            StopAgro();
        }
        
        if (playerDistance < 3.8f)
        {
            StopAgro();
            if (!attackCD)
            {
                Attack();
            } 
        }
    }
    private void StartAgro()
    {
        if (transform.position.x < player.position.x)
        {
            transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            rb.velocity = new Vector2(moveSpeed, 0);
        }
        else
        {
            transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
            rb.velocity = new Vector2(-moveSpeed, 0);
        }
    }
    private void StopAgro()
    {
        rb.velocity = new Vector2(0, 0);
    }
    IEnumerator AttackCooldown()
    {
        attackCD = true;
        yield return new WaitForSeconds(attackCooldown);
        attackCD = false;
    }
    private void Attack()
    {
        int randVal = UnityEngine.Random.Range(-4, 4);
        firePoint.eulerAngles = new Vector3(firePoint.rotation.x, firePoint.rotation.y, firePoint.rotation.z + randVal);
        animator.SetTrigger("Attack");
        for (int i = 0; i < 11; i++)
        {
            Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            firePoint.Rotate(0, 0, 30, Space.Self);
        }
        StartCoroutine(AttackCooldown());
    }
}
