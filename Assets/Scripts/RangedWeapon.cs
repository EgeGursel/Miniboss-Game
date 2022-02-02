using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : MonoBehaviour
{
    Transform firePoint;
    Animator animator;
    public GameObject arrowPrefab;
    public float attackCooldown = 0.5f;
    private bool attackCD = false;

    // ROTATE AIM DIRECTION
    Transform player;
    Vector3 mousePos;
    Vector3 objectPos;
    float angle;

    // Start is called before the first frame update
    void Start()
    {
        player = transform.parent.transform;
        animator = transform.GetChild(0).GetComponent<Animator>();
        firePoint = transform.GetChild(0).transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0f)
        {
            return;
        }

        if (Input.GetButtonDown("Fire1") && PlayerPrefs.GetInt("Bow") == 1 && !attackCD)
        {
            Shoot();
        }
        
        mousePos = Input.mousePosition;
        mousePos.z = 5.23f;

        objectPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        if (player.rotation.y < 0)
        {
            if (angle < -120 || angle > 120)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            }
        }
        else
        {
            if (angle < 60 && angle > -60)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            }
        }
    }
    IEnumerator AttackCooldown()
    {
        attackCD = true;
        yield return new WaitForSeconds(attackCooldown);
        attackCD = false;
    }
    void Shoot()
    {
        Instantiate(arrowPrefab, firePoint.position, transform.rotation);
        animator.SetTrigger("Attack");
        StartCoroutine(AttackCooldown());
    }
}
