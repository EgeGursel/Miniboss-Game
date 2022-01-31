using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : MonoBehaviour
{
    Transform firePoint;
    Animator animator;
    public GameObject arrowPrefab;
    public float attackCooldown = 0.4f;
    private bool attackCD = true;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        firePoint = transform.GetChild(0).transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && PlayerPrefs.GetInt("Bow") == 1 && PlayerPrefs.GetInt("Katana") == 0)
        {
            Shoot();
        }
    }
    IEnumerator AttackCooldown()
    {
        attackCD = false;
        yield return new WaitForSeconds(attackCooldown);
        attackCD = true;
    }
    void Shoot()
    {
        if (attackCD)
        {
            Instantiate(arrowPrefab, firePoint.position, firePoint.rotation);

        }
        animator.SetTrigger("Attack");
        StartCoroutine(AttackCooldown());
    }
    
}
